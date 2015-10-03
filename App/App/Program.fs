open System
open System.Reactive
open System.Reactive.Subjects
 
type Message<'a, 'b> =
    | Completed
    | Error of exn
    | Next of 'a
    | CompletedWork
    | NextAndGet of 'a * AsyncReplyChannel<'b>
    | Subscribe of IObserver<'b>
    | Unsubscribe of IObserver<'b>
 
type state<'a> = { isStopped:bool; observers:IObserver<'a> list}
 
type AgentRx<'a, 'b>(work:('a -> 'b), ?limit:int, ?token:System.Threading.CancellationToken) =
  
    let token = defaultArg token (new System.Threading.CancellationToken())
    let limit = defaultArg limit 1
    let publish f (observers:IObserver<'b> list) = observers |> List.iter f
    let onCompleted() = publish (fun o -> o.OnCompleted())
    let onError exn = publish (fun o -> o.OnError(exn))
    let onNext item = publish (fun o -> o.OnNext(item))
    let subscribe observer  (observers:IObserver<'b> list) = observer::observers
    let unsubscribe observer (observers:IObserver<'b> list) = observers |> List.filter((<>) observer)
    
    let agent =
        MailboxProcessor<Message<'a, 'b>>.Start(fun inbox ->
 
                    let rec waiting subscribers =
                        inbox.Scan(function 
                            | CompletedWork -> Some( loop (limit - 1) subscribers )
                            | Subscribe(o) -> Some( waiting (subscribe o subscribers) )
                            | Unsubscribe(o) -> Some( waiting (unsubscribe o subscribers) ) 
                            | _ -> None )
                    and loop count subscribers = async {
                        let! msg = inbox.Receive()
                        match msg with
                        | Subscribe(o) -> return! loop count (subscribe o subscribers)
                        | Unsubscribe(o) -> return! loop count (unsubscribe o subscribers)
                        | Completed -> ()
                        | CompletedWork -> return! loop (count - 1) subscribers
                        | NextAndGet(item, reply) ->
                                        async { try
                                                    let res = work item  
                                                    reply.Reply res                                   
                                                    onNext res subscribers
                                                finally inbox.Post(CompletedWork) }
                                        |> Async.Start
                                        if count < limit - 1 then return! loop (count + 1) subscribers
                                        else return! waiting subscribers
                        | Error(exn) -> onError exn subscribers
                                        return! loop count subscribers
                        | Next(item) -> async { try
                                                    let res = work item
                                                    onNext res subscribers
                                                finally inbox.Post(CompletedWork) } 
                                        |> Async.Start
                                        if count < limit - 1 then return! loop (count + 1) subscribers
                                        else return! waiting subscribers
                    }
                    loop 0 [])
 
    do
        token.Register(fun () -> agent.Post(Completed)
                                 (agent :> IDisposable).Dispose()) |> ignore
 
    member x.DoWork(item:'a)  = agent.Post(Next item)
    member x.DoWorkResult(item:'a)  = agent.PostAndAsyncReply(fun ch -> NextAndGet(item, ch))
    member x.Stop() = agent.Post(Completed)
 
    member x.LinkTo(agentRx:AgentRx<'b, _>) =
            x.Subscribe(agentRx.DoWork)
 
    interface IObserver<'a> with
        member x.OnCompleted() = x.Stop()
        member x.OnNext(item) = x.DoWork item
        member x.OnError(exn) = agent.Post(Error exn)
       
    interface IObservable<'b> with
        member x.Subscribe(observer) =
            agent.Post(Subscribe(observer))
            { new IDisposable with
                member x.Dispose() =
                    agent.Post(Unsubscribe(observer))
                }
   
let m = AgentRx(fun n -> printfn "Hello World %s" n
                         7)   
let d = m.Subscribe(fun n -> printfn "result = %d" (n * n)) 
m.DoWork("Ciao")

let n = AgentRx(fun n -> printfn "Get result World %d" n)
m.LinkTo(n)

(d :> IDisposable).Dispose()
 
/// Message type used by the agent - contains queueing
/// of work items and notification of completion
type internal ThrottlingAgentMessage =
  | Completed
  | Work of Async<unit>
   

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    System.Console.ReadLine() |> ignore
    0
    