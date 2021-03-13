using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.BehavioralPatterns
{
    // The Command interface declares a method for executing a command.
    public interface ICommand
    {
        void Execute();
    }


    public interface IReceiver
    {
        void DoSomething(string str);
        void DoSomethingElse(string str);
    }

    // Some commands can implement simple operations on their own.
    class SimpleCommand : ICommand
    {
        private string str = string.Empty;

        public SimpleCommand(string _str)
        {
            str = _str;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand:{str}");
        }
    }

    // However, some commands can delegate more complex operations to other
    // objects, called "receivers."
    class ComplexCommand : ICommand
    {
        private IReceiver receiver;

        // Context data, required for launching the receiver's methods.
        private string a;

        private string b;

        // Complex commands can accept one or several receiver objects along
        // with any context data via the constructor.
        public ComplexCommand(IReceiver _receiver, string _a, string _b)
        {
            receiver = _receiver;
            a = _a;
            b = _b;
        }

        // Commands can delegate to any methods of a receiver.
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            receiver.DoSomething(a);
            receiver.DoSomethingElse(b);
        }
    }

    // The Receiver classes contain some important business logic. They know how
    // to perform all kinds of operations, associated with carrying out a
    // request. In fact, any class may serve as a Receiver.
    class FinishReceiver : IReceiver
    {
        public void DoSomething(string str)
        {
            Console.WriteLine($"Receiver: As a last step do sth on {str}");
        }

        public void DoSomethingElse(string str)
        {
            Console.WriteLine($"Receiver:  a last step do sth else on {str}");
        }
    }

    class OnProgressReceiver : IReceiver
    {
        public void DoSomething(string str)
        {
            Console.WriteLine($"Receiver: processing sth on {str}");
        }

        public void DoSomethingElse(string str)
        {
            Console.WriteLine($"Receiver: processing sth else on {str}");
        }
    }



    // The Invoker is associated with one or several commands. It sends a
    // request to the command.
    class Invoker
    {
        private ICommand onStart;
        private ICommand onProcess;
        private ICommand onFinish;

        public Invoker(ICommand OnStartCommand, ICommand OnProcessCommand, ICommand OnFinishCommand)
        {
            onStart = OnStartCommand;
            onProcess = OnProcessCommand;
            onFinish = OnFinishCommand;
        }

        // The Invoker does not depend on concrete command or receiver classes.
        // The Invoker passes a request to a receiver indirectly, by executing a
        // command.
        public void InvokeAllCommands()
        {
            Console.WriteLine("\nInvoker: Start");
            onStart.Execute();

            Console.WriteLine("\nInvoker: On Progress");
            onProcess.Execute();

            Console.WriteLine("\nInvoker: End");
            onFinish.Execute();
        }
    }

    class CommandClient
    {
        public static void Test()
        {
            // The client code can parameterize an invoker with any commands.

            ICommand startcommand = new SimpleCommand("[initialization]");
            ICommand processcommand = new ComplexCommand(new OnProgressReceiver(),"[process1]", "[process2]");
            ICommand endcommend = new ComplexCommand(new FinishReceiver(), "[just before finish]", "[finish]");

            Invoker invoker = new Invoker(startcommand, processcommand, endcommend);
            invoker.InvokeAllCommands();
        }
    }


}
