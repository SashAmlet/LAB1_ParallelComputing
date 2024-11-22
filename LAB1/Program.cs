namespace LAB1;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Зчитування параметрів
            int N = 10;     // Кількість клітинок
            int K = 20;     // Кількість атомів
            double p = 0.5; // Ймовірність переходу
            int time = 10;  // Час моделювання
            int sleep = 10;
            int sleep_sec = 1000;

            // Вибір версії програми
            Console.WriteLine("Select version: 0 (no locks) or 1 (with locks): ");
            int version = int.Parse(Console.ReadLine() ?? "1");

            if (version == 0)
            {
                Console.WriteLine("Running version without locks...");
                Cells0.Run(N, K, p, time, sleep, sleep_sec);
            }
            else if (version == 1)
            {
                Console.WriteLine("Running version with locks...");
                Cells1.Run(N, K, p, time, sleep, sleep_sec);
            }
            else
            {
                Console.WriteLine("Invalid version selected.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}