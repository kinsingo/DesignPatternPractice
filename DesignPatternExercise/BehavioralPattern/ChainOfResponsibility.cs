using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.BehavioralPatterns
{
    // The Handler interface declares a method for building the chain of
    // handlers. It also declares a method for executing a request.
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(string request);
    }

    // The default chaining behavior can be implemented inside a base handler
    // class.
    abstract class AbstractHandler : IHandler
    {
        private IHandler nextHandler;

        // Returning a handler from here will let us link handlers in a
        // convenient way like this:
        // monkey.SetNext(squirrel).SetNext(dog);
        public IHandler SetNext(IHandler _nexthandler)
        {
            nextHandler = _nexthandler;
            return nextHandler;
        }

        public virtual object Handle(string request)
        {
            return (nextHandler != null) ? nextHandler.Handle(request) : null;
        }
    }

    class MonkeyHandler : AbstractHandler
    {
        public override object Handle(string request)
        {
            return (request == "Banana") ? $"Monkey: I'll eat the {request.ToString()}.\n" : base.Handle(request);
        }
    }

    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(string request)
        {
            return (request == "Nut") ? $"Squirrel: I'll eat the {request.ToString()}.\n" : base.Handle(request);
        }
    }

    class DogHandler : AbstractHandler
    {
        public override object Handle(string request)
        {
            return (request == "MeatBall") ? $"Dog: I'll eat the {request.ToString()}.\n" : base.Handle(request);
        }
    }



    class ChainOfResponsibilityClient
    {
        public static void ClientCode(AbstractHandler handler, List<string> RequestList)
        {
            foreach (string food in RequestList)
            {
                Console.WriteLine($"Client: Who wants a {food}?");

                object result = handler.Handle(food);

                if (result != null)
                    Console.Write($"{result}");
                else
                    Console.WriteLine($"{food} was left untouched.");

            }
        }

        static public void Test()
        {
            List<string> RequestList = new List<string> { "Nut", "Banana", "Cup of coffee" };

            // The other part of the client code constructs the actual chain.
            MonkeyHandler monkey = new MonkeyHandler();
            SquirrelHandler squirrel = new SquirrelHandler();
            DogHandler dog = new DogHandler();

            monkey.SetNext(squirrel).SetNext(dog);

            // The client should be able to send a request to any handler, not
            // just the first one in the chain.
            Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
            ClientCode(monkey, RequestList);
            Console.WriteLine();

            Console.WriteLine("Subchain: Squirrel > Dog\n");
            ClientCode(squirrel, RequestList);
        }
    }
}
