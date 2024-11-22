using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1;

class Cells1
{
    static int[] cells;
    static int N, K;
    static double p;
    static Thread[] threads;
    static readonly object lockObject = new object(); // Об'єкт для блокування

    public static void Run(int _N, int _K, double _p, int time, int sleep, int sleep_sec)
    {
        // Зчитування параметрів
        N = _N; // Кількість клітинок
        K = _K; // Кількість атомів
        p = _p; // Ймовірність переходу

        // Ініціалізація клітинок
        cells = new int[N];
        cells[0] = K; // Усі атоми на початку в лівій клітинці

        PrintState(0);
        var stopwatch = Stopwatch.StartNew();

        // Створення потоків для кожного атома
        threads = new Thread[K];
        for (int i = 0; i < K; i++)
        {
            int atomIndex = i;
            threads[i] = new Thread(() => Simulate(atomIndex, sleep));
            threads[i].Start();
        }

        // Збір моментальних станів
        for (int t = 1; t < time; t++)
        {
            Thread.Sleep(sleep_sec);
            PrintState(t);
        }

        // Завершення потоків
        foreach (var thread in threads)
            thread.Interrupt();

        stopwatch.Stop();
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");

        // Перевірка кількості атомів
        int totalAtoms = 0;
        foreach (var count in cells) totalAtoms += count;
        
        Console.WriteLine($"Total number of atoms: {totalAtoms}");
    }

    static void Simulate(int atomIndex, int sleep)
    {
        Random rand = new Random(atomIndex);
        int position = 0;

        try
        {
            while (true)
            {
                Thread.Sleep(sleep);
                double m = rand.NextDouble();
                int newPosition = m > p ? position + 1 : position - 1;

                lock (lockObject)
                {
                    if (newPosition >= 0 && newPosition < N)
                    {
                        cells[position]--;
                        position = newPosition;
                        cells[position]++;
                    }
                }
            }
        }
        catch (ThreadInterruptedException) { }
    }

    static void PrintState(int time)
    {
        Console.WriteLine($"Time {time} s: [{string.Join(", ", cells)}]");
    }
}