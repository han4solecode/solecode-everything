using FactoryPatternDemo.Factories;

namespace FactoryPatternDemo;

class Program
{
    static void Main(string[] args)
    {
        var newCar = new VehicleFactory().CreateVehicle("car");
        var newBike = new VehicleFactory().CreateVehicle("bike");

        newCar.Drive();
        newBike.Drive();
    }
}
