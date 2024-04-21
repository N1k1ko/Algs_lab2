using System.Diagnostics;

namespace Jarvis
{
    public class Point(int x, int y)
    {
        public readonly int x = x;
        public readonly int y = y;

        /// <summary>
        /// Определяет, с какой стороны от вектора AB находится точка.
        /// </summary>
        /// <returns>Положительное значение соответствует левой стороне, отрицательное — правой.</returns>
        public int Rotate(Point A, Point B) => (B.x - A.x) * (this.y - B.y) - (B.y - A.y) * (this.x - B.x);
        ///<summary>Пифагор</summary>
        public double Length(Point B) => Math.Sqrt((this.x - B.x) * (this.x - B.x) + (this.y - B.y) * (this.y - B.y));
    }

    class Jarvis
    {
        static void Main()
        {
            List<Point> input =
                [
                ];
            
            for (int i = -10000; i <=10000; i++)
            {
                input.Add(new Point(i, 10000));
                input.Add(new Point(i, -10000));
                input.Add(new Point(-10000, i));
                input.Add(new Point(10000, i));
            }
            for (int i = 0; i < 4; i++)
                input.Remove(input[0]);

            Console.WriteLine(input.Count);/*
            foreach (var e in input)
                Console.WriteLine($"{e.x} {e.y}");*/
            
            /*
            List<Point> input = [];
            int N = int.Parse(Console.ReadLine());
            for (int i=0; i<N; i++)
            {
                var tmp = Console.ReadLine().Split();
                input.Add(new Point(int.Parse(tmp[0]), int.Parse(tmp[1])));
            }
            */
            Stopwatch stopWatch = new();

            stopWatch.Start();
            double result = GetResult(input);
            stopWatch.Stop();

            Console.WriteLine($"Time: {stopWatch.Elapsed.Milliseconds}ms");
            Console.WriteLine($"Result: {result:f2}");
        }
        static double GetResult(List<Point> A)
        {
            int n = A.Count;
            List<int> P = [];
            for (int i = 0; i < n; i++)
                P.Add(i);

            for (int i = 0; i < n; i++)
                if (A[P[i]].x < A[P[0]].x)
                    (P[i], P[0]) = (P[0], P[i]);

            List<int> H = [P[0]];
            P.Remove(P[0]);
            P.Add(H[0]);

            while (true)
            {
                int right = 0;
                for (int i = 1; i<P.Count;i++)
                    if (A[P[i]].Rotate(A[H[^1]], A[P[right]]) < 0)
                        right = i;
                if (P[right] == H[0])
                    break;
                H.Add(P[right]);
                P.Remove(P[right]);
            }

            double result = A[H[0]].Length(A[H[^1]]);
            for (int i = 0; i < H.Count - 1; i++)
                result += A[H[i]].Length(A[H[i + 1]]);

            return result;
        }
    }
}