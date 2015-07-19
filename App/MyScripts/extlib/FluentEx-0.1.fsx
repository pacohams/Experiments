
[<AutoOpen>]
module FluentExtensions = 
    open System
    open System.Linq
    open System.Linq.Expressions
    open System.Collections.Generic
    open System.Runtime.CompilerServices

    [<Extension>] 
    type Extensions =

        /// <summary>Returns the average of the elements in the array.</summary>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when <c>array</c> is empty.</exception>
        /// <returns>The average of the elements in the array.</returns>
        [<Extension>] 
        static member inline average(array:'T[]) = Array.average array

        /// <summary>Returns the average of the elements generated by applying the function to each element of the array.</summary>
        /// <param name="projection">The function to transform the array elements before averaging.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when <c>array</c> is empty.</exception>
        /// <returns>The computed average.</returns>
        [<Extension>] 
        static member inline averageBy(array:'T[], f) = Array.averageBy f array

        /// <summary>Returns the greatest of all elements of the array, compared via Operators.max on the function result.</summary>
        ///
        /// <remarks>Throws ArgumentException for empty arrays.</remarks>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        /// <returns>The maximum element.</returns>
        [<Extension>]
        static member inline max(array:'T[]) = Array.max array

        /// <summary>Returns the greatest of all elements of the array, compared via Operators.max on the function result.</summary>
        ///
        /// <remarks>Throws ArgumentException for empty arrays.</remarks>
        /// <param name="projection">The function to transform the elements into a type supporting comparison.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        /// <returns>The maximum element.</returns>
        [<Extension>]
        static member inline maxBy(array:'T[], projection) = Array.maxBy projection array

        /// <summary>Returns the lowest of all elements of the array, compared via Operators.min.</summary>
        ///
        /// <remarks>Throws ArgumentException for empty arrays</remarks>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        /// <returns>The minimum element.</returns>
        [<Extension>]
        static member inline min (array:'T[]) = Array.min array

        /// <summary>Returns the lowest of all elements of the array, compared via Operators.min on the function result.</summary>
        ///
        /// <remarks>Throws ArgumentException for empty arrays.</remarks>
        /// <param name="projection">The function to transform the elements into a type supporting comparison.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        /// <returns>The minimum element.</returns>
        [<Extension>]
        static member inline minBy(array:'T[], projection) = Array.minBy projection array

        /// <summary>Sorts the elements of an array, returning a new array. Elements are compared using Operators.compare. </summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved. 
        /// For a stable sort, consider using Seq.sort.</remarks>
        /// <param name="array">The input array.</param>
        /// <returns>The sorted array.</returns>
        [<Extension>]
        static member inline sort(array:'T[]) = Array.sort array

        /// <summary>Sorts the elements of an array, using the given projection for the keys and returning a new array. 
        /// Elements are compared using Operators.compare.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved. 
        /// For a stable sort, consider using Seq.sort.</remarks>
        /// <param name="projection">The function to transform array elements into the type that is compared.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The sorted array.</returns>
        [<Extension>]
        static member inline sortBy(array:'T[], projection) = Array.sortBy projection array

        /// <summary>Sorts the elements of an array, using the given comparison function as the order, returning a new array.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved. 
        /// For a stable sort, consider using Seq.sort.</remarks>
        /// <param name="comparer">The function to compare pairs of array elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The sorted array.</returns>
        [<Extension>]
        static member inline sortWith(array:'T[], comparer) = Array.sortWith comparer array

        /// <summary>Sorts the elements of an array by mutating the array in-place, using the given projection for the keys. 
        /// Elements are compared using Operators.compare.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved. 
        /// For a stable sort, consider using Seq.sort.</remarks>
        /// <param name="projection">The function to transform array elements into the type that is compared.</param>
        /// <param name="array">The input array.</param>
        [<Extension>]
        static member inline sortInPlaceBy (array:'T[],projection) = Array.sortInPlaceBy projection array

        /// <summary>Sorts the elements of an array by mutating the array in-place, using the given comparison function as the order.</summary>
        /// <param name="comparer">The function to compare pairs of array elements.</param>
        /// <param name="array">The input array.</param>
        [<Extension>]
        static member inline sortInPlaceWith(array:'T[],comparer) = Array.sortInPlaceWith comparer array

        /// <summary>Sorts the elements of an array by mutating the array in-place, using the given comparison function. 
        /// Elements are compared using Operators.compare.</summary>
        /// <param name="array">The input array.</param>
        [<Extension>]
        static member inline sortInPlace(array) = Array.sortInPlace array

        /// <summary>Returns the sum of the elements in the array.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>The resulting sum.</returns>
        [<Extension>]
        static member inline sum(array: 'T[]) = Array.sum array

        /// <summary>Returns the sum of the results generated by applying the function to each element of the array.</summary>
        /// <param name="projection">The function to transform the array elements into the type to be summed.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The resulting sum.</returns>
        [<Extension>]
        static member inline sumBy(array: 'T[], projection) = Array.sumBy projection array

        /// <summary>Splits an array of pairs into two arrays.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>The two arrays.</returns>
        [<Extension>]
        static member inline unzip(array) = Array.unzip array

        /// <summary>Splits an array of triples into three arrays.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>The tuple of three arrays.</returns>
        [<Extension>]
        static member inline unzip3(array) = Array.unzip3 array
                                    
    type ``[]``<'T> with
        /// <summary>Builds a new array that contains the elements of the first array followed by the elements of the second array.</summary>
        /// <param name="array1">The first input array.</param>
        /// <param name="array2">The second input array.</param>
        /// <returns>The resulting array.</returns>
        member inline array.append array2 = Array.append array array2

        /// <summary>For each element of the array, applies the given function. Concatenates all the results and return the combined array.</summary>
        /// <param name="mapping">The function to create sub-arrays from the input array elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The concatenation of the sub-arrays.</returns>
        member inline array.collect mapping = Array.collect mapping array

        /// <summary>Builds a new array that contains the elements of each of the given sequence of arrays.</summary>
        /// <param name="arrays">The input sequence of arrays.</param>
        /// <returns>The concatenation of the sequence of input arrays.</returns>
        member inline array.concat(arrays) = Array.concat (Array.append [| array |] arrays)

        /// <summary>Builds a new array that contains the elements of the given array.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>A copy of the input array.</returns>
        member inline array.copy() = Array.copy array

        /// <summary>Applies the given function to successive elements, returning the first
        /// result where function returns <c>Some(x)</c> for some <c>x</c>. If the function 
        /// never returns <c>Some(x)</c> then <c>None</c> is returned.</summary>
        /// <param name="chooser">The function to transform the array elements into options.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The first transformed element that is <c>Some(x)</c>.</returns>
        member inline array.tryPick chooser = Array.tryPick chooser array

        /// <summary>Applies the given function to successive elements, returning the first
        /// result where function returns <c>Some(x)</c> for some <c>x</c>. If the function 
        /// never returns <c>Some(x)</c> then <c>KeyNotFoundException</c> is raised.</summary>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if every result from
        /// <c>chooser</c> is <c>None</c>.</exception>
        /// <returns>The first result.</returns>
        member inline array.pick chooser = Array.pick chooser array

        /// <summary>Applies the given function to each element of the array. Returns
        /// the array comprised of the results "x" for each element where
        /// the function returns Some(x)</summary>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The array of results.</returns>
        member inline array.choose chooser = Array.choose chooser array

        /// <summary>Tests if any element of the array satisfies the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input array. If any application 
        /// returns true then the overall result is true and no further elements are tested. 
        /// Otherwise, false is returned.</remarks>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>True if any result from <c>predicate</c> is true.</returns>
        member inline array.exists predicate = Array.exists predicate array

        /// <summary>Returns a new collection containing only the elements of the collection
        /// for which the given predicate returns "true".</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>An array containing the elements for which the given predicate returns true.</returns>
        member inline array.filter predicate = Array.filter predicate array

        /// <summary>Returns the first element for which the given function returns 'true'.
        /// Raise <c>KeyNotFoundException</c> if no such element exists.</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if <c>predicate</c>
        /// never returns true.</exception>
        /// <returns>The first element for which <c>predicate</c> returns true.</returns>
        member inline array.find predicate = Array.find predicate array

        /// <summary>Returns the index of the first element in the array
        /// that satisfies the given predicate. Raise <c>KeyNotFoundException</c> if 
        /// none of the elements satisy the predicate.</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if <c>predicate</c>
        /// never returns true.</exception>
        /// <returns>The index of the first element in the array that satisfies the given predicate.</returns>
        member inline array.findIndex predicate = Array.findIndex predicate array

        /// <summary>Tests if all elements of the array satisfy the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input collection. If any application 
        /// returns false then the overall result is false and no further elements are tested. 
        /// Otherwise, true is returned.</remarks>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>True if all of the array elements satisfy the predicate.</returns>
        member inline array.forall predicate = Array.forall predicate array


        /// <summary>Applies a function to each element of the collection, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then computes 
        /// <c>f (... (f s i0)...) iN</c></summary>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The final state.</returns>
        member inline array.fold(state,folder) = Array.fold folder state array

        /// <summary>Applies a function to each element of the array, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then computes 
        /// <c>f i0 (...(f iN s))</c></summary>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The final state.</returns>
        member inline array.foldBack(folder,state) = Array.fold folder array state 

        /// <summary>Returns true if the given array is empty, otherwise false.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>True if the array is empty.</returns>
        member inline array.IsEmpty = Array.isEmpty array

        /// <summary>Applies the given function to each element of the array.</summary>
        /// <param name="action">The function to apply.</param>
        /// <param name="array">The input array.</param>
        member inline array.iter action = Array.iter action array

        /// <summary>Applies the given function to each element of the array. The integer passed to the
        /// function indicates the index of element.</summary>
        /// <param name="action">The function to apply to each index and element.</param>
        /// <param name="array">The input array.</param>
        member inline array.iteri action = Array.iteri action array

        /// <summary>Returns the length of an array. You can also use property arr.Length.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>The length of the array.</returns>
        member inline array.length = Array.length array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array.</summary>
        /// <param name="mapping">The function to transform elements of the array.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The array of transformed elements.</returns>
        member inline array.map mapping = Array.map mapping array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array. The integer index passed to the
        /// function indicates the index of element being transformed.</summary>
        /// <param name="mapping">The function to transform elements and their indices.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The array of transformed elements.</returns>
        member inline array.mapi mapping = Array.mapi mapping array

(*
        /// <summary>Builds an array from the given list.</summary>
        /// <param name="list">The input list.</param>
        /// <returns>The array of elements from the list.</returns>
        member inline array.ofList : list:'T list -> 'T[]

        /// <summary>Builds a new array from the given enumerable object.</summary>
        /// <param name="source">The input sequence.</param>
        /// <returns>The array of elements from the sequence.</returns>
        member inline array.ofSeq: source:seq<'T> -> 'T[]
*)

        /// <summary>Splits the collection into two collections, containing the 
        /// elements for which the given predicate returns "true" and "false"
        /// respectively.</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>A pair of arrays. The first containing the elements the predicate evaluated to true,
        /// and the second containing those evaluated to false.</returns>
        member inline array.partition predicate = Array.partition predicate array

        /// <summary>Returns an array with all elements permuted according to the
        /// specified permutation.</summary>
        /// <param name="indexMap">The function that maps input indices to output indices.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The output array.</returns>
        member inline array.permute indexMap = Array.permute indexMap array

        /// <summary>Applies a function to each element of the array, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> 
        /// then computes <c>f (... (f i0 i1)...) iN</c>.
        /// Raises ArgumentException if the array has size zero.</summary>
        /// <param name="reduction">The function to reduce a pair of elements to a single element.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        /// <returns>The final result of the redcutions.</returns>
        member inline array.reduce reduction = Array.reduce reduction array

        /// <summary>Applies a function to each element of the array, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> 
        /// then computes <c>f i0 (...(f iN-1 iN))</c>.
        /// Raises ArgumentException if the array has size zero.</summary>
        /// <param name="reduction">The function to reduce a pair of elements to a single element.</param>
        /// <param name="array">The input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        /// <returns>The final result of the reductions.</returns>
        member inline array.reduceBack reduction = Array.reduceBack reduction array

        /// <summary>Returns a new array with the elements in reverse order.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>The reversed array.</returns>
        member inline array.reverse() = Array.rev array

        /// <summary>Like <c>fold</c>, but return the intermediary and final results.</summary>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The array of state values.</returns>
        member inline array.scan (state, folder) = Array.scan folder state array

        /// <summary>Like <c>foldBack</c>, but return both the intermediary and final results.</summary>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The array of state values.</returns>
        member inline array.scanBack (folder, state) = Array.scanBack folder array state

        /// <summary>Builds a list from the given array.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>The list of array elements.</returns>
        member inline array.toList() = Array.toList array

        /// <summary>Views the given array as a sequence.</summary>
        /// <param name="array">The input array.</param>
        /// <returns>The sequence of array elements.</returns>
        member inline array.toSeq() = Array.toSeq array

        /// <summary>Returns the first element for which the given function returns <c>true</c>.
        /// Return <c>None</c> if no such element exists.</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The first element that satisfies the predicate, or None.</returns>
        member inline array.tryFind predicate = Array.tryFind predicate array

        /// <summary>Returns the index of the first element in the array
        /// that satisfies the given predicate.</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="array">The input array.</param>
        /// <returns>The index of the first element that satisfies the predicate, or None.</returns>
        member inline array.tryFindIndex predicate = Array.tryFindIndex predicate array


        /// <summary>Combines the two arrays into an array of pairs. The two arrays must have equal lengths, otherwise an <c>ArgumentException</c> is
        /// raised.</summary>
        /// <param name="array1">The first input array.</param>
        /// <param name="array2">The second input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input arrays differ in length.</exception>
        /// <returns>The array of tupled elements.</returns>
        member inline array.zip array2 = Array.zip array array2

        /// <summary>Combines three arrays into an array of pairs. The three arrays must have equal lengths, otherwise an <c>ArgumentException</c> is
        /// raised.</summary>
        /// <param name="array1">The first input array.</param>
        /// <param name="array2">The second input array.</param>
        /// <param name="array3">The third input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input arrays differ in length.</exception>
        /// <returns>The array of tupled elements.</returns>
        member inline array.zip3(array2, array3) = Array.zip3 array array2 array3

(*
#if FX_NO_TPL_PARALLEL
#else
        /// <summary>Provides parallel operations on arrays </summary>
        module Parallel =

            /// <summary>Apply the given function to each element of the array. Return
            /// the array comprised of the results "x" for each element where
            /// the function returns Some(x).</summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to elements of the input array is not specified.</remarks>
            /// <param name="chooser">The function to generate options from the elements.</param>
            /// <param name="array">The input array.</param>
            /// <returns>'U[]</returns>
            [<CompiledName("Choose")>]
            member choose: chooser:('T -> 'U option) -> array:'T[] -> 'U[]

            /// <summary>For each element of the array, apply the given function. Concatenate all the results and return the combined array.</summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to elements of the input array is not specified.</remarks>
            /// <param name="mapping"></param>
            /// <param name="array">The input array.</param>
            /// <returns>'U[]</returns>
            [<CompiledName("Collect")>]
            member collect : mapping:('T -> 'U[]) -> array:'T[] -> 'U[]
            
            /// <summary>Build a new array whose elements are the results of applying the given function
            /// to each of the elements of the array.</summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to elements of the input array is not specified.</remarks>
            /// <param name="mapping"></param>
            /// <param name="array">The input array.</param>
            /// <returns>'U[]</returns>
            [<CompiledName("Map")>]
            member map : mapping:('T -> 'U) -> array:'T[] -> 'U[]
            
            /// <summary>Build a new array whose elements are the results of applying the given function
            /// to each of the elements of the array. The integer index passed to the
            /// function indicates the index of element being transformed.</summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to elements of the input array is not specified.</remarks>
            /// <param name="mapping"></param>
            /// <param name="array">The input array.</param>
            /// <returns>'U[]</returns>
            [<CompiledName("MapIndexed")>]
            member mapi: mapping:(int -> 'T -> 'U) -> array:'T[] -> 'U[]

            /// <summary>Apply the given function to each element of the array. </summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to elements of the input array is not specified.</remarks>
            /// <param name="action"></param>
            /// <param name="array">The input array.</param>
            [<CompiledName("Iterate")>]
            member iter : action:('T -> unit) -> array:'T[] -> unit

            /// <summary>Apply the given function to each element of the array. The integer passed to the
            /// function indicates the index of element.</summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to elements of the input array is not specified.</remarks>
            /// <param name="action"></param>
            /// <param name="array">The input array.</param>
            [<CompiledName("IterateIndexed")>]
            member iteri: action:(int -> 'T -> unit) -> array:'T[] -> unit
            
            /// <summary>Create an array given the dimension and a generator function to compute the elements.</summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to indicies is not specified.</remarks>
            /// <param name="count"></param>
            /// <param name="initializer"></param>
            /// <returns>'T[]</returns>
            [<CompiledName("Initialize")>]
            member init : count:int -> initializer:(int -> 'T) -> 'T[]
            
            /// <summary>Split the collection into two collections, containing the 
            /// elements for which the given predicate returns "true" and "false"
            /// respectively </summary>
            ///
            /// <remarks>Performs the operation in parallel using System.Threading.Parallel.For.
            /// The order in which the given function is applied to indicies is not specified.</remarks>
            /// <param name="predicate">The function to test the input elements.</param>
            /// <param name="array">The input array.</param>
            /// <returns>'T[] * 'T[]</returns>
            [<CompiledName("Partition")>]
            member partition : predicate:('T -> bool) -> array:'T[] -> 'T[] * 'T[]
*)


[<AutoOpen>]
module Test = 
    let arr1 = [| 10 .. 20 |]

    arr1.pick(fun n -> if n % 2 = 0 then Some n else None)
    arr1.pick(fun n -> if n % 2 = 0 then Some n else None)
    arr1.map (fun n -> n + 1)
    arr1.collect (fun n -> [| n; n + 1 |])
    
    let arr2,arr3 = arr1.zip(arr1).unzip()
    let arr2b,arr3b,arr4b = arr1.zip3(arr1,arr2).unzip3()

    arr1.toList()
    arr1.toSeq()
    arr1.tryFind(fun x -> x % 2 = 0)
    arr1.find(fun x -> x % 2 = 0)
    arr1.choose(fun x -> if x % 2 = 0 then Some x else None)

    arr1.reduce(+)
    arr1.reduce(fun x y -> x * y % 29)
    arr1.fold (3, fun z x -> x + z)

    arr1.sum()
    arr1.sumBy(fun x -> x + 1)






