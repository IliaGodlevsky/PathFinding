using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Pathfinding.App.Console.ValueInput
{
    internal sealed class TestInput : IInput<int>, IFilePathInput, IInput<TimeSpan>, IInput<string>, IInput<Answer>, IInput<(string, int)>, IInput<ConsoleKey>
    {
        private static readonly TimeSpan WaitTime = TimeSpan.FromSeconds(1);

        private readonly Queue<int> intsQueue = new();
        private readonly Queue<TimeSpan> timeQueue = new();
        private readonly Queue<string> stringsQueue = new();
        private readonly Queue<ConsoleKey> keysQueue = new();
        private readonly Queue<Answer> answerQueue = new();
        private readonly Queue<(string, int)> sendQueue = new();

        private readonly AutoResetEvent resetEvent = new(false);

        public TestInput()
        {
            intsQueue.Enqueue(1);
            intsQueue.Enqueue(1);
            intsQueue.Enqueue(35);
            intsQueue.Enqueue(25);
            intsQueue.Enqueue(2);
            intsQueue.Enqueue(0);
            intsQueue.Enqueue(3);
            intsQueue.Enqueue(1);
            intsQueue.Enqueue(1);
            intsQueue.Enqueue(6);
            intsQueue.Enqueue(2);
            intsQueue.Enqueue(1);
            intsQueue.Enqueue(1);
            keysQueue.Enqueue(ConsoleKey.Enter);
            for (int i = 0; i < 24; i++)
            {
                keysQueue.Enqueue(ConsoleKey.S);
            }
            keysQueue.Enqueue(ConsoleKey.A);
            keysQueue.Enqueue(ConsoleKey.Enter);
            keysQueue.Enqueue(ConsoleKey.Escape);
            intsQueue.Enqueue(3);
            intsQueue.Enqueue(1);
            intsQueue.Enqueue(1);
            intsQueue.Enqueue(1);
            keysQueue.Enqueue(ConsoleKey.Enter);
            keysQueue.Enqueue(ConsoleKey.Enter);
            intsQueue.Enqueue(9);
            intsQueue.Enqueue(8);
            intsQueue.Enqueue(7);
            answerQueue.Enqueue(Answer.Yes);
        }

        public int Input()
        {
            return GetValueAndShow(intsQueue);
        }

        TimeSpan IInput<TimeSpan>.Input()
        {
            return GetValueAndShow(timeQueue);
        }

        string IInput<string>.Input()
        {
            return GetValueAndShow(stringsQueue);
        }

        Answer IInput<Answer>.Input()
        {
            return GetValueAndShow(answerQueue);
        }

        (string, int) IInput<(string, int)>.Input()
        {
            return GetValueAndShow(sendQueue);
        }

        ConsoleKey IInput<ConsoleKey>.Input()
        {
            resetEvent.WaitOne(TimeSpan.FromMilliseconds(150));
            return keysQueue.Dequeue();
        }

        private T GetValue<T>(Queue<T> queue)
        {
            resetEvent.WaitOne(WaitTime);
            return queue.Dequeue();
        }

        private T GetValueAndShow<T>(Queue<T> queue)
        {
            var value = queue.Dequeue();
            Terminal.WriteLine(value);
            resetEvent.WaitOne(WaitTime);
            return value;
        }
    }
}
