namespace DotsAndCuts
{
    public class Cut
    {
        public readonly int begin;
        public readonly int end;
        public Cut(int begin, int end) { this.begin = begin; this.end = end; }
    }
    class DotsAndCuts
    {
        static void Main()
        {
            string dir = "..\\..\\..\\..\\";
            string fileName = "test1.txt";
            string[] f = File.ReadAllLines(dir + fileName);
            File.WriteAllText(dir + "result.txt", GetResult(f));
        }
        static string GetResult(string[] f)
        {
            int N = int.Parse(f[0].Split()[0]);
            int M = int.Parse(f[0].Split()[1]);
            Cut[] cuts = new Cut[N];
            int[] dots = new int[M];
            int[] counter = new int[M];

            for (int i = 1; i <= N; i++)
                cuts[i - 1] = new Cut(int.Parse(f[i].Split()[0]), int.Parse(f[i].Split()[1]));
            for (int i = 0; i < M; i++)
                dots[i] = int.Parse(f[^1].Split()[i]);

            cuts = SortCuts(cuts, 0, cuts.Length - 1);
            for (int j = 0; j < M; j++)
            {
                for (int i = FindIndex(cuts, dots[j]); i < N; i++)
                {
                    if (cuts[i].begin > dots[j])
                        break;
                    if (cuts[i].begin <= dots[j] && cuts[i].end >= dots[j])
                        counter[j]++;
                }
            }

            string result = counter[0].ToString();
            foreach (var e in counter[1..])
                result += " " + e.ToString();
            return result;
        }
        static int FindIndex(Cut[] cuts, int begin)
        {
            int middle = (cuts.Length - 1) / 2;
            if (middle - 1 <= 0)
                return 0;
            if (cuts[middle].begin < begin)
                return middle;
            return FindIndex(cuts[..middle], begin);
        }
        static Cut[] SortCuts(Cut[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex].begin;
            while (i <= j)
            {
                while (array[i].begin < pivot)
                    i++;
                while (array[j].begin > pivot)
                    j--;
                if (i <= j)
                {
                    (array[j], array[i]) = (array[i], array[j]);
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                SortCuts(array, leftIndex, j);
            if (i < rightIndex)
                SortCuts(array, i, rightIndex);
            return array;
        }
    }
}
