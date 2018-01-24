using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanesDelegatesRefactored
{

    // declare the delegate type

    class Program
    {
        
        static void Main(string[] args)
        {
            CityDelegate newDelegate;
            City destinationCity;

            string cityStr;
            string priceStr;

            // make pretty list of available city names
            string cityList = string.Join(", ", Enum.GetNames(typeof(Cities))); 

            do
            {
                Console.Write($"Enter a city name\n({cityList}) or \"exit\": ");
                cityStr = Console.ReadLine();

                while (!System.Enum.IsDefined(typeof(Cities), cityStr) && cityStr != "exit")
                {
                    Console.Write("Please enter a valid city name (or \"exit\"): ");
                    cityStr = Console.ReadLine();

                }
                if (cityStr == "exit")
                {
                    break;
                }

                Cities chosenCity = (Cities)Enum.Parse(typeof(Cities), cityStr);

                Console.Write("Enter a price: ");
                priceStr = Console.ReadLine();
                decimal shippingPrice;

                while (!Decimal.TryParse(priceStr, out shippingPrice))
                {
                    Console.Write("Please enter a valid price: ");
                    priceStr = Console.ReadLine();
                };


                destinationCity = City.getCity(chosenCity);          

                // assign the delegate to the city's shipping method
                newDelegate = destinationCity.shippingCosts;

                if (destinationCity.hasHazardFee == true) {

                    // create a new anonymous delegate with the same signature to add the Surcharge
                    // += adds it to the existing delegate to increase the cost
                    // Composing the delegates in this way adds the surcharge to the final basecost
                    // via the reference variable, because if we added it to the baseCost var before calling newDelegate,
                    // then the surcharge would get taxed, which isn't what we want.
                    newDelegate += delegate(decimal price, ref decimal baseCost)
                    {
                        baseCost += destinationCity.surcharge;
                    };

                    Console.WriteLine($"This city has a Hazard Fee ({destinationCity.hazardDesc}) of ${destinationCity.surcharge}: ");

                }

                decimal finalCost = 0.0m;
                newDelegate(shippingPrice, ref finalCost);
                Console.WriteLine($"Total shipping cost: ${finalCost}");

                Console.WriteLine();


            } while (cityStr != "exit");


            Console.WriteLine("\nYou're finished shipping. \nPress Enter Key to Continue...");
            Console.ReadLine();
        } 
    }
}
