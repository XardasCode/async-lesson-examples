namespace ConsoleApp4
{

    public class RunMutex
    {
        Mutex mutex = new Mutex();

        public void RunNamedMutex()
        {
            // Іменований Mutex
            using (Mutex mutex = new Mutex(false, "Global\\MyMutex"))
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
                {
                    Console.WriteLine("Another instance of the application is running. Exiting...");
                    return;
                }

                Console.WriteLine("No other instances detected. Running...");
            }
        }

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
            Console.WriteLine("Thread1 waiting for the mutex");
            mutex.WaitOne(); // Заблокувати mutex
            Console.WriteLine("Thread1 has entered the protected area");

            Thread.Sleep(2000); // Імітація роботи

            Console.WriteLine("Thread1 is leaving the protected area");
            mutex.ReleaseMutex(); // Розблокувати mutex
        }

        public void Thread2()
        {
            Console.WriteLine("Thread2 waiting for the mutex");
            mutex.WaitOne(); // Заблокувати mutex
            Console.WriteLine("Thread2 has entered the protected area");

            Thread.Sleep(1000); // Імітація роботи

            Console.WriteLine("Thread2 is leaving the protected area");
            mutex.ReleaseMutex(); // Розблокувати mutex
        }
    }
}