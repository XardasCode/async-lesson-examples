namespace ConsoleApp4
{
    class PoolUsingClass
    {
        static void Main(string[] args)
        {
            //BadAsync();
            //GoodAsync();
            //LockAsync();
            //StaticLockAsync();

            //Deadlock deadlock = new Deadlock();
            //deadlock.Run();

            //RunMutex runMutex = new RunMutex();
            //runMutex.Run();
            //runMutex.RunNamedMutex();

            //RunSemaphore runSemaphore = new RunSemaphore();
            //runSemaphore.Run();

            ThreadPoolRun threadPool = new ThreadPoolRun();
            threadPool.Run();

            Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
            Console.ReadLine();
        }

        private static void LockAsync()
        {
            Console.WriteLine("Синхронізація блокування:");

            LockCounter lockAsync = new LockCounter();

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(lockAsync.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", lockAsync.Field1, lockAsync.Field2);
        }

        private static void StaticLockAsync()
        {
            Console.WriteLine("Синхронізація блокування:");

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(StaticLockCounter.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", StaticLockCounter.Field1, StaticLockCounter.Field2);
        }

        private static void GoodAsync()
        {
            Console.WriteLine("Синхронізація блокування:");
            MonitorLockCounter c = new MonitorLockCounter();

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", c.Field1, c.Field2);
        }

        private static void BadAsync()
        {
            Console.WriteLine("Синхронізація Interlocked-методами:");
            InterlockedCounter c = new InterlockedCounter();

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", c.Field1, c.Field2);
        }
    }
    class LockCounter
    {
        int field1;
        int field2;

        public int Field1
        {
            get { return field1; }
        }

        public int Field2
        {
            get { return field2; }
        }

        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                lock (this)
                {
                    ++field1;
                    if (field1 % 2 == 0)
                        ++field2;
                }
            }
        }
    }


    static class StaticLockCounter
    {
        static int field1;
        static int field2;

        public static int Field1
        {
            get { return field1; }
        }

        public static int Field2
        {
            get { return field2; }
        }

        public static void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                lock (typeof(StaticLockCounter))
                {
                    ++field1;
                    if (field1 % 2 == 0)
                        ++field2;
                }
            }
        }
    }

    class MonitorLockCounter
    {
        int field1;
        int field2;

        public int Field1 { get { return field1; } }
        public int Field2 { get { return field2; } }
        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                Monitor.Enter(this);
                try
                {
                    ++field1;
                    if (field1 % 2 == 0)
                        ++field2;
                }
                finally
                {
                    Monitor.Exit(this);
                }
            }
        }
    }

    class InterlockedCounter
    {
        int field1;
        int field2;

        public int Field1
        {
            get { return field1; }
        }

        public int Field2
        {
            get { return field2; }
        }

        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                Interlocked.Increment(ref field1);
                if (field1 % 2 == 0)
                    Interlocked.Increment(ref field2);
            }
        }
    }
}