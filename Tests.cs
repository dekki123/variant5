// Модуль тестирования (Студент 2). Запуск: dotnet run -- --test
// Простой самописный набор тестов, не требует установки NuGet-пакетов.

namespace Variant5Arrays;

public static class Tests
{
    private static int passed, failed;

    private static void Check(string name, bool condition)
    {
        if (condition) { passed++; Console.WriteLine($"[ OK ] {name}"); }
        else           { failed++; Console.WriteLine($"[FAIL] {name}"); }
    }

    public static int Run()
    {
        // --- процедура проверки столбца ---
        Check("все отрицательные",
            MatrixLogic.IsColumnNonPositive(new[] { -1.0, -2.5, -3.0 }));
        Check("нули считаются неположительными",
            MatrixLogic.IsColumnNonPositive(new[] { 0.0, 0.0 }));
        Check("есть положительный элемент",
            !MatrixLogic.IsColumnNonPositive(new[] { -1.0, 0.1, -3.0 }));
        Check("один элемент: -5",  MatrixLogic.IsColumnNonPositive(new[] { -5.0 }));
        Check("один элемент: 5",  !MatrixLogic.IsColumnNonPositive(new[] { 5.0 }));

        // --- подсчёт столбцов ---
        var a = new double[,] { { -1, 2.5, -3, 0 }, { 0, 1, -4.5, -2 }, { -7.2, 3, -1.1, 0 } };
        var r1 = MatrixLogic.CountNonPositiveColumns(a);
        Check("пример: 3 столбца (1, 3, 4)",
            r1.Count == 3 && r1.Columns.SequenceEqual(new[] { 1, 3, 4 }));

        var b = new double[,] { { 1, 2 }, { 3, 4 } };
        Check("нет подходящих столбцов", MatrixLogic.CountNonPositiveColumns(b).Count == 0);

        var c = new double[,] { { -1, 0 }, { -2, -3 } };
        Check("все столбцы подходят", MatrixLogic.CountNonPositiveColumns(c).Count == 2);

        var big = new double[10, 10];
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++) big[i, j] = -1;
        Check("максимальный размер 10x10", MatrixLogic.CountNonPositiveColumns(big).Count == 10);

        Console.WriteLine($"\nПройдено: {passed}, провалено: {failed}");
        return failed == 0 ? 0 : 1;
    }
}