using System;
using System.IO;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {           
            string lines;
            StreamReader sr = new StreamReader("C:/Users/denta/OneDrive/Desktop/CrossPlat/CrossPlat/Croos_lab2/input.txt");
            lines = sr.ReadLine();
            int N = Convert.ToInt32(lines);
            double n = Convert.ToDouble(lines);
            double k = n / 3;
            int K = N / 3;
            if (k == K)
            {
                double result = Math.Pow(2, Convert.ToDouble(K - 1));//test
                Console.WriteLine(result);
            }
            else
            {
                double result = Math.Pow(2, Convert.ToDouble(K));
                Console.WriteLine(result);
            }
            sr.Close();
            Console.ReadKey();
            
        }
    }
}
      