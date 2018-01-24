using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanesDelegatesRefactored
{
   
    // this is the delegate
    public delegate void CityDelegate(decimal price, ref decimal costs);

    //list of all available locations:
    enum Cities { OklahomaCity, Lansing, Wichita, Denver, Miami };


    //TODO: set up base class to more closely match example
    //    set up delegate override functions for each city
    // refactor outer code to more closely match example


    // This is the base location class
    class City
    {

        public bool hasHazardFee = false;
        public string hazardDesc = "None";
        public decimal surcharge = 0m;

        public City() { }
                   
        // default shipping calculation 
        // This default method is what we're delegating.
        // (Actually, the individual cities' overrides get delegated.)
        // Notice that it matches the format of the CityDelegate, above.

        public virtual void shippingCosts(decimal price, ref decimal baseCost) {
            decimal tax = .10m;
            baseCost = Decimal.Round(price + (price * tax), 2);
        }

        // TODO: FIX THIS

        //Location switcher
        public static City getCity(Cities chosenCity) {
            switch (chosenCity)
            {
                case Cities.OklahomaCity:
                    return new OklahomaCity();

                case Cities.Lansing:
                    return new Lansing();

                case Cities.Wichita:
                    return new Wichita();

                case Cities.Denver:
                    return new Denver();

                case Cities.Miami:
                    return new Miami();

                default:
                    return new City();
            }

        }

    }
        // CITY DELEGATES 
        class OklahomaCity : City
        {
            public OklahomaCity() {
                this.hasHazardFee = true;
                this.hazardDesc = "Tornadoes";
                this.surcharge = 25.00m;
            }

            // this overrides the default shippingCosts method.
            // this is the method we're delegating.
            public override void shippingCosts(decimal price, ref decimal baseCost)
            {
                decimal tax = .25m;
                baseCost = Decimal.Round(price + (price * tax), 2);
            }
        }


        class Lansing : City
        {
            public Lansing()
            {
                this.hasHazardFee = true;
                this.hazardDesc = "Republicans";
                this.surcharge = 125.00m;
            }
            public override void shippingCosts(decimal price, ref decimal baseCost)
            {
                decimal tax = .12m;
                baseCost = Decimal.Round(price + (price * tax), 2);
            }
        }
        class Wichita : City
        {
            // this city uses the City defaults
            public Wichita()
            {
            }
        }
    class Denver : City
        {
            public Denver()
            {
                this.hasHazardFee = true;
                this.hazardDesc = "Avalanches";
                this.surcharge = 5.00m;
            }
            public override void shippingCosts(decimal price, ref decimal baseCost)
            { 
                decimal tax = .08m;
                baseCost = Decimal.Round(price + (price * tax), 2);
            }
        }

        class Miami : City
        {
            public Miami()
            {
                this.hasHazardFee = true;
                this.hazardDesc = "Hurricanes";
                this.surcharge = 15.00m;
            }
            public override void shippingCosts(decimal price, ref decimal baseCost)
            {
                decimal tax = .04m;
                baseCost = Decimal.Round(price + (price * tax), 2);
            }
        }

}

