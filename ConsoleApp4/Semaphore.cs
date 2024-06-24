using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class RunSemaphore
    {
        // Створюємо семафор, який дозволяє одночасно працювати 3 потокам
        private Semaphore semaphore = new Semaphore(3, 3);

        public void Run()
        {
            for (int i = 1; i <= 10; i++)
            {
                Thread thread = new Thread(DoWork);
                thread.Name = $"Thread {i}";
                thread.Start(i);
            }
        }

        void DoWork(object id)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting to enter the semaphore.");

            // Захоплення семафора
            semaphore.WaitOne();

            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} has entered the semaphore.");
                Thread.Sleep(2000); // Імітація роботи
            }
            finally
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is leaving the semaphore.");
                // Звільнення семафора
                semaphore.Release();
            }
        }
    }
}
