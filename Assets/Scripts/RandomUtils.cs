using System.Collections.Generic;
using System.Linq;

public static class RandomUtils
{
    public static int[] GenerateRandomizedIntegerList(int size)
    {
        // Create an ordered list of integers
        int[] orderedList = Enumerable.Range(0, size).ToArray();

        // Shuffle the list using Fisher-Yates shuffle algorithm
        Shuffle(orderedList);

        return orderedList;
    }

    private static void Shuffle(int[] array)
    {
        var random = new System.Random();
        for (int i = array.Length - 1; i > 0; i--)
        {
            // Get a random index between 0 and current index (inclusive)
            int j = random.Next(i + 1);

            // Swap current element with the randomly chosen element
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
    
    public static void Shuffle<T>(List<T> list)
    {
        if (list is not { Count: > 1 }) return; // No shuffling needed for empty or single-item list

        var random = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            // Get a random index between 0 and current index (inclusive)
            int j = random.Next(i + 1);

            // Swap current element with the randomly chosen element
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    
    public static Queue<T> Shuffle<T>(Queue<T> queue)
    {
        if (queue == null || queue.Count <= 1) return queue; // No shuffling needed

        // Convert Queue to List
        var list = new List<T>(queue);

        // Shuffle the List
        Shuffle(list);

        // Convert List back to Queue (preserving order)
        var shuffledQueue = new Queue<T>();
        foreach (var item in list)
        {
            shuffledQueue.Enqueue(item);
        }

        return shuffledQueue;
    }
}