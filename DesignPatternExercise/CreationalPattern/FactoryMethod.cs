using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.CreationalPatterns
{
    abstract class Creator
    {
        public abstract IProduct GetIProduct();

        public string SomeOperation()
        {
            return "Creator is calling GetIProduct().Operation() : " + GetIProduct().Operation();
        }
    }

    class ConcreteCreator1 : Creator
    {
        public override IProduct GetIProduct()
        {
            return new ConcreteProduct1();
        }
    }

    class ConcreteCreator2 : Creator
    {
        public override IProduct GetIProduct()
        {
            return new ConcreteProduct2();
        }
    }

    public interface IProduct
    {
        string Operation();
    }
    class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Operation() of ConcreteProduct1}";
        }
    }

    class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Operation() of ConcreteProduct2}";
        }
    }

    class FactoryMethodClient
    {
        public static void Test()
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new ConcreteCreator1());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new ConcreteCreator2());
        }

        public static void ClientCode(Creator creator)
        {
            Console.WriteLine(creator.SomeOperation());
        }
    }
}


