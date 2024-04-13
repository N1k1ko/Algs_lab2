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

            int[] maxP = [0, 0, 0];// +100
            int[] minN = [0, 0, 0];// -100
            int[] maxN = [0, 0, 0];// -1

            Console.WriteLine("Ai:");
            FillNumbers(N, maxP, minN, maxN);

            Stopwatch stopWatch = new();

            stopWatch.Start();
            long result = FindMaxTriple(maxP, minN, maxN);
            stopWatch.Stop();

            Console.WriteLine($"Time: {stopWatch.Elapsed.Milliseconds}ms");

            return result;
        }

        static void FillNumbers(int N, int[] maxP, int[] minN, int[] maxN)
        {
            for (int i = 0; i<3;i++)
            {
                int a = int.Parse(Console.ReadLine());
                maxP[i] = a;
                minN[i] = a;
                maxN[i] = a;
            }

            for (int i = 3; i < N; i++)
            {
                int a = int.Parse(Console.ReadLine());

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

        static int MinElemIndex(int[] mas)
        {
            if (mas[0] <= mas[1] && mas[0] <= mas[2])
                return 0;
            if (mas[1] <= mas[2] && mas[1] <= mas[0])
                return 1;
            return 2;
        }

        static int MaxElemIndex(int[] mas)
        {
            if (mas[0] >= mas[1] && mas[0] >= mas[2])
                return 0;
            if (mas[1] >= mas[2] && mas[1] >= mas[0])
                return 1;
            return 2;
        }

        static long FindMaxTriple(int[] maxP, int[] minN, int[] maxN)
        {
            long result = int.MinValue;
            long mN = int.MinValue;

            long m1 = minN[0] * minN[1];
            long m2 = minN[1] * minN[2];
            long m3 = minN[0] * minN[2];

            if (m1 >= m2 && m1 >= m3)
                mN = m1;
            if (m2 >= m1 && m2 >= m3)
                mN = m2;
            if (m3 >= m1 && m3 >= m2)
                mN = m3;

            long[] tmp = [maxP[0] * maxP[1] * maxP[2],
                        maxN[0] * maxN[1] * maxN[2],
                        mN*maxP[0],
                        mN*maxP[1],
                        mN*maxP[2]];

            foreach (long i in tmp)
                if (i > result)
                    result = i;

            return result;
        }
    }
}