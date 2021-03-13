using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternExercise.BehavioralPatterns
{
    // The Handler interface declares a method for building the chain of
    // handlers. It also declares a method for executing a request.
    class Context
    {
        // A reference to the current state of the Context.
        private State _state = null;

        public Context(State state)
        {
            this.TransitionTo(state);
        }

        // The Context allows changing the State object at runtime.
        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // The Context delegates part of its behavior to the current State
        // object.
        public void Handle1()
        {
            _state.Handle1();
        }

        public void Handle2()
        {
            _state.Handle2();
        }

        public void ChangeState()
        {
            _state.ChangeState();
        }
    }

    // The base State class declares methods that all Concrete State should
    // implement and also provides a backreference to the Context object,
    // associated with the State. This backreference can be used by States to
    // transition the Context to another State.
    abstract class State
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            this._context = context;
        }

        public abstract void Handle1();

        public abstract void Handle2();

        public abstract void ChangeState();
    }

    // Concrete States implement various behaviors, associated with a state of
    // the Context.
    class ConcreteStateA : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA handles request1.");

        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA handles request2.");
        }

        public override void ChangeState()
        {
            Console.WriteLine("ConcreteStateA wants to change the state of the context.");
            _context.TransitionTo(new ConcreteStateB());
        }
    }

    class ConcreteStateB : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateB handles request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB handles request2.");

        }
        public override void ChangeState()
        {
            Console.WriteLine("ConcreteStateB wants to change the state of the context.");
            _context.TransitionTo(new ConcreteStateA());
        }
    }

    class StateClient
    {
        public static void Test()
        {
            // The client code.
            var context = new Context(new ConcreteStateA());
            context.Handle1();
            context.Handle2();

            Console.WriteLine();
            context.ChangeState();
            context.Handle1();
            context.Handle2();

            Console.WriteLine();
            context.ChangeState();
            context.Handle1();
            context.Handle2();

        }
    }
}