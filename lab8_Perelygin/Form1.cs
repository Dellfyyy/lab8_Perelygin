using System;
using System.Linq;
using System.Windows.Forms;

namespace lab8_Perelygin
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateMatrix(MatrixGenerator generator)
        {
            // Генерация матрицы
            int[,] matrix = generator.GenerateMatrix(5, 5); // 5x5 матрица.

            // Вывод матрицы в DataGridView
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                dataGridView1.Columns.Add($"Col{i}", $"Колонка {i + 1}");
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }

            // Вычисление характеристик
            int diagonalSum = generator.CalculateDiagonalSum(matrix);
            int totalSum = generator.CalculateTotalSum(matrix);
            int maxValue = generator.GetMaxValue(matrix);
            int minValue = generator.GetMinValue(matrix);

            // Вывод характеристик на экран
            label1.Text = $"Сумма диагонали: {diagonalSum}, Сумма всех элементов: {totalSum}\n" +
                               $"Макс: {maxValue}, Мин: {minValue}";
        }

        private void GenerateMatrix(object sender, EventArgs e)
        {

        }
    }

    // Шаблонный метод

    public abstract class MatrixGenerator
    {
        public abstract int[,] GenerateMatrix(int rows, int cols);

        public int CalculateDiagonalSum(int[,] matrix)
        {
            int sum = 0;
            int size = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
            for (int i = 0; i < size; i++)
            {
                sum += matrix[i, i];
            }
            return sum;
        }

        public int CalculateTotalSum(int[,] matrix)
        {
            return matrix.Cast<int>().Sum();
        }

        public int GetMaxValue(int[,] matrix)
        {
            return matrix.Cast<int>().Max();
        }

        public int GetMinValue(int[,] matrix)
        {
            return matrix.Cast<int>().Min();
        }
    }

    // Генерация единичной матрицы по диагонали
    public class DiagonalOnesGenerator : MatrixGenerator
    {
        public override int[,] GenerateMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols];
            for (int i = 0; i < Math.Min(rows, cols); i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }
    }

    // Генерация нулевой матрицы
    public class ZeroMatrixGenerator : MatrixGenerator
    {
        public override int[,] GenerateMatrix(int rows, int cols)
        {
            return new int[rows, cols]; // Все элементы автоматически инициализируются 0.
        }
    }

    // Генерация случайной матрицы
    public class RandomMatrixGenerator : MatrixGenerator
    {
        public override int[,] GenerateMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols];
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = random.Next(0, 100); // Случайные числа от 0 до 99.
                }
            }
            return matrix;
        }
    }

    // Генерация случайной матрицы по диагонали
    public class RandomDiagonalGenerator : MatrixGenerator
    {
        public override int[,] GenerateMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols];
            Random random = new Random();
            for (int i = 0; i < Math.Min(rows, cols); i++)
            {
                matrix[i, i] = random.Next(0, 100);
            }
            return matrix;
        }
    }

    // Генерация матрицы по спирали
    public class SpiralMatrixGenerator : MatrixGenerator
    {
        public override int[,] GenerateMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols]; // Матрица, которую будем заполнять
            int value = 1;                       // Начальное значение, которое будем записывать

            int top = 0, bottom = rows - 1;      // Вершина и низ спирали
            int left = 0, right = cols - 1;      // Лево и право спирали

            while (value <= rows * cols)
            {
                // Движение слева направо по верхнему краю
                for (int i = left; i <= right && value <= rows * cols; i++)
                {
                    matrix[top, i] = value++;
                }
                top++;

                // Движение сверху вниз по правому краю
                for (int i = top; i <= bottom && value <= rows * cols; i++)
                {
                    matrix[i, right] = value++;
                }
                right--;

                // Движение справа налево по нижнему краю
                for (int i = right; i >= left && value <= rows * cols; i--)
                {
                    matrix[bottom, i] = value++;
                }
                bottom--;

                // Движение снизу вверх по левому краю
                for (int i = bottom; i >= top && value <= rows * cols; i--)
                {
                    matrix[i, left] = value++;
                }
                left++;
            }

            return matrix;
        }
    }
}