namespace MaxTriple
{ 
    public class MaxTriple
    {
        static void Main()
        {
            Console.WriteLine("N:");
            int N = int.Parse(Console.ReadLine());

            int result = GetResult(N);

            Console.WriteLine($"result: {result}");
        }

        static int GetResult(int N)
        {
            if (N < 3)
                throw new Exception("Need minimum 3 numbers");

            int[] maxP = [int.MinValue, int.MinValue, int.MinValue];// +100
            int[] minN = [int.MaxValue, int.MaxValue, int.MaxValue];// -100
            int[] maxN = [int.MinValue, int.MinValue, int.MinValue];// -1

            Console.WriteLine("Ai:");
            FillNumbers(N, maxP, minN, maxN);

            if (maxP[0] == int.MinValue)
                return maxN[0] * maxN[1] * maxN[2];

            int mN = MaxMult(minN);
            if (maxP[1] == int.MinValue)
                return maxP[0] * mN;
            if (maxP[2] == int.MinValue)
                return maxP[0] * maxP[1] * maxN[0];

            int[] tmp = [maxP[0] * maxP[1] * maxP[2],
                        mN*maxP[0],
                        mN*maxP[1],
                        mN*maxP[2]];
            int result = tmp[0];
            foreach (int i in tmp)
                if (i > result)
                    result = i;

            return result;
        }

        static void FillNumbers(int N, int[] maxP, int[] minN, int[] maxN)
        {
            for (int i = 0; i < N; i++)
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
            if (mas[2] <= mas[0] && mas[2] <= mas[1])
                return 2;

            throw new Exception();
        }

        static int MaxElemIndex(int[] mas)
        {
            if (mas[0] >= mas[1] && mas[0] >= mas[2])
                return 0;
            if (mas[1] >= mas[2] && mas[1] >= mas[0])
                return 1;
            if (mas[2] >= mas[0] && mas[2] >= mas[1])
                return 2;

            throw new Exception();
        }

        static int MaxMult(int[] mas)
        {
            if (mas[0] == int.MaxValue)
                throw new Exception("No negative numbers");
            if (mas[1] == int.MaxValue)
                return mas[0];
            if (mas[2] == int.MaxValue)
                return mas[0] * mas[1];

            int m1 = mas[0] * mas[1];
            int m2 = mas[1] * mas[2];
            int m3 = mas[0] * mas[2];

            if (m1 >= m2 && m1 >= m3)
                return m1;
            if (m2 >= m1 && m2 >= m3)
                return m2;
            if (m3 >= m2 && m3 >= m1)
                return m3;

            throw new Exception();
        }
    }
}