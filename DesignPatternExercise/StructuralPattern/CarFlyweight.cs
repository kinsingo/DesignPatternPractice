using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.StructuralPatterns
{
    // The Flyweight stores a common portion of the state (also called intrinsic
    // state) that belongs to multiple real business entities. The Flyweight
    // accepts the rest of the state (extrinsic state, unique for each entity)
    // via its method parameters.
    public class CarFlyweight
    {
        private Car sharedState;

        public CarFlyweight(Car car)
        {
            this.sharedState = car;
        }

        public void ShowCarFlyWeightInfo()
        {
            Console.WriteLine("\nDisplaying SharedCarInfo : " + sharedState.GetCarInfo());
        }
    }

    // The Flyweight Factory creates and manages the Flyweight objects. It
    // ensures that flyweights are shared correctly. When the client requests a
    // flyweight, the factory either returns an existing instance or creates a
    // new one, if it doesn't exist yet.
    public class FlyweightFactory
    {
        private Dictionary<string,CarFlyweight> flyweight_dict = new Dictionary<string,CarFlyweight>();

        public FlyweightFactory(params Car[] cars)
        {
            foreach (var car in cars)
                AddCarToFlyWeighDictionary(car);
        }

        private void AddCarToFlyWeighDictionary(Car car)
        {
            string CarInfo = car.GetCarInfo();

            if (flyweight_dict.ContainsKey(CarInfo) == false)
            {
                Console.WriteLine("this car's flyweight doesn't exist, create new one");
                flyweight_dict.Add(CarInfo, new CarFlyweight(car));
            }
        }

        // Returns an existing Flyweight with a given state or creates a new one
        public CarFlyweight GetFlyweight(Car car)
        {
            AddCarToFlyWeighDictionary(car);
            return flyweight_dict[car.GetCarInfo()];
        }

        public void ShowExistringFlyWeightList()
        {
            Console.WriteLine($"\nFlyweightFactory: I have {flyweight_dict.Count} flyweights:");
            foreach (string key in flyweight_dict.Keys)
                Console.WriteLine(key);
        }
    }

    public class Car
    {
        public string Owner;
        public string Number;
        public string Company;
        public string Model;
        public string Color;

        public string GetCarInfo()
        {
            StringBuilder ans = new StringBuilder();
            if (Owner != string.Empty)
                ans.Append(" CarOwner : " + Owner);
            if (Model != string.Empty)
                ans.Append(" Model : " + Model);
            if (Number != string.Empty)
                ans.Append(" CarNum : " + Number);
            if (Color != string.Empty)
                ans.Append(" Color : " + Color);
            if (Company != string.Empty)
                ans.Append(" Made by : " + Company);

            return ans.ToString();
        }
        
    }

    class FlyWeightClinet
    {
        public static void Test()
        {
            Console.WriteLine("--this example assume that the Car object creation is very expensive--");
            // The client code usually creates a bunch of pre-populated
            // flyweights in the initialization stage of the application.

            Car[] cars = new Car[5]
            {
                new Car { Company = "Chevrolet", Model = "Camaro2018", Color = "pink" },
                new Car { Company = "Mercedes Benz", Model = "C300", Color = "black" },
                new Car { Company = "Mercedes Benz", Model = "C500", Color = "red" },
                new Car { Company = "BMW", Model = "M5", Color = "red" },
                new Car { Company = "BMW", Model = "X6", Color = "white" }
            };


            var factory = new FlyweightFactory(cars[0], cars[1]);
            factory.ShowExistringFlyWeightList();

            Console.WriteLine();
            factory.GetFlyweight(cars[0]).ShowCarFlyWeightInfo();
            factory.ShowExistringFlyWeightList();

            Console.WriteLine();
            factory.GetFlyweight(cars[3]).ShowCarFlyWeightInfo();
            factory.ShowExistringFlyWeightList();

            Console.WriteLine();
            factory.GetFlyweight(cars[1]).ShowCarFlyWeightInfo();
            factory.ShowExistringFlyWeightList();

            Console.WriteLine();
            factory.GetFlyweight(cars[4]).ShowCarFlyWeightInfo();
            factory.ShowExistringFlyWeightList();

            Console.WriteLine();
            for (int i = 0; i < 10; i++)
                factory.GetFlyweight(cars[2]).ShowCarFlyWeightInfo();
        }


    }

}
