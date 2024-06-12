using Dumpify;

Console.WriteLine("This entire class you see all LINQ extension exmples");
Console.WriteLine("You can see all the results in similar LINQPAD Format called tabular mode using .Dump() method.");

#region Extensions_Filtering_1_to_2
// Extension 1: Where
IEnumerable<int> numbers = [1, 2, 3, 4, 5];
numbers.Dump();
// Output
/*
┌─────────────────────────┐
│ <>z__ReadOnlyArray<int> │
├─────────────────────────┤
│ 1                       │
│ 2                       │
│ 3                       │
│ 4                       │
│ 5                       │
└─────────────────────────┘
 */

// Getting the results whose number is > 2
numbers.Where(x => x > 2).Dump();
/*
┌──────────────────────────────┐
│ WhereEnumerableIterator<int> │
├──────────────────────────────┤
│ 3                            │
│ 4                            │
│ 5                            │
└──────────────────────────────┘
 */

// Extension 2: OfType
IEnumerable<object> collectionOfTypeData = [1, "Two", 3, "Four"];

// Objective:
// 1. If only int values then use OfType<int> => 1,3
// 2. If only string values then use OfType<string> => "Two", "Four"

collectionOfTypeData.Dump(); // Full data
/*
 ┌────────────────────────────┐
│ <>z__ReadOnlyArray<object> │
├────────────────────────────┤
│ 1                          │
│ "Two"                      │
│ 3                          │
│ "Four"                     │
└────────────────────────────┘
 */

collectionOfTypeData.OfType<string>().Dump();
/*
┌───────────────────────────────┐
│ <OfTypeIterator>d__66<string> │
├───────────────────────────────┤
│ "Two"                         │
│ "Four"                        │
└───────────────────────────────┘
 */
collectionOfTypeData.OfType<int>().Dump();
/*
┌────────────────────────────┐
│ <OfTypeIterator>d__66<int> │
├────────────────────────────┤
│ 1                          │
│ 3                          │
└────────────────────────────┘
 */

#endregion

#region Extensions_Partioning_3_to_8

// Extension 3: Skip
IEnumerable<int> numberCollection = [1, 3, 4, 5];

// If you want only 4, 5 then use Skip(2) => This will skip first two elements in the collection
numberCollection.Skip(2).Dump();
// Output:
/*
 ┌────────────────────┐
│ ListPartition<int> │
├────────────────────┤
│ 4                  │
│ 5                  │
└────────────────────┘
 */

// Extension 4: Take
// If you want to take first 2 numbers from the collection then use Take(2).
numberCollection.Take(2).Dump();

// Output:

/*
┌────────────────────┐
│ ListPartition<int> │
├────────────────────┤
│ 1                  │
│ 3                  │
└────────────────────┘
 */

// Extension 5: SkipLast
numberCollection.SkipLast(3).Dump("SkipLast");
// This will skip last 3 elements and give other values from the collection.
// Output:
/*
┌───────────────────────────────────────┐
│ < TakeRangeFromEndIterator > d__281<int> │
├───────────────────────────────────────┤
│ 1                                     │
└───────────────────────────────────────┘
                SkipLast
*/

// Extension 6: TakeLast
numberCollection.TakeLast(3).Dump("TakeLast");
// This will take last 3 elements and skip other values.
// Output:
/*
┌───────────────────────────────────────┐
│ <TakeRangeFromEndIterator>d__281<int> │
├───────────────────────────────────────┤
│ 3                                     │
│ 4                                     │
│ 5                                     │
└───────────────────────────────────────┘
                TakeLast
*/

// Extension 7: SkipWhile
// This method iterates each element from collection based on predicate/condition matches it skips those element and remaining to print.
numberCollection.SkipWhile(x => x < 2).Dump("SkipWhile");
// Output:
/*

┌────────────────────────────────┐
│ <SkipWhileIterator>d__249<int> │
├────────────────────────────────┤
│ 3                              │
│ 4                              │
│ 5                              │
└────────────────────────────────┘
            SkipWhile
*/

// Extension 8: TakeWhile
// This method iterates each element from collection based on predicate/condition matches it takes those element and remaining skip.
numberCollection.TakeWhile(x => x < 3).Dump("TakeWhile");
/*
 ┌────────────────────────────────┐
│ <TakeWhileIterator>d__283<int> │
├────────────────────────────────┤
│ 1                              │
└────────────────────────────────┘
            TakeWhile
 */
#endregion

#region Extensions_Projection_9_to_12
// Extention 9: Select
IEnumerable<int> projectionCollection = [1, 2, 3, 4, 5];

projectionCollection.Select(x => x).Dump("Select");
// Returns IEnumerable<int>
//Output:
/*
┌───────────────────────────────┐
│ SelectIListIterator<int, int> │
├───────────────────────────────┤
│ 1                             │
│ 2                             │
│ 3                             │
│ 4                             │
│ 5                             │
└───────────────────────────────┘
             Select
*/
projectionCollection.Select(x => x.ToString()).Dump("Select- Numbers to String");
// Convert all into a string
// Output:

/*
┌──────────────────────────────────┐
│ SelectIListIterator<int, string> │
├──────────────────────────────────┤
│ "1"                              │
│ "2"                              │
│ "3"                              │
│ "4"                              │
│ "5"                              │
└──────────────────────────────────┘
     Select- Numbers to String
 */
projectionCollection.Select((x, i) => $"Index: {i} - Value: {x}").Dump("Select - Index and it's value in formatted");
// Fetch the value with index in formatted way

//Output:
/*
┌─────────────────────────────────────┐
│ <SelectIterator>d__229<int, string> │
├─────────────────────────────────────┤
│ "Index: 0 - Value: 1"               │
│ "Index: 1 - Value: 2"               │
│ "Index: 2 - Value: 3"               │
│ "Index: 3 - Value: 4"               │
│ "Index: 4 - Value: 5"               │
└─────────────────────────────────────┘
     Select - Index and it's value in formatted
*/

projectionCollection.Select((x, i) => (i, x)).Dump("Select - Index and it's value without formatting");
//Output:

/*
┌───────────────────────────────────────────────────┐
│ <SelectIterator>d__229<int, ValueTuple<int, int>> │
├───────────────────────────────────────────────────┤
│ ValueTuple<int,                                   │
│       int>                                        │
│ ┌───────┬───────┐                                 │
│ │ Item  │ Value │                                 │
│ ├───────┼───────┤                                 │
│ │ Item1 │ 0     │                                 │
│ │ Item2 │ 1     │                                 │
│ └───────┴───────┘                                 │
│ ValueTuple<int,                                   │
│       int>                                        │
│ ┌───────┬───────┐                                 │
│ │ Item  │ Value │                                 │
│ ├───────┼───────┤                                 │
│ │ Item1 │ 1     │                                 │
│ │ Item2 │ 2     │                                 │
│ └───────┴───────┘                                 │
│ ValueTuple<int,                                   │
│       int>                                        │
│ ┌───────┬───────┐                                 │
│ │ Item  │ Value │                                 │
│ ├───────┼───────┤                                 │
│ │ Item1 │ 2     │                                 │
│ │ Item2 │ 3     │                                 │
│ └───────┴───────┘                                 │
│ ValueTuple<int,                                   │
│       int>                                        │
│ ┌───────┬───────┐                                 │
│ │ Item  │ Value │                                 │
│ ├───────┼───────┤                                 │
│ │ Item1 │ 3     │                                 │
│ │ Item2 │ 4     │                                 │
│ └───────┴───────┘                                 │
│ ValueTuple<int,                                   │
│       int>                                        │
│ ┌───────┬───────┐                                 │
│ │ Item  │ Value │                                 │
│ ├───────┼───────┤                                 │
│ │ Item1 │ 4     │                                 │
│ │ Item2 │ 5     │                                 │
│ └───────┴───────┘                                 │
└───────────────────────────────────────────────────┘
  Select - Index and it's value without formatting
*/

// Extention 10: SelectMany
IEnumerable<List<int>> selectManyCollection = [[1, 2, 3], [4, 5, 6]];
selectManyCollection.Select(x => x).Dump("Select - On List of Integers arrays");

// Output
/*
┌───────────────────────────────────────────┐
│ SelectIListIterator<List<int>, List<int>> │
├───────────────────────────────────────────┤
│ ┌───────────┐                             │
│ │ List<int> │                             │
│ ├───────────┤                             │
│ │ 1         │                             │
│ │ 2         │                             │
│ │ 3         │                             │
│ └───────────┘                             │
│ ┌───────────┐                             │
│ │ List<int> │                             │
│ ├───────────┤                             │
│ │ 4         │                             │
│ │ 5         │                             │
│ │ 6         │                             │
│ └───────────┘                             │
└───────────────────────────────────────────┘
     Select - On List of Integers arrays
 */

selectManyCollection.Select((x, i) => $"Index: {i} and it's value: {x}").Dump("Select with Index- On List of Integers arrays");
// Output:

/*
┌────────────────────────────────────────────────────────────────────────────┐
│ < SelectIterator > d__229<List<int>, string>                                  │
├────────────────────────────────────────────────────────────────────────────┤
│ "Index: 0 and it's value: System.Collections.Generic.List`1[System.Int32]" │
│ "Index: 1 and it's value: System.Collections.Generic.List`1[System.Int32]" │
└────────────────────────────────────────────────────────────────────────────┘
                Select with Index- On List of Integers arrays
*/

// To get combined result
selectManyCollection.SelectMany(x => x).Dump("SelectMany Without Index");

// Combined both collection into one.

// Output:
/*
┌──────────────────────────────────────────────────┐
│ SelectManySingleSelectorIterator<List<int>, int> │
├──────────────────────────────────────────────────┤
│ 1                                                │
│ 2                                                │
│ 3                                                │
│ 4                                                │
│ 5                                                │
│ 6                                                │
└──────────────────────────────────────────────────┘
              SelectMany Without Index

*/

selectManyCollection.SelectMany(x => x.Select(x => x.ToString())).Dump("SelectMany to Select - Print values in strings");
// Output: Select each collection and convert each value from the collection and print.
/*
┌─────────────────────────────────────────────────────┐
│ SelectManySingleSelectorIterator<List<int>, string> │
├─────────────────────────────────────────────────────┤
│ "1"                                                 │
│ "2"                                                 │
│ "3"                                                 │
│ "4"                                                 │
│ "5"                                                 │
│ "6"                                                 │
└─────────────────────────────────────────────────────┘
    SelectMany to Select - Print values in strings
 */
// Extention 11: Cast

// Extention 12: Chunk
#endregion