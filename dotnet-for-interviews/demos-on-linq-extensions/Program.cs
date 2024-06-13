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
IEnumerable<object> convertIntoInt = [1, 2, 3, 4, 5];
// Using Cast it converts the entire list from object to int
//Note: we cannot use this Cast on multi type object values.

convertIntoInt.Cast<int>().Dump("Convert from Object to Int");
// Output:
/*
┌──────────────────────────┐
│ <CastIterator>d__68<int> │
├──────────────────────────┤
│ 1                        │
│ 2                        │
│ 3                        │
│ 4                        │
│ 5                        │
└──────────────────────────┘
 Convert from Object to Int
*/

// convertIntoInt.Cast<string>().Dump("Convert from Object to String");
// Output:

// [Failed to Render System.Collections.Generic.IEnumerable`1[System.String] - System.Linq.Enumerable +< CastIterator > 
// d__68`1[System.String]].Unable to cast object of type 'System.Int32' to type 'System.String'.

// Extention 12: Chunk
IEnumerable<int> splitThisListIntoChunks = [1, 2, 3, 4, 5, 6];
splitThisListIntoChunks.Chunk(3).Dump("Split the entire list into multiple arrays: list.Length/3(chunk value) size arrays.");
// Output:
/*
┌───────────────────────────┐
│ <ChunkIterator>d__70<int> │
├───────────────────────────┤
│ ┌───┬────────┐            │
│ │ # │ int[3] │            │
│ ├───┼────────┤            │
│ │ 0 │ 1      │            │
│ │ 1 │ 2      │            │
│ │ 2 │ 3      │            │
│ └───┴────────┘            │
│ ┌───┬────────┐            │
│ │ # │ int[3] │            │
│ ├───┼────────┤            │
│ │ 0 │ 4      │            │
│ │ 1 │ 5      │            │
│ │ 2 │ 6      │            │
│ └───┴────────┘            │
└───────────────────────────┘
 Split the entire list into
      multiple arrays:
 list.Length/3 size arrays.

 */

splitThisListIntoChunks.Chunk(3).SelectMany(x => x).Dump("Revert back to single array");
// Output:
/*
┌────────────────────────────────────────────────┐
│ SelectManySingleSelectorIterator<Int32[], int> │
├────────────────────────────────────────────────┤
│ 1                                              │
│ 2                                              │
│ 3                                              │
│ 4                                              │
│ 5                                              │
│ 6                                              │
└────────────────────────────────────────────────┘
           Revert back to single array
 */

IEnumerable<int> unevenOrder = [1, 2, 3, 4, 5];
unevenOrder.Chunk(3).Dump("UnevenOrder - Split the entire list into multiple arrays: list.Length/3 size arrays.");
unevenOrder.Chunk(1).Dump("UnevenOrder - Split the entire list into multiple arrays: list.Length/4 size arrays.");
// Output:
/*
┌───────────────────────────┐
│ <ChunkIterator>d__70<int> │
├───────────────────────────┤
│ ┌───┬────────┐            │
│ │ # │ int[3] │            │
│ ├───┼────────┤            │
│ │ 0 │ 1      │            │
│ │ 1 │ 2      │            │
│ │ 2 │ 3      │            │
│ └───┴────────┘            │
│ ┌───┬────────┐            │
│ │ # │ int[2] │            │
│ ├───┼────────┤            │
│ │ 0 │ 4      │            │
│ │ 1 │ 5      │            │
│ └───┴────────┘            │
└───────────────────────────┘
  UnevenOrder - Split the
 entire list into multiple
 arrays: list.Length/3 size

 */
#endregion

#region Extensions_Existence_Or_QuantityChecks_13_to_15
IEnumerable<int> immediationExcetionElements = [1, 2, 3, 4, 5, 6];

// Extension 13: Any => If condition matches with in collection then it gives true.
// Ex: x>2 then 3>2 => matches then it won't check 4,5,6
immediationExcetionElements.Any(x => x > 2).Dump("Any extension method");
// Output:
/*
┌───────┐
│ True  │
└───────┘
  Any
extension
 method
 */

// Extension 14: All => Condition matches with all values in the collection if any one is fail then it gives false
// Ex: x>2 then 1>2 => first itself failed so it won't tranverse
// Ex: x>0 then 1,2,3,4,5,6 => all values >0 so it gives true.
immediationExcetionElements.All(x => x > 2).Dump("All extension method");
// Output:
/*
┌───────┐
│ False │
└───────┘
  All
extension
 method
 */

// Extension 15: Contains => If specified element is matched in collection then it gives true
// Ex: 3 then [1,2,3] => 3 is available then true
// Ex: 10 then [1,2,3,4,5,6] => then it gives false
immediationExcetionElements.Contains(3).Dump("Contains extension method - Returns true");
//Output:
/*
┌───────┐
│ True  │
└───────┘
Contains
extension
method -
Returns
  true
 */
immediationExcetionElements.Contains(31).Dump("Contains extension method - Returns false");
// Output:
/*
┌───────┐
│ False │
└───────┘
Contains
extension
method -
Returns
  false
 */
#endregion

#region Extensions_SequenceManipulation_16_to_17
// Extension  16: Append() => Adds one value at end of the list
IEnumerable<int> sequenceCollections = [1, 2, 3, 4, 5];
sequenceCollections.Append(6).Dump("Append Extension - 6");
// Output:
/*
┌─────────────────────────────┐
│ AppendPrepend1Iterator<int> │
├─────────────────────────────┤
│ 1                           │
│ 2                           │
│ 3                           │
│ 4                           │
│ 5                           │
│ 6                           │
└─────────────────────────────┘
       Append Extension - 6
 */
// Extension 17: Prepend() => Adds one value at begining of the list
sequenceCollections.Prepend(0).Dump("Prepend Extension - 0");
// Output:
/*
 ┌─────────────────────────────┐
│ AppendPrepend1Iterator<int> │
├─────────────────────────────┤
│ 0                           │
│ 1                           │
│ 2                           │
│ 3                           │
│ 4                           │
│ 5                           │
└─────────────────────────────┘
       Prepend Extension - 0
 */


#endregion

#region Extensions_Aggregation_Methods_18_to_

#endregion

#region Differneces_Btw_IEnumerable_Vs_ICollection_Vs_IList
// Order for upgrade => IEnumerable => ICollection => IList => IQueryable

// Option 1: IEnumerable


IEnumerable<int> inMemoryNumbers = [1, 2, 3, 4];

// Problems:
/*
 * 1. They cannot add any new element inside collection.
 * 2. To access element there is no option to use index like numbers[0].
 */
foreach (var number in inMemoryNumbers)
{
    Console.WriteLine(number);
}

numbers.Dump("Using IEnumerable");

// Output:
/*
┌─────────────────────────┐
│ <> z__ReadOnlyArray<int> │
├─────────────────────────┤
│ 1                       │
│ 2                       │
│ 3                       │
│ 4                       │
└─────────────────────────┘
     Using IEnumerable
*/

// Option 2: ICollection
ICollection<int> collectionNumbers = [1, 2, 3, 4];

// Pros over IEnumerable
// Elements can add using 
collectionNumbers.Add(1);
collectionNumbers.Dump("Using ICollection - Added 1");

// Output: Added new element in last
/*
┌───────────┐
│ List<int> │
├───────────┤
│ 1         │
│ 2         │
│ 3         │
│ 4         │
│ 1         │
└───────────┘
   Using
ICollection -
   Added 1
 */
collectionNumbers.Remove(2);
collectionNumbers.Dump("Using ICollection - Removed 1");

// Output: Remove the first occurance in collection
/*
┌───────────┐
│ List<int> │
├───────────┤
│ 1         │
│ 3         │
│ 4         │
│ 1         │
└───────────┘
   Using
ICollection -
  Removed 1
 */

// Problems:
/*
 * 1. We can add elements in the collection
 * 2. To access element there is no option to use index => Error: collectionNumbers[2] this gives: Error CS0021	Cannot apply indexing with [] to an expression of type 'ICollection<int>'
 */

// Option 3: IList
IList<int> listNumbers = [1, 2, 3, 4];
listNumbers.Add(1);
listNumbers.Dump("Using IList - Added 1");
// Output: Added at last
/*
┌───────────┐
│ List<int> │
├───────────┤
│ 1         │
│ 2         │
│ 3         │
│ 4         │
│ 1         │
└───────────┘
Using IList -
   Added 1
 */
listNumbers.Remove(2);
listNumbers.Dump("Using IList - Remove 2");
// Output: Remove the first occurance item in the List 
/*
┌───────────┐
│ List<int> │
├───────────┤
│ 1         │
│ 3         │
│ 4         │
│ 1         │
└───────────┘
Using IList -
  Remove 2
 */

listNumbers[1].Dump("Accessing 1st position value");
// Output:
/*
┌───────┐
│ 3     │
└───────┘
Accessing
  1st
position
  value
 */

// On overall - If you want to perform accessing elements or adding a new or removing an existing element then use IList.
// If you want to read only then go with IEnumerble.
#endregion