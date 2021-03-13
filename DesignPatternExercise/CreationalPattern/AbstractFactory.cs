using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.CreationalPatterns
{
    public interface IAbstractFactory
    {
        IChair CreateIChair();

        ITable CreateITable();
    }


    class ModernFurnitureFactory : IAbstractFactory
    {
        public IChair CreateIChair()
        {
            return new ModernChair();
        }

        public ITable CreateITable()
        {
            return new ModernTable();
        }
    }

    class ClassicFurnitureFactory : IAbstractFactory
    {
        public IChair CreateIChair()
        {
            return new ClassicChair();
        }

        public ITable CreateITable()
        {
            return new ClassicTable();
        }
    }


    public interface IChair
    {
        string GetChairInfo();
    }

    class ModernChair : IChair
    {
        public string GetChairInfo()
        {
            return "modern chair";
        }
    }

    class ClassicChair : IChair
    {
        public string GetChairInfo()
        {
            return "classic chair";
        }
    }


    public abstract class ITable
    {
        abstract protected string GetTableInfo();

        public void ShowInfomationWithChair(IChair chair)
        {
            Console.WriteLine(GetTableInfo() + " with " + chair.GetChairInfo());
        }
    }

    class ModernTable : ITable
    {
        protected override string GetTableInfo()
        {
            return "Modern Table";
        }
    }

    class ClassicTable : ITable
    {
        protected override string GetTableInfo()
        {
            return "Classic Table";
        }
    }


    class AbstractFactoryClient
    {
        public static void Test()
        {
            // The client code can work with any concrete factory class.
            Console.WriteLine("Client: Testing client code with the modern furniture factory type...");
            ClientMethod(new ModernFurnitureFactory());
            Console.WriteLine();
            Console.WriteLine("Client: Testing client code with the modern classic factory type...");
            ClientMethod(new ClassicFurnitureFactory());
        }

        public static void ClientMethod(IAbstractFactory factory)
        {
            var chair = factory.CreateIChair();
            var table = factory.CreateITable();
            table.ShowInfomationWithChair(chair);
        }
    }

}
       
    