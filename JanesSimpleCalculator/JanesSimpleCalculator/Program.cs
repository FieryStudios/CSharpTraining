using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanesSimpleCalculator
{
    class Program
    {
        enum MyOperators { A, S, M, D };
       

        static void Main(string[] args)
        {                          
            Console.WriteLine("This is a calculator. Enter two numbers to calculate, then choose your operator.");

            double firstNumber = validateEntry("Please enter your first number: ");
            double secondNumber = validateEntry("Please enter your second number: ");

            Console.WriteLine("Choose your operator: ");
            Console.Write("Enter one of the following letters: (A)dd (S)ubtract (M)ultiply (D)ivide :");
            string mathFunction = Console.ReadKey().KeyChar.ToString().ToUpper();
            Console.WriteLine(" ");

            while (!Enum.IsDefined(typeof(MyOperators), mathFunction))
            {
                Console.WriteLine(" ");
                Console.WriteLine("That is not a valid entry.");
                Console.Write("Please re-enter one of the following letters: A(+) S(-) M(*) D(/) : ");
                mathFunction = Console.ReadKey().KeyChar.ToString().ToUpper();
                continue;
            } 
            Console.WriteLine(" ");

            switch (mathFunction) {
                case "A" :
                    Console.WriteLine("You have chosen to ADD your numbers.");
                    Console.WriteLine(firstNumber);
                    Console.WriteLine("+ " + secondNumber);
                    Console.WriteLine("________________");
                    Console.WriteLine(firstNumber + secondNumber);
                    break;
                case "S":
                    Console.WriteLine("You have chosen to SUBTRACT your numbers.");
                    Console.WriteLine(firstNumber);
                    Console.WriteLine("- " + secondNumber);
                    Console.WriteLine("________________");
                    Console.WriteLine(firstNumber - secondNumber);
                    break;
                case "M":
                    Console.WriteLine("You have chosen to MULTIPLY your numbers.");
                    Console.WriteLine(firstNumber);
                    Console.WriteLine("* " + secondNumber);
                    Console.WriteLine("________________");
                    Console.WriteLine(firstNumber * secondNumber);
                    break;
                case "D":
                    Console.WriteLine("You have chosen to DIVIDE your numbers.");
                    divider(firstNumber, secondNumber);
                    break;

                default:
                    Console.WriteLine("Invalid Operation. You broke it.");
                    break;
            }

            Console.ReadKey();
        }

        private static double validateEntry(string phrase)
        {
            double result;

            Console.Write(phrase);
            string inputVal = Console.ReadLine();

            while (true)
            {
                if (Double.TryParse(inputVal, out result))
                {
                    Console.WriteLine(" ");
                    return result;
                }
                else {
                    Console.WriteLine("That is not a valid entry.");
                    Console.Write("Re-enter this number using only digits: ");
                    inputVal = Console.ReadLine();
                }                
            };         
        }

        private static void divider(double firstNumber, double secondNumber) {
            if ((firstNumber == 0) || (secondNumber == 0))
            {
                Console.WriteLine("You can't divide by zero.  You'll break the calculator.");
            }
            else {
                Console.WriteLine(firstNumber);
                Console.WriteLine("/ " + secondNumber);
                Console.WriteLine("________________");
                Console.WriteLine(firstNumber / secondNumber);
            }
            
        }
    }
}

