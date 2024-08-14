using FactoryPatternDemo.Interfaces;

namespace FactoryPatternDemo.Concrete
{
    public class Bike : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("Bike is driving!");
        }
    }
}