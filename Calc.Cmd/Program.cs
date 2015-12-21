using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Calc.Fx;

namespace Calc
{
    internal class Program
    { 
        private static void Main(string[] args)
        {
            Console.Title = @"Factorial demo";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please enter number:");
            Console.ResetColor();

            while (true)
            {
                Console.Write("n=");
                var s = Console.ReadLine();
                int i;

                if (!int.TryParse(s, out i))
                    break;
                 
                Console.ForegroundColor = ConsoleColor.Green;
                   
                var calculation = new FactCalculation(i);

                calculation.Calculate();
                Console.WriteLine("n!={0}({1} elapsed)", calculation.Result, calculation.Elapsed);
                
                Console.ResetColor();

            }
        }
    }
}
