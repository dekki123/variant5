// Главный модуль: интеграция MatrixLogic и IoUtils.
// Запуск:  dotnet run              - ввод с клавиатуры
//          dotnet run -- --demo    - демонстрационные данные
//          dotnet run -- --test    - запуск тестов

using System.Text;
namespace Variant5Arrays;

public static class Program
{
    private static readonly double[,] DemoA =
    {
        { -1.0, 2.5, -3.0, 0.0 },
        {  0.0, 1.0, -4.5, -2.0 },
        { -7.2, 3.0, -1.1, 0.0 }
    };

    private static readonly double[,] DemoB =
    {
        { 1.0, 2.0 },
        { 3.0, 4.0 }
    };

    public static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;   // чтобы русский текст отображался корректно
        Console.InputEncoding = Encoding.UTF8;

        if (args.Contains("--test"))
            return Tests.Run();

        double[,] a, b;
        if (args.Contains("--demo"))
        {
            a = DemoA;
            b = DemoB;
            Console.WriteLine("Массив A:");
            IoUtils.PrintMatrix(a);
            Console.WriteLine("Массив B:");
            IoUtils.PrintMatrix(b);
        }
        else
        {
            a = IoUtils.ReadMatrix("A");
            b = IoUtils.ReadMatrix("B");
        }

        var resA = MatrixLogic.CountNonPositiveColumns(a);
        var resB = MatrixLogic.CountNonPositiveColumns(b);

        IoUtils.PrintResult("A", resA.Count, resA.Columns);
        IoUtils.PrintResult("B", resB.Count, resB.Columns);

        // по условию: если таких столбцов нет ни для одного массива - сообщение
        if (resA.Count == 0 && resB.Count == 0)
            Console.WriteLine("Ни в одном из массивов нет столбцов только с неположительными элементами.");

        return 0;
    }
}
