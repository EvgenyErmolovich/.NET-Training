<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{
	int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    var intArrayIterator = array.CustomIterator<int[], int, int>(
                                    0,
                                    (arr, cursor) => arr[cursor],
                                    (arr, cursor) => cursor > arr.Length - 1,
                                    cursor => cursor + 1);
    foreach (var element in intArrayIterator)
        Console.Write(" " + element);

    var intArrayIterator2 = array.CustomIterator<int[], int, int>(
                                    0,
                                    (arr, cur) => arr[cur],
                                    (arr, cur) => cur > arr.Length - 1,
                                    cur => cur + 2);

    foreach (var element in intArrayIterator2)
        Console.WriteLine(element);

    var stringIterator = "Hello, world!"
                            .CustomIterator(0,
                            (col, cur) => col[cur] + "  ",
                            (col, cur) => cur > col.Length - 1,
                            cur => cur + 1);

    foreach (var symbol in stringIterator)
		Console.Write(symbol);
	
	Console.WriteLine();
    var strList = new List<string> {"one", "two", "three", "four"};
    var iter = strList.CustomTransformator(str => str.Length);
    foreach (var element in iter)
    {
        Console.WriteLine(element);
	}
}	

	public static class CustomExtentions
    {
        public static IEnumerable<TItem> CustomIterator<TCollection, TIndex, TItem>(
            this TCollection collection,
            TIndex index,
            Func<TCollection, TIndex, TItem> getItemRule,
            Func<TCollection, TIndex, bool> finishedRule,
            Func<TIndex, TIndex> indexChangeRule)
        {
            while (!finishedRule(collection, index))
            {
                yield return getItemRule(collection, index);
                index = indexChangeRule(index);
            }
        }

        public static IEnumerable<TResult> CustomTransformator<TSourse,TResult>(
				this IEnumerable<TSourse> collection, 
				Func<TSourse, TResult> transformer)
		{
            foreach (var term in collection)
                yield return transformer(term);
		}
    }