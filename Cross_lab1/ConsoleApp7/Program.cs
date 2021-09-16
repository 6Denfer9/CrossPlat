using System;
using System.IO;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            StreamReader sr = new StreamReader("C:/Users/denta/OneDrive/Desktop/CrossPlat/CrossPlat/Cross_lab1/input.txt");
            line = sr.ReadLine();
            string[] arr = line.Split(" ");
            int n = Convert.ToInt32(arr[2]);
            int blue = Convert.ToInt32(arr[0]);
            int red = Convert.ToInt32(arr[1]);
            int result = (blue + red) * (blue + red) * n;
            Console.WriteLine(result);
            sr.Close();
            Console.ReadKey();
        }
    }
}
