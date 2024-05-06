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
}