using FactoryPatternDemo.Concrete;
using FactoryPatternDemo.Interfaces;

namespace FactoryPatternDemo.Factories
{
    public class VehicleFactory
    {
        public IVehicle CreateVehicle(string type)
        {
            switch (type.ToLower())
            {
                case "car":
                    return new Car();
                case "bike":
                    return new Bike();
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
        }
    }
}