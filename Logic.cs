// Модуль бизнес-логики (Студент 1).
// Вариант 5: для каждого вещественного массива найти количество столбцов,
// содержащих только неположительные элементы (<= 0).

namespace Variant5;

public static class MatrixLogic
{
    /// <summary>
    /// Процедура проверки столбца. В неё передаются ВСЕ элементы текущего столбца.
    /// Возвращает true, если каждый элемент <= 0.
    /// </summary>
    public static bool IsColumnNonPositive(double[] column)
    {
        foreach (double value in column)
        {
            if (value > 0)      // нашли положительный элемент - столбец не подходит
                return false;
        }
        return true;
    }

    /// <summary>Возвращает j-й столбец матрицы в виде одномерного массива.</summary>
    public static double[] GetColumn(double[,] matrix, int j)
    {
        int rows = matrix.GetLength(0);
        double[] column = new double[rows];
        for (int i = 0; i < rows; i++)
            column[i] = matrix[i, j];
        return column;
    }

    /// <summary>
    /// Считает столбцы, состоящие только из неположительных элементов.
    /// Возвращает количество и номера столбцов (нумерация с 1).
    /// </summary>
    public static (int Count, List<int> Columns) CountNonPositiveColumns(double[,] matrix)
    {
        var columns = new List<int>();
        int cols = matrix.GetLength(1);
        for (int j = 0; j < cols; j++)
        {
            if (IsColumnNonPositive(GetColumn(matrix, j)))
                columns.Add(j + 1);
        }
        return (columns.Count, columns);
    }
}
