using System;
using System.Collections.Generic;
using System.Threading;

namespace Sharper.Other
{
    internal class ThreadManager
    {
        public List<Thread> threads = new List<Thread>();

        public ThreadManager()
        {
        }

        public void Add(ThreadStart function)
        {
            threads.Add(new Thread(function));
        }

        public void StartAll()
        {
            threads.ForEach(x => x.Start());
        }
    }
}