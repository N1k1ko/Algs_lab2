using System.Diagnostics;

namespace SortManyFields
{
    public class SortManyFields
    {
        static void Main()
        {
            Stopwatch stopWatch = new();

            stopWatch.Start();
            GetResult();
            stopWatch.Stop();

            Console.WriteLine($"Time: {stopWatch.Elapsed.Milliseconds}ms");
        }

        static void GetResult()
        {
            int N = int.Parse(Console.ReadLine());
            int K = int.Parse(Console.ReadLine());
            string[] reg = [.. Console.ReadLine().Split()];
            if (reg.Length > K)
                reg = reg[..K];
            List<string[]> fields = [];

            for (int i = 0; i < N; i++)
                fields.Add([.. Console.ReadLine().Split()]);

            Smth(reg, fields, 0);
        }

        static void Smth(string[] reg, List<string[]> fields, int i)
        {
            Dictionary<int, List<string[]>> tmp = [];
            int index = int.Parse(reg[i]);
            foreach (string[] e in fields)
            {
                int key = int.Parse(e[index]);
                if (!(tmp.ContainsKey(key)))
                    tmp[key] = [];
                tmp[key].Add(e);
            }

            int[] keys = [.. tmp.Keys];
            foreach (int key in SortKeys(keys, 0, tmp.Count - 1))
            {
                if (tmp[key].Count > 1 && i < reg.Length)
                    Smth(reg, tmp[key], i + 1);
                else
                    foreach (string[] e in tmp[key])
                        Console.WriteLine(e[0]);
            }
            static int[] SortKeys(int[] array, int leftIndex, int rightIndex)
            {
                var i = leftIndex;
                var j = rightIndex;
                var pivot = array[leftIndex];

                while (i <= j)
                {
                    while (array[i] > pivot)
                        i++;

                    while (array[j] < pivot)
                        j--;

                    if (i <= j)
                    {
                        (array[j], array[i]) = (array[i], array[j]);
                        i++;
                        j--;
                    }
                }

                if (leftIndex < j)
                    SortKeys(array, leftIndex, j);

                if (i < rightIndex)
                    SortKeys(array, i, rightIndex);

                return array;
            }
        }
    }
}