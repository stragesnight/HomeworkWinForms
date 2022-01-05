using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace WPF_DZ_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _shouldEraseText = true;
        private bool _isLastEnteredOperation = true;

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

        private void ValidateExpressionState()
        {
            string text = textBlockExpression.Text.Trim();
            char lastChar = text[text.Length - 1];
            bool isDigit = (lastChar >= '0' && lastChar <= '9') || lastChar == ',';

            _isLastEnteredOperation = !isDigit;
            if (isDigit)
                textBlockExpression.Text = text;
        }

        private void textBlockExpression_TextChanged(object sender, EventArgs e)
        {
            if (textBlockExpression.Text.Length < 1)
                textBlockExpression.Text = "0";

            if (TryEvaluateExpression(out double newValue))
                textBlockResult.Text = String.Format("{0:F2}", newValue);
        }


        private void buttonCE_Click(object sender, RoutedEventArgs e)
        {
            textBlockExpression.Text = String.Empty;
            _isLastEnteredOperation = true;
        }

        private void buttonC_Click(object sender, RoutedEventArgs e)
        {
            string text = textBlockExpression.Text.Trim();
            int spaceIndex = text.LastIndexOf(' ') + 1;
            string substring = text.Substring(0, spaceIndex);
            textBlockExpression.Text = substring;
            ValidateExpressionState();
        }

        private void buttonErase_Click(object sender, RoutedEventArgs e)
        {
            string text = textBlockExpression.Text;

            if (text.Length < 1)
                return;

            textBlockExpression.Text = text.Substring(0, text.Length - 1);
            ValidateExpressionState();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonChangeSign_Click(object sender, RoutedEventArgs e)
        {
            if (_isLastEnteredOperation)
                return;

            string text = textBlockExpression.Text.Trim();
            if (text.Length < 1)
                return;

            int spaceIndex = text.LastIndexOf(' ') + 1;
            if (spaceIndex < 0)
                spaceIndex = 0;

            if (text[spaceIndex] == '-')
                textBlockExpression.Text = text.Remove(spaceIndex, 1);
            else
                textBlockExpression.Text = text.Insert(spaceIndex, "-");
        }

        private void buttonEquals_Click(object sender, RoutedEventArgs e)
        {
            if (TryEvaluateExpression(out double result))
                textBlockExpression.Text = String.Format("{0:F2}", result);
            else
                EvaluationError();
        }

        private void NumericButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockExpression.Text == "0")
                _shouldEraseText = true;

            if (_shouldEraseText)
            {
                textBlockExpression.Text = (sender as Button).Content.ToString();
                _shouldEraseText = false;
            }
            else
            {
                textBlockExpression.Text += (sender as Button).Content;
            }

            _isLastEnteredOperation = false;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLastEnteredOperation)
                return;
            textBlockExpression.Text += ' ' + (sender as Button).Content.ToString() + ' ';
            _isLastEnteredOperation = true;
        }

        // Вычислить результат операции
        private double EvaluateOperation(double x, double y, char op)
        {
            switch (op)
            {
                case (char)0x2b:
                    return x + y;
                case (char)0x2212:
                    return x - y;
                case (char)0xd7:
                    return x * y;
                case (char)0xf7:
                    return x / y;
                case '^':
                    return Math.Pow(x, y);
                case '%':
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
