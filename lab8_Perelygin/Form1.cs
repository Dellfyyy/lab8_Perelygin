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
            // ��������� �������
            int[,] matrix = generator.GenerateMatrix(5, 5); // 5x5 �������.

            // ����� ������� � DataGridView
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                dataGridView1.Columns.Add($"Col{i}", $"������� {i + 1}");
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }

            // ���������� �������������
            int diagonalSum = generator.CalculateDiagonalSum(matrix);
            int totalSum = generator.CalculateTotalSum(matrix);
            int maxValue = generator.GetMaxValue(matrix);
            int minValue = generator.GetMinValue(matrix);

            // ����� ������������� �� �����
            label1.Text = $"����� ���������: {diagonalSum}, ����� ���� ���������: {totalSum}\n" +
                               $"����: {maxValue}, ���: {minValue}";
        }

        private void GenerateMatrix(object sender, EventArgs e)
        {

        }
    }

    // ��������� �����

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

    // ��������� ��������� ������� �� ���������
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

    // ��������� ������� �������
    public class ZeroMatrixGenerator : MatrixGenerator
    {
        public override int[,] GenerateMatrix(int rows, int cols)
        {
            return new int[rows, cols]; // ��� �������� ������������� ���������������� 0.
        }
    }

    // ��������� ��������� �������
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
                    matrix[i, j] = random.Next(0, 100); // ��������� ����� �� 0 �� 99.
                }
            }
            return matrix;
        }
    }

    // ��������� ��������� ������� �� ���������
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

    // ��������� ������� �� �������
    public class SpiralMatrixGenerator : MatrixGenerator
    {
        public override int[,] GenerateMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols]; // �������, ������� ����� ���������
            int value = 1;                       // ��������� ��������, ������� ����� ����������

            int top = 0, bottom = rows - 1;      // ������� � ��� �������
            int left = 0, right = cols - 1;      // ���� � ����� �������

            while (value <= rows * cols)
            {
                // �������� ����� ������� �� �������� ����
                for (int i = left; i <= right && value <= rows * cols; i++)
                {
                    matrix[top, i] = value++;
                }
                top++;

                // �������� ������ ���� �� ������� ����
                for (int i = top; i <= bottom && value <= rows * cols; i++)
                {
                    matrix[i, right] = value++;
                }
                right--;

                // �������� ������ ������ �� ������� ����
                for (int i = right; i >= left && value <= rows * cols; i--)
                {
                    matrix[bottom, i] = value++;
                }
                bottom--;

                // �������� ����� ����� �� ������ ����
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