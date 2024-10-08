﻿using System.Data;

namespace oop;

public delegate int MathOperation (int x, int y);

class Program
{
    static void Main(string[] args)
    {
        // ==== class ====
        // class in C# could have this member:
        // fields, constants, methods, properties, indexers, events, operators, constructors, destructors
        // member scopes:
        // public, private, protected, internal
        // member can be static or specific to a given object

        // using class
        // we're using Car class that have been created
        string carColor, carBrand;

        // first car
        Console.Write("Enter first Car color: ");
        carColor = Console.ReadLine();
        Console.Write("Enter first Car brand: ");
        carBrand = Console.ReadLine();

        // using Car constructor to set color and brand
        var firstCar = new Car(carColor, carBrand);

        // second car
        // instatiate new Car object first
        var secondCar = new Car(carColor, carBrand);

        Console.Write("Enter second Car color: ");
        carColor = Console.ReadLine();
        Console.Write("Enter second Car brand: ");
        carBrand = Console.ReadLine();

        // using properties to set color and brand
        secondCar.Color = carColor;
        secondCar.Brand = carBrand;

        // third car
        var thirdCar = new Car();

        Console.Write("Enter third Car color: ");
        carColor = Console.ReadLine();
        Console.WriteLine("Oops! you cannot input third car brand");

        thirdCar.Color = carColor;

        // append all created Car to a list
        List<Car> cars = [firstCar, secondCar, thirdCar];

        // print all cars and drive them
        for (int i = 0; i < (cars.Count); i++)
        {
            cars[i].Drive();
        }

        var profile = new UserProfile()
        {
            FirstName = "Steve",
            LastName = "Balmer",
            UserId = 91112
        };

        Console.WriteLine(profile.FirstName);

        profile.UserInfo();

        Console.WriteLine(Mathmath.SumAll(1, 2, 3, 4, 5));

        // interface implementation
        var triangle = new Triangle(5, 12);

        Console.WriteLine("Triangle surcafe area: {0}", triangle.CalculateSurface());

        // abstract implementation
        var circle = new MovableCircle(10);

        Console.WriteLine("Circle surface area: {0}", circle.CalculateSurface());

        // polymorphism implementation
        Person[] persons = { new Person(), new Trainer(), new Student() };

        foreach (Person p in persons)
        {
            p.PrintName();
        }

        // polymorphism implementation: overloading
        var calc = new Calculator();
        int a = 2, b = 3, c = 4;
        double x = 2.7, y = 5.2;
        var arr = new[] { 12, 343, 5, 467, 5 };

        Console.WriteLine(calc.Add(a, b));
        Console.WriteLine(calc.Add(a, b, c));
        Console.WriteLine(calc.Add(x, y));
        Console.WriteLine(calc.Add(arr));

        // delegate implementation
        static int Add(int a, int b) { return a + b; }
        static int Multiply(int a, int b) { return a * b; }

        MathOperation operation = Add;
        Console.WriteLine(operation(5,3));

        operation = Multiply;
        Console.WriteLine(operation(5,3));

    }
}

// class example
// Car class
/*public class Car
{
    // Car has color and brand field
    private string color;
    private string brand;

    // if the Car has no color or brand, make it a Red Mazda
    public Car()
    {
        this.color = "Red";
        this.brand = "Mazda";
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
}*/