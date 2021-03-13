using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.CreationalPatterns
{
    // The Builder interface specifies methods for creating the different parts
    // of the Product objects.
    public interface ICarBuilder
    {
        void BuildTire();

        void BuildDoor();

        void BuildCarFrame();
        void BuildEngine();

        string GetProduct();
    }


    public class BaseCar
    {
        protected StringBuilder _product_SB;
        protected BaseCar()
        {
            _product_SB = new StringBuilder();
        }
        public void Reset()
        {
            _product_SB.Clear();
        }

        public string GetProduct()
        {
            string result = _product_SB.ToString();
            Reset();
            return result;
        }
    }

    // The Concrete Builder classes follow the Builder interface and provide
    // specific implementations of the building steps. Your program may have
    // several variations of Builders, implemented differently.
    public class PorcheBuilder : BaseCar , ICarBuilder
    {
        // All production steps work with the same product instance.
        public void BuildTire()
        {
            _product_SB.Append("Porche's Tire\n");
        }

        public void BuildDoor()
        {
            _product_SB.Append("Porche's CarDoor\n");
        }

        public void BuildCarFrame()
        {
            _product_SB.Append("Porche's fancy Car Frame\n");
        }

        public void BuildEngine()
        {
            _product_SB.Append("Porche's Super Powerful Engine\n");
        }
    }

    public class AudiBuilder : BaseCar, ICarBuilder
    {
        // All production steps work with the same product instance.
        public void BuildTire()
        {
            _product_SB.Append("Audi's Tire\n");
        }

        public void BuildDoor()
        {
            _product_SB.Append("Audi's CarDoor\n");
        }

        public void BuildCarFrame()
        {
            _product_SB.Append("Audi's stylish Car Frame\n");
        }

        public void BuildEngine()
        {
            _product_SB.Append("Audi's robust Engine\n");
        }
    }


    // The Director is only responsible for executing the building steps in a
    // particular sequence. It is helpful when producing products according to a
    // specific order or configuration. Strictly speaking, the Director class is
    // optional, since the client can control builders directly.
    public class CarDirector
    {
        private ICarBuilder _builder;

        public CarDirector(ICarBuilder carbuilder)
        {
            SetBuilder(carbuilder);
        }

        public void SetBuilder(ICarBuilder carbuilder)
        {
            _builder = carbuilder;
        }
        
        // The Director can construct several product variations using the same
        // building steps.
        public void BuildTire()
        {
            _builder.BuildTire();
        }

        public void BuildFullCar()
        {
            _builder.BuildTire();
            _builder.BuildDoor();
            _builder.BuildCarFrame();
            _builder.BuildEngine();
        }

        public string GetProduct()
        {
            return _builder.GetProduct();
        }
    }

    class BuilderClient
    {
        static public void Test()
        {
            Console.WriteLine("--use Audibuilder--");
            var director = new CarDirector(new PorcheBuilder());
            director.BuildFullCar();
            Console.Write(director.GetProduct());

            Console.WriteLine("--use Audibuilder--");
            director.SetBuilder(new AudiBuilder());
            director.BuildFullCar();
            Console.Write(director.GetProduct());

        }
    }

}
