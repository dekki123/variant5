using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variant5Arrays
{
    internal class IoUtils
    {
        public const int MaxSize = 10;   // размер массива не превосходит 10x10

        /// <summary>Читает целое число в диапазоне [low, high], повторяя запрос при ошибке.</summary>
        public static int ReadInt(string prompt, int low, int high)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();
                if (!int.TryParse(s, out int value))
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                    continue;
                }
                if (value < low || value > high)
                {
                    Console.WriteLine($"Ошибка: число должно быть от {low} до {high}.");
                    continue;
                }
                return value;
            }
        }

        /// <summary>Читает вещественное число (допускается запятая или точка).</summary>
        private static bool TryParseDouble(string s, out double value)
        {
            return double.TryParse(s.Replace(',', '.'), NumberStyles.Float,
                                   CultureInfo.InvariantCulture, out value);
        }

        /// <summary>Вводит вещественный массив с клавиатуры с проверкой размеров.</summary>
        public static double[,] ReadMatrix(string name)
        {
            Console.WriteLine($"--- Ввод массива {name} ---");
            int n = ReadInt($"Число строк (1..{MaxSize}): ", 1, MaxSize);
            int m = ReadInt($"Число столбцов (1..{MaxSize}): ", 1, MaxSize);
            var matrix = new double[n, m];

            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    Console.Write($"Строка {i + 1} ({m} чисел через пробел): ");
                    string[] parts = (Console.ReadLine() ?? "")
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != m)
                    {
                        Console.WriteLine($"Ошибка: нужно ровно {m} чисел.");
                        continue;
                    }

                    var row = new double[m];
                    bool ok = true;
                    for (int j = 0; j < m; j++)
                    {
                        if (!TryParseDouble(parts[j], out row[j])) { ok = false; break; }
                    }
                    if (!ok)
                    {
                        Console.WriteLine("Ошибка: вводите только числа.");
                        continue;
                    }

                    for (int j = 0; j < m; j++) matrix[i, j] = row[j];
                    break;
                }
            }
            return matrix;
        }

        /// <summary>Печатает массив.</summary>
        public static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(matrix[i, j].ToString("F2", CultureInfo.InvariantCulture).PadLeft(9));
                Console.WriteLine();
            }
        }

        /// <summary>Печатает результат для одного массива.</summary>
        public static void PrintResult(string name, int count, List<int> columns)
        {
            if (count == 0)
                Console.WriteLine($"Массив {name}: столбцов только с неположительными элементами нет.");
            else
                Console.WriteLine($"Массив {name}: количество таких столбцов = {count} " +
                                  $"(номера: {string.Join(", ", columns)}).");
        }
    }
}
