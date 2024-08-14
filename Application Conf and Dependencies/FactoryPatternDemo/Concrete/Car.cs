using FactoryPatternDemo.Interfaces;

namespace FactoryPatternDemo.Concrete
{
    public class Car : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("Car is driving!");
        }
    }
}