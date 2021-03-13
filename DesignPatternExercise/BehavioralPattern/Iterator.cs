using System;
using System.Collections;
using System.Collections.Generic;


namespace DesignPatternExercise.BehavioralPatterns
{
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        // Returns the key of the current element
        public abstract int Key();

        // Returns the current element
        public abstract object Current();

        // Move forward to next element(IEnumerator)
        public abstract bool MoveNext();

        // Rewinds the Iterator to the first element()
        public abstract void Reset();
    }




    // Concrete Iterators implement various traversal algorithms. These classes
    // store the current traversal position at all times.
    class AlphabeticalOrderIterator : Iterator
    {
        private WordsCollection wordscollection;

        // Stores the current traversal position. An iterator may have a lot of
        // other fields for storing iteration state, especially when it is
        // supposed to work with a particular kind of collection.
        private int position = -1;
        private bool IsReverse = false;

        public AlphabeticalOrderIterator(WordsCollection _wordscollection, bool reverse = false)
        {
            this.wordscollection = _wordscollection;
            this.IsReverse = reverse;
            if (reverse) this.position = _wordscollection.getItems().Count;
        }

        public override object Current()
        {
            return this.wordscollection.getItems()[position];
        }

        public override int Key()
        {
            return this.position;
        }

        public override bool MoveNext()
        {
            int updatedPosition = this.position + (this.IsReverse ? -1 : 1);

            if (updatedPosition >= 0 && updatedPosition < this.wordscollection.getItems().Count)
            {
                this.position = updatedPosition;
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            position = IsReverse ? this.wordscollection.getItems().Count - 1 : 0;
        }
    }

    // Concrete Collections provide one or several methods for retrieving fresh
    // iterator instances, compatible with the collection class.
    class WordsCollection : IEnumerable
    {
        List<string> stringList = new List<string>();

        bool IsReverse = false;

        public void ReverseDirection()
        {
            IsReverse = !IsReverse;
        }

        public List<string> getItems()
        {
            return stringList;
        }

        public void AddItem(string item)
        {
            stringList.Add(item);
        }

        public IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, IsReverse);
        }
    }

    class IteratorClient
    {
        public static void Test()
        {
            // The client code may or may not know about the Concrete Iterator
            // or Collection classes, depending on the level of indirection you
            // want to keep in your program.
            WordsCollection wordcollection = new WordsCollection();
            wordcollection.AddItem("First");
            wordcollection.AddItem("Second");
            wordcollection.AddItem("Third");
            wordcollection.AddItem("4");
            wordcollection.AddItem("5");
            wordcollection.AddItem("6");

            Console.WriteLine("--Straight traversal--");
            foreach (var element in wordcollection)
                Console.WriteLine(element);
            
            Console.WriteLine("\n--Reverse traversal--");
            wordcollection.ReverseDirection();
            foreach (var element in wordcollection)
                Console.WriteLine(element);
        }
    }
}