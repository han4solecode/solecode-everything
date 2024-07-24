using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Car
    {
        private string color;
        private string brand;

        // if the Car has no color or brand, make it a Red Mazda
        public Car()
        {
            this.color = "Red";
            this.brand = "Ferrari";
        }

        // Car should be able to viewed and modified
        // Car constructor
        public Car(string color, string brand)
        {
            this.color = color;
            this.brand = brand;
        }

        // create a Color property
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        // create a Color property
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        // Car method, Car should be able to drive
        public void Drive(string color, string brand)
        {
            Console.WriteLine("{0} {1} is driving!", color, brand);
        }
    }
}
