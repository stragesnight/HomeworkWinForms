using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace WPF_DZ_02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _shouldEraseText = true;

        public MainWindow()
        {
            InitializeComponent();
            var dependencyProperty = DependencyPropertyDescriptor.FromProperty(
                TextBlock.TextProperty, typeof(TextBlock)
            );
            dependencyProperty.AddValueChanged(
                textBlockExpression, textBlockExpression_TextChanged
            );
        }

        private void textBlockExpression_TextChanged(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double newValue))
                textBlockResult.Text = String.Format("{0:F2}", newValue);
        }


        private void buttonCE_Click(object sender, RoutedEventArgs e)
        {
            textBlockExpression.Text = String.Empty;
        }

        private void buttonC_Click(object sender, RoutedEventArgs e)
        {
            string text = textBlockExpression.Text.Trim();
            int spaceIndex = text.LastIndexOf(' ') + 1;
            string substring = text.Substring(0, spaceIndex);
            textBlockExpression.Text = substring;
        }

        private void buttonErase_Click(object sender, RoutedEventArgs e)
        {
            string text = textBlockExpression.Text;

            if (text.Length < 1)
                return;

            textBlockExpression.Text = text.Substring(0, text.Length - 1);
        }

        private void buttonEquals_Click(object sender, RoutedEventArgs e)
        {
            if (TryEvaluateExpression(out double result))
                textBlockExpression.Text = result.ToString();
            else
                EvaluationError();
        }

        private void NumericButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockExpression.Text == "0")
                _shouldEraseText = true;

            if (_shouldEraseText)
            {
                textBlockExpression.Text = String.Empty;
                _shouldEraseText = false;
            }

            textBlockExpression.Text += (sender as Button).Content;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            textBlockExpression.Text += ' ' + (sender as Button).Content.ToString() + ' ';
        }

        // Вычислить результат операции
        private double EvaluateOperation(double x, double y, char op)
        {
            switch (op)
            {
                case '+':
                    return x + y;
                case '-':
                    return x - y;
                case '*':
                    return x * y;
                case '/':
                    return x / y;
                case '^':
                    return Math.Pow(x, y);
                case 'M':
                    return (int)x % (int)y;
                default:
                    return 0;
            }
        }

        private bool TryEvaluateExpression(out double result)
        {
            result = 0;

            // разбить текст строки по словам
            string[] tokens = textBlockExpression.Text.Split(' ');

            try
            {
                result = double.Parse(tokens[0]);

                for (int i = 1; i < tokens.Length - 1; i += 2)
                {
                    char operation = tokens[i][0];
                    double operand = double.Parse(tokens[i + 1]);
                    result = EvaluateOperation(result, operand, operation);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void EvaluationError()
        {
            textBlockExpression.Text = "ОШИБКА ПРИ ВЫЧИСЛЕНИИ";
            _shouldEraseText = true;
        }
    }
}
