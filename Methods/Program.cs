using System.Linq;
using System.Numerics;

namespace Methods
{
    internal class Program
    {
        static int IterPartSum(int[] a, int n)
        {
            int soucet = 0;

            for (int i = 0; i <= n; i++)
                soucet += a[i];

            return soucet;
        }

        static int RecursPartSum(int[] a, int i)
        {
            if (i == 0)
                return a[i];
            else
                return a[i] + RecursPartSum(a, i - 1);
        }

        // iterační součet řaday Sum(1/(n*n))
        static double IterRadaSum(int n)
        {
            double soucet = 0.0;

            for (int i = 1; i <= n; i++)
                soucet += 1.0 / (i * i);

            return soucet;
        }

        static double RekurzRadaSum(int n)
        {
            if (n == 1)
                return 1;
            else
                return 1.0 / (n * n) + RekurzRadaSum(n - 1);
        }

        // iterační součet řaday Sum(1/(n*n)) s omezením přesnosti
        static decimal IterRadaSumLimit(int n = 1, decimal eps = 1e-5m)
        {
            decimal clen = 1.0m, soucet = 0.0m;

            for (int i = n; clen > eps; i++)
            {
                clen = 1.0m / (i * i);
                if (clen > eps)
                    soucet += clen;
            }
            return soucet;
        }

        static decimal RekurzRadaSumLimit(int n = 1, decimal eps = 1e-5m)
        {
            decimal clen = 1.0m / (n * n);

            if (clen > eps)
                return clen + RekurzRadaSumLimit(n + 1, eps);
            else
                return 0.0m;

        }

        // generické metody
        static T GetMax<T>(T x, T y) where T : IComparable<T>
        {
            if (x.CompareTo(y) < 0) 
                return y; 
            
            return x;
        }

        static T GetSum<T>(T[] sumElements, int fromIndex, int toIndex) where T: INumber<T>
        {
            if (fromIndex > toIndex)
                throw new ArgumentException($"fromIndex: {fromIndex}, toIndex: {toIndex}");

            if(fromIndex < toIndex)
            {
                return sumElements[fromIndex] + GetSum(sumElements, fromIndex + 1, toIndex);
            }
            else 
            { 
                return sumElements[fromIndex]; 
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GetMax(2, 3));
            Console.WriteLine(GetMax("abc", "def"));
            Console.WriteLine(GetMax(6.28, 3.2));
            Console.WriteLine(GetMax(6.3f, 12.5f));
            Console.WriteLine(GetMax(10.20m, 11.60m));

            int[] ints = { 1, 2, 3, 4, 5, };
            double[] doubles = { 10, 20, 30, 40, 50, 60, };

            Console.WriteLine(GetSum(ints, 0, ints.Length-1));
            Console.WriteLine(GetSum(doubles, 1, 1));
            Console.WriteLine(GetSum(doubles, 1, 3));
        }
    }
}