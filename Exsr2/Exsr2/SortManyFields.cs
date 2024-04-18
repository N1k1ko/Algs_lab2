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
            tmp = tmp.OrderByDescending(x => x.Key).ToDictionary();
            foreach (int key in tmp.Keys)
            {
                if (tmp[key].Count > 1 && i<reg.Length)
                    Smth(reg,tmp[key],i+1);
                else
                    foreach (string[] e in tmp[key])
                        Console.WriteLine(e[0]);
            }
        }
    }
}