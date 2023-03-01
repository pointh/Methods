using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Dynamic;

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
                return a[0];
            else
                return a[i] + RecursPartSum(a, i - 1);
            // sum = a[i] + (a[0] + a[1] + a[2] + ... + a[i-1]) =
            //       a[i] + a[i-1] + (a[0] + a[1] + a[2] + ... + a[i-2]) =
            //       ...
            //       a[i] + a[i-1] + a[i-2] + a[i-3] + ... + a[0]
        }

        // iterační součet prvních n členů řady Sum(1/(n*n))
        static double IterRadaSum(int pocetClenu)
        {
            double soucet = 0.0;

            for (int i = 1; i <= pocetClenu; i++)
                soucet += 1.0 / (i * i);

            return soucet;
        }

        // rekurzivní součet prvních n členů řady Sum(1/(n*n))
        static double RekurzRadaSum(int pocetClenu)
        {
            if (pocetClenu == 1)
                return 1.0 / 1.0 * 1.0;
            else
                return 1.0 / (pocetClenu * pocetClenu) + RekurzRadaSum(pocetClenu - 1);
        }

        // iterační součet řaday Sum(1/(n*n)) se zanedbáním členů < 1e-5
        static decimal IterRadaSumLimit(decimal minimalniClen = 1e-5m)
        {
            decimal clen = 1.0m, soucet = 0.0m;

            for (int i = 1; clen > minimalniClen; i++)
            {
                clen = 1.0m / (i * i);
                if (clen > minimalniClen)
                    soucet += clen;
            }
            return soucet;
        }

        // rekurzivní součet řaday Sum(1/(n*n)) se zanedbáním členů < 1e-5
        static decimal RekurzRadaSumLimit(int n = 1, decimal minimalniClen = 1e-5m)
        {
            decimal clen = 1.0m / (n * n);

            if (clen > minimalniClen)
                return clen + RekurzRadaSumLimit(n + 1, minimalniClen);
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

        static IEnumerable<int> CyclicIndex(int length, int start)
        {
            int i = start;
            while(true) 
            {
                if (i == length)
                    i = 0;
                yield return i++;  // vrať hodnotu, ale nekonči s metodou (= yield)
                                   // při dalším volání metody zde pokračuj 
                Console.WriteLine(i);
            }
        }

        static dynamic InitMe(string jmeno, int vek, double vyska )
        {
            // dynamic je datový typ, který se definuje "za chodu"
            dynamic man = new ExpandoObject();
            man.Name = jmeno;
            man.Age = vek;
            man.Height = vyska;
            man.WriteMe = $"Name: {man.Name}\tAge: {man.Age}\tHeight: {man.Height}";

            return man;
        }

        static void Main(string[] args)
        {
            // 1. generické metody
            Console.WriteLine("1");
            Console.WriteLine(GetMax(2, 3));
            Console.WriteLine(GetMax("abc", "def"));
            Console.WriteLine(GetMax(6.28, 3.2));
            Console.WriteLine(GetMax(6.3f, 12.5f));
            Console.WriteLine(GetMax(10.20m, 11.60m));

            int[] ints = { 1, 2, 3, 4, 5, };
            double[] doubles = { 10, 20, 30, 40, 50, 60, };
            Console.WriteLine(GetSum(ints, 0, ints.Length - 1));
            Console.WriteLine(GetSum(doubles, 1, 1));
            Console.WriteLine(GetSum(doubles, 1, 3));

            // 2. částečný součet pole
            Console.WriteLine("\n2");
            Console.WriteLine(IterPartSum(ints, 3));
            Console.WriteLine(RecursPartSum(ints, 3));

            // 3. součet řady
            Console.WriteLine("\n3");
            Console.WriteLine(RekurzRadaSum(pocetClenu:350));
            Console.WriteLine(IterRadaSum(pocetClenu:350));
            Console.WriteLine(RekurzRadaSumLimit(minimalniClen:1e-5m));
            Console.WriteLine(IterRadaSumLimit(minimalniClen:1e-5m));

            // 4. return yield - iterátor
            Console.WriteLine("\n4");
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Console.WriteLine("Stiskni 'q' pro ukončení cyklu:");
            foreach(var x in CyclicIndex(s.Length, 3))
            {
                Console.Write(s[x]);
                if(Console.ReadKey(true).KeyChar == 'q')
                    break;
            }

            // 5. typ dynamic
            Console.WriteLine("\n\n5");
            dynamic man = InitMe("Jan", 20, 202.5);
            Console.WriteLine(man.WriteMe);
        }
    }
}