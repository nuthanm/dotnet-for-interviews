using Dumpify;

Console.WriteLine("This entire class you see all LINQ extension exmples");
Console.WriteLine("You can see all the results in similar LINQPAD Format called tabular mode using .Dump() method.");

#region Extensions_Filtering
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

#region Extensions_Partioning

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
