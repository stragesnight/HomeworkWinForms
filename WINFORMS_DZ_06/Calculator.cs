using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS_DZ_06
{
    public partial class Calculator : Form
    {
        // нужо ли стереть текст перед вводом
        private bool _shouldEraseText = true;

        public Calculator()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "Калькулятор";
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            // инициализация компонентов формы

            this.Text = "Калькулятор";

            button1.Text = "1";
            button2.Text = "2";
            button3.Text = "3";
            button4.Text = "4";
            button5.Text = "5";
            button6.Text = "6";
            button7.Text = "7";
            button8.Text = "8";
            button9.Text = "9";
            button10.Text = "0";

            button11.Text = ",";
            button12.Text = String.Empty;

            button13.Text = "+";
            button14.Text = "-";
            button15.Text = "*";
            button16.Text = "/";
            button17.Text = "^";
            button18.Text = "MOD";

            button21.Text = "%";
            button22.Text = Convert.ToChar(0x221A).ToString();
            button23.Text = "Log" + Convert.ToChar(0x2091);
            button24.Text = "Log" + Convert.ToChar(0x2081) + Convert.ToChar(0x2080);
            button25.Text = "Sin";
            button26.Text = "Cos";
            button27.Text = Convert.ToChar(0x2190).ToString();
            button28.Text = "C";

            label1.Text = "0";
            label2.Text = "BIN: ";
            label3.Text = String.Empty;
            label4.Text = "HEX: ";

            this.Font = new Font(this.Font.FontFamily, this.Font.Size * 1.2f);
            label1.Font = new Font("Consolas", label1.Font.Size * 1.2f);
            label2.Font = new Font("Consolas", label2.Font.Size * 0.87f);
            label3.Font = new Font("Consolas", label3.Font.Size);
            label4.Font = new Font("Consolas", label4.Font.Size * 0.87f);
            button11.Font = new Font(this.Font.FontFamily, this.Font.Size * 1.5f);

            Color accentedButtonColor = Color.FromArgb(200, 220, 250);
            Color accentedButtonColor2 = Color.FromArgb(240, 200, 100);
            button12.BackColor = accentedButtonColor;
            button13.BackColor = accentedButtonColor;
            button14.BackColor = accentedButtonColor;
            button15.BackColor = accentedButtonColor;
            button16.BackColor = accentedButtonColor;
            button17.BackColor = accentedButtonColor;
            button18.BackColor = accentedButtonColor;
            button21.BackColor = accentedButtonColor;
            button22.BackColor = accentedButtonColor;
            button23.BackColor = accentedButtonColor;
            button24.BackColor = accentedButtonColor;
            button25.BackColor = accentedButtonColor;
            button26.BackColor = accentedButtonColor;
            button27.BackColor = accentedButtonColor2;
            button28.BackColor = accentedButtonColor2;
            label3.ForeColor = Color.DarkGray;

            // привязка кнопок к функциям

            button1.Click += new EventHandler(NumericButton_Click);
            button2.Click += new EventHandler(NumericButton_Click);
            button3.Click += new EventHandler(NumericButton_Click);
            button4.Click += new EventHandler(NumericButton_Click);
            button5.Click += new EventHandler(NumericButton_Click);
            button6.Click += new EventHandler(NumericButton_Click);
            button7.Click += new EventHandler(NumericButton_Click);
            button8.Click += new EventHandler(NumericButton_Click);
            button9.Click += new EventHandler(NumericButton_Click);
            button10.Click += new EventHandler(NumericButton_Click);
            button11.Click += new EventHandler(NumericButton_Click);

            button12.Click += new EventHandler(EvaluateExpressionProxy);

            button13.Click += new EventHandler(OperationButton_Click);
            button14.Click += new EventHandler(OperationButton_Click);
            button15.Click += new EventHandler(OperationButton_Click);
            button16.Click += new EventHandler(OperationButton_Click);
            button17.Click += new EventHandler(OperationButton_Click);
            button18.Click += new EventHandler(OperationButton_Click);

            button21.Click += new EventHandler(ImmediateOperation_Percent);
            button22.Click += new EventHandler(ImmediateOperation_Sqrt);
            button23.Click += new EventHandler(ImmediateOperation_LogE);
            button24.Click += new EventHandler(ImmediateOperation_Log10);
            button25.Click += new EventHandler(ImmediateOperation_Sin);
            button26.Click += new EventHandler(ImmediateOperation_Cos);
            button27.Click += new EventHandler(ImmediateOperation_Erase);
            button28.Click += new EventHandler(ImmediateOperation_C);
        }

        // Обновить информацию о результате выражения
        // в разных системах счисления
        private void UpdateNumericSystemLabels(double res)
        {
            char[] hexchars = "0123456789ABCDEF".ToCharArray();
            string binstring = "";
            string hexstring = "";
            uint ures = (uint)res;

            for (int i = sizeof(uint) * 8 - 1; i >= 0; --i)
            {
                bool b = (ures & (1 << i)) != 0;
                binstring += b ? '1' : '0';

                if (i % 4 == 0)
                    binstring += ' ';
            }

            for (int i = sizeof(uint) * 8 - 4; i >= 0; i -= 4)
            {
                uint x = (ures & (uint)(15 << i)) >> i;
                hexstring += hexchars[x];

                if (i % 16 == 0)
                    hexstring += ' ';
            }

            label2.Text = "BIN: " + binstring.TrimStart('0', ' ');
            label3.Text = "= " + res.ToString();
            label4.Text = "HEX: " + hexstring.TrimStart('0', ' ');
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

        // Срабатывает при нажатию на числовую кнопку
        private void NumericButton_Click(object sender, EventArgs e)
        {
            if (label1.Text == "0")
                _shouldEraseText = true;

            if (_shouldEraseText)
            {
                label1.Text = String.Empty;
                _shouldEraseText = false;
            }

            label1.Text += (sender as Button).Text;

            if (TryEvaluateExpression(out double result))
                UpdateNumericSystemLabels(result);
        }

        // Срабатывает при нажатии на кнопку операции
        private void OperationButton_Click(object sender, EventArgs e)
        {
            label1.Text += ' ' + (sender as Button).Text + ' ';
        }

        // Вычислить результат выражения
        private bool TryEvaluateExpression(out double result)
        {
            result = 0;

            // разбить текст строки по словам
            string[] tokens = label1.Text.Split(' ');

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

        // Вывести ошибку о невозможности вычисления
        private void EvaluationError()
        {
            label1.Text = "ОШИБКА ПРИ ВЫЧИСЛЕНИИ";
            _shouldEraseText = true;
        }

        // Срабатывает при нажатии кнопки "="
        private void EvaluateExpressionProxy(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double result))
            {
                label1.Text = result.ToString();
                UpdateNumericSystemLabels(result);
            }
            else
                EvaluationError();
        }

        // Найти процент от результата выражения
        private void ImmediateOperation_Percent(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double result))
            {
                double x = result / 100;
                label1.Text = x.ToString();
                UpdateNumericSystemLabels(x);
            }
            else
                EvaluationError();
        }

        // Найти квадратный корень результата выражения
        private void ImmediateOperation_Sqrt(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double result))
            {
                double x = Math.Sqrt(result);
                label1.Text = x.ToString();
                UpdateNumericSystemLabels(x);
            }
            else
                EvaluationError();
        }

        // Найти натуральный логарифм результата выражения
        private void ImmediateOperation_LogE(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double result))
            {
                double x = Math.Log(result);
                label1.Text = x.ToString();
                UpdateNumericSystemLabels(x);
            }
            else
                EvaluationError();
        }

        // Найти логарифм по основе 10 результата выражения
        private void ImmediateOperation_Log10(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double result))
            {
                double x = Math.Log10(result);
                label1.Text = x.ToString();
                UpdateNumericSystemLabels(x);
            }
            else
                EvaluationError();
        }

        // Найти синус результата выражения
        private void ImmediateOperation_Sin(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double result))
            {
                double x = Math.Sin(result);
                label1.Text = x.ToString();
                UpdateNumericSystemLabels(x);
            }
            else
                EvaluationError();
        }

        // Найти косинус результата выражения
        private void ImmediateOperation_Cos(object sender, EventArgs e)
        {
            if (TryEvaluateExpression(out double result))
            {
                double x = Math.Cos(result);
                label1.Text = x.ToString();
                UpdateNumericSystemLabels(x);
            }
            else
                EvaluationError();
        }

        // Стереть один символ с конца выражения
        private void ImmediateOperation_Erase(object sender, EventArgs e)
        {
            if (label1.Text.Length == 0)
                return;

            label1.Text = label1.Text.Substring(0, label1.Text.Length - 1);

            if (TryEvaluateExpression(out double result))
                UpdateNumericSystemLabels(result);
        }

        // Стереть выражение
        private void ImmediateOperation_C(object sender, EventArgs e)
        {
            label1.Text = String.Empty;
            UpdateNumericSystemLabels(0);
        }
    }
}
