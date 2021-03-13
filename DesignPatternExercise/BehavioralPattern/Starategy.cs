using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.BehavioralPatterns
{
    // The Context defines the interface of interest to clients.
    class StrategyTestClass
    {
        // The Context maintains a reference to one of the Strategy objects. The
        // Context does not know the concrete class of a strategy. It should
        // work with all strategies via the Strategy interface.
        private IStrategy _strategy;

        // Usually, the Context allows replacing a Strategy object at runtime.
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // The Context delegates some work to the Strategy object instead of
        // implementing multiple versions of the algorithm on its own.
        public void ConductStarategy(List<string> list)
        {
            List<string> new_list = _strategy.DoAlgorithm(list);

            string resultStr = string.Empty;
            foreach (var element in new_list)
                resultStr += element + " ";
            
            Console.WriteLine(resultStr);
        }
    }

    public interface IStrategy
    {
        List<string> DoAlgorithm(List<string> list);
    }

    class SortStrategy : IStrategy
    {
        public List<string> DoAlgorithm(List<string> list)
        {
            list.Sort();

            return new List<string>(list);
        }
    }

    class ReverseSortStrategy : IStrategy
    {
        public List<string> DoAlgorithm(List<string> list)
        {
            list.Sort();
            list.Reverse();

            return new List<string>(list);
        }
    }

    class StarategyClient
    {
        public static void Test()
        {
            // The client code picks a concrete strategy and passes it to the
            // context. The client should be aware of the differences between
            // strategies in order to make the right choice.
            var list = new List<string> { "c", "f", "a", "d", "e","w","b"};
            var StarategyTest = new StrategyTestClass();

            Console.WriteLine("Client: Strategy is set to normal sorting.");
            StarategyTest.SetStrategy(new SortStrategy());
            StarategyTest.ConductStarategy(list);
            Console.WriteLine();

            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            StarategyTest.SetStrategy(new ReverseSortStrategy());
            StarategyTest.ConductStarategy(list);
        }
    }


}