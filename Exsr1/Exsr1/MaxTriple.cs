using System.Diagnostics;

namespace MaxTriple
{
    public class MaxTriple
    {
        static void Main()
        {
            Console.WriteLine("N:");
            int N = int.Parse(Console.ReadLine());

            long result = GetResult(N);

            Console.WriteLine("Result: " + result);
        }

        static long GetResult(int N)
        {
            if (N < 3)
                throw new Exception("Need minimum 3 numbers");

            long[] maxP = [-1000001, -1000001, -1000001];// +100
            long[] minN = [0, 0, 0];// -100
            long[] maxN = [-1000001, -1000001, -1000001];// -1

            Console.WriteLine("Ai:");
            FillNumbers(N, maxP, minN, maxN);

            Stopwatch stopWatch = new();

            stopWatch.Start();
            long result = FindMaxTriple(maxP, minN, maxN);
            stopWatch.Stop();

            Console.WriteLine($"Time: {stopWatch.Elapsed.Milliseconds}ms");

            return result;
        }

        static void FillNumbers(int N, long[] maxP, long[] minN, long[] maxN)
        {
            //Random rand = new();
            for (int i = 0; i < N; i++)
            {
                int a = int.Parse(Console.ReadLine());
                //int a = rand.Next(-5, 6);

                if (a >= 0)
                {
                    int index = MinElemIndex(maxP);
                    if (a > maxP[index])
                        maxP[index] = a;
                }
                else
                {
                    int maxIndex = MaxElemIndex(minN);
                    int minIndex = MinElemIndex(maxN);

                    if (a > maxN[minIndex])
                        maxN[minIndex] = a;
                    if (a < minN[maxIndex])
                        minN[maxIndex] = a;
                }
            }

        }

        static int MinElemIndex(long[] mas)
        {
            int i = 2;
            if (mas[1] <= mas[i])
                i = 1;
            if (mas[0] <= mas[i])
                i = 0;
            return i;
        }

        static int MaxElemIndex(long[] mas)
        {
            int i = 2;
            if (mas[1] >= mas[i])
                i = 1;
            if (mas[0] >= mas[i])
                i = 0;
            return i;
        }

        static long FindMaxTriple(long[] maxP, long[] minN, long[] maxN)
        {
            //all negative; negative result
            if (maxP[0] == -1000001)
                return maxN[0] * maxN[1] * maxN[2];

            long result = -1;
            //two pos, other neg; negative result
            if (maxP[2] == -1000001 && maxP[1] != -1000001)
            {
                result = maxN[0] * maxN[1] * maxN[2];
                foreach (long i in maxN)
                    if (maxP[0] * maxP[1] * i > result)
                        result = maxP[0] * maxP[1] * i;
                return result;
            }

            if (maxP[1] != -1000001)
                result = maxP[0] * maxP[1] * maxP[2];

            //dnm, positive result
            long mN = minN[0] * minN[1];
            if (minN[1] * minN[2] >= mN)
                mN = minN[1] * minN[2];
            if (minN[0] * minN[2] >= mN)
                mN = minN[0] * minN[2];

            for (int i = 0; i < 3; i++)
                if (mN * maxP[i] > result)
                    result = mN * maxP[i];

            return result;
        }
    }
}