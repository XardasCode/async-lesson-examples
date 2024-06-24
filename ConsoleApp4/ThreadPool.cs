namespace ConsoleApp4
{
    public class ThreadPoolRun
    {
        public void Run()
        {
            // Створюємо декілька завдань для виконання в пулі потоків
            for (int i = 0; i < 10; i++)
            {
                int taskNumber = i; // Локальна копія змінної для захоплення замиканням
                ThreadPool.QueueUserWorkItem(DoWork, taskNumber);
            }

            // Очікуємо завершення роботи всіх потоків
            Console.WriteLine("Main thread does some work, then waits.");
            Thread.Sleep(2000); // Імітація роботи головного потоку
            Console.WriteLine("Main thread exits.");
        }

        public void DoWork(object state)
        {
            int taskNumber = (int)state;
            Console.WriteLine($"Task {taskNumber} is starting on thread {Thread.CurrentThread.ManagedThreadId}.");
            Thread.Sleep(2000); // Імітація роботи
            Console.WriteLine($"Task {taskNumber} is completed on thread {Thread.CurrentThread.ManagedThreadId}.");
        }
    }
}
