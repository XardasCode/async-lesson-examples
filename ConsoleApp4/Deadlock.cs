namespace ConsoleApp4
{
    public class Deadlock
    {
        private readonly object lock1 = new object();
        private readonly object lock2 = new object();

        public void Run()
        {
            Thread thread1 = new Thread(Thread1);
            Thread thread2 = new Thread(Thread2);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Completed");
        }

        public void Thread1()
        {
            lock (lock1)
            {
                Console.WriteLine("Thread1 locked lock1");
                Thread.Sleep(1000); // Imitating work by sleeping

                lock (lock2)
                {
                    Console.WriteLine("Thread1 locked lock2");
                }
            }
        }

        void Thread2()
        {
            lock (lock2)
            {
                Console.WriteLine("Thread2 locked lock2");
                Thread.Sleep(1000); // Imitating work by sleeping

                lock (lock1)
                {
                    Console.WriteLine("Thread2 locked lock1");
                }
            }
        }
    }
}
