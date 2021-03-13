using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.StructuralPatterns
{
    public class Person
    {
        public int Age;
        public DateTime BirthDate;
        public string Name;
        public IdInfo IdInfo;

        public Person Clone()
        {
            Person clone = new Person();
            clone.Age = Age;
            clone.BirthDate = BirthDate;
            clone.Name = Name;
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);

            return clone;
        }
    }

    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            this.IdNumber = idNumber;
        }
    }

    class PrototypeClient
    {
        public static void Test()
        {
            Person p1 = new Person();
            p1.Age = 42;
            p1.BirthDate = Convert.ToDateTime("1977-01-01");
            p1.Name = "Jack Daniels";
            p1.IdInfo = new IdInfo(666);

            Person p3 = p1.Clone();

            // Display values of p1, p2 and p3.
            Console.WriteLine("Original values of p1, p2");
            Console.WriteLine("--p1 instance values--");
            DisplayValues(p1);
            Console.WriteLine("--p3 instance values--");
            DisplayValues(p3);

            // Change the value of p1 properties and display the values of p1,
            // p2 and p3.
            p1.Age = 32;
            p1.BirthDate = Convert.ToDateTime("1900-01-01");
            p1.Name = "Frank";
            p1.IdInfo.IdNumber = 7878;
            Console.WriteLine("\nValues of p1, and p3 after changes to p1:");
            Console.WriteLine("--p1 instance values--");
            DisplayValues(p1);
            Console.WriteLine("--p3 instance values (everything was kept the same)--");
            DisplayValues(p3);
        }

        public static void DisplayValues(Person p)
        {
            Console.WriteLine("Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}", p.Name, p.Age, p.BirthDate);
            Console.WriteLine("ID#: {0:d}", p.IdInfo.IdNumber);
        }
    }
}
