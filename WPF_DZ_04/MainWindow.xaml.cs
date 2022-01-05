using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.Generic;

namespace WPF_DZ_04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _shouldEraseText = true;
        private TokenType _lastEnteredToken 
        {
            get
            {
                if (_tokenStack.Count < 1)
                    return TokenType.Invalid;
                return _tokenStack.Peek();
            }
            set
            {
                _tokenStack.Push(value);
            }
        }
        private Stack<TokenType> _tokenStack;

        public MainWindow()
        {
            InitializeComponent();
            var dependencyProperty = DependencyPropertyDescriptor.FromProperty(
                TextBlock.TextProperty, typeof(TextBlock)
            );
            dependencyProperty.AddValueChanged(
                textBlockExpression, textBlockExpression_TextChanged
            );

            _tokenStack = new Stack<TokenType>();
            ConstantHandler.InitializeConstants();
        }

        private void ValidateExpressionState()
        {
            string text = textBlockExpression.Text.Trim();
            char lastChar = text[text.Length - 1];
            bool isDigit = (lastChar >= '0' && lastChar <= '9') 
                           || lastChar == ',';

            if (isDigit)
                _lastEnteredToken = TokenType.Numeric;
            else if (lastChar == '(' || lastChar == ')')
                _lastEnteredToken = TokenType.Paren;

            textBlockExpression.Text = text;
        }

        private void UpdateNumericSystemTextBlocks(double value)
        {
            char[] hexchars = "0123456789ABCDEF".ToCharArray();
            string decstring = value.ToString();
            string hexstring = "";
            string binstring = "";
            uint uivalue = (uint)Math.Abs(value);

            for (int i = sizeof(uint) * 8 - 1; i >= 0; --i)
            {
                bool b = (uivalue & (1 << i)) != 0;
                binstring += b ? '1' : '0';

                if (i % 4 == 0)
                    binstring += ' ';
            }

            for (int i = sizeof(uint) * 8 - 4; i >= 0; i -= 4)
            {
                uint x = (uivalue & (uint)(15 << i)) >> i;
                hexstring += hexchars[x];

                if (i % 16 == 0)
                    hexstring += ' ';
            }

            textBlockResultDec.Text = "DEC " + decstring;
            textBlockResultHex.Text = "HEX " + hexstring.TrimStart('0', ' ');
            textBlockResultBin.Text = "BIN " + binstring.TrimStart('0', ' ');
        }

        private void textBlockExpression_TextChanged(object sender, EventArgs e)
        {
            if (textBlockExpression.Text.Length < 1)
                textBlockExpression.Text = "0";

            if (ExpressionParser.TryParse(textBlockExpression.Text, out double newValue))
                UpdateNumericSystemTextBlocks(newValue);
        }


        private void buttonCE_Click(object sender, RoutedEventArgs e)
        {
            textBlockExpression.Text = String.Empty;
            _lastEnteredToken = TokenType.Invalid;
            UpdateNumericSystemTextBlocks(0);
        }

        private void buttonC_Click(object sender, RoutedEventArgs e)
        {
            string text = textBlockExpression.Text.Trim();
            if (text.Length < 1)
                return;

            int spaceIndex = text.LastIndexOf(' ') + 1;
            string substring = text.Substring(0, spaceIndex);
            textBlockExpression.Text = substring;
            _tokenStack.Pop();
            _lastEnteredToken = _tokenStack.Pop();
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
            if (_lastEnteredToken != TokenType.Numeric)
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
            if (ExpressionParser.TryParse(textBlockExpression.Text, out double result))
            {
                textBlockResult.Text = textBlockExpression.Text;
                textBlockExpression.Text = String.Format("{0:F8}", result).TrimEnd('0', ',');
                _lastEnteredToken = TokenType.Numeric;
            }
            else
                EvaluationError();
        }

        private string GetBaseString()
        {
            string basestr = textBlockExpression.Text;
            if (textBlockExpression.Text == "0")
                _shouldEraseText = true;

            if (_shouldEraseText)
            {
                basestr = String.Empty;
                _shouldEraseText = false;
            }

            if (_lastEnteredToken == TokenType.Operation)
                basestr += ' ';

            return basestr;
        }

        private void NumericButton_Click(object sender, RoutedEventArgs e)
        {
            if (_lastEnteredToken == TokenType.Constant)
                return;

            textBlockExpression.Text = GetBaseString() + (sender as Button).Content;
            _lastEnteredToken = TokenType.Numeric;
        }

        private void ParenButton_Click(object sender, RoutedEventArgs e)
        {
            textBlockExpression.Text = GetBaseString() + (sender as Button).Content;
            _lastEnteredToken = TokenType.Paren;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (_lastEnteredToken == TokenType.Operation ||_lastEnteredToken == TokenType.Function)
                return;
            textBlockExpression.Text += ' ' + (sender as Button).Content.ToString();
            _lastEnteredToken = TokenType.Operation;
        }

        private void FunctionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(_lastEnteredToken == TokenType.Operation || _lastEnteredToken == TokenType.Function
                || _lastEnteredToken == TokenType.Invalid))
                return;

            textBlockExpression.Text = GetBaseString() + (sender as Button).Content.ToString() + '(';
            _lastEnteredToken = TokenType.Function;
        }

        private void ConstantButton_Click(object sender, RoutedEventArgs e)
        {
            if (_lastEnteredToken == TokenType.Numeric || _lastEnteredToken == TokenType.Constant)
                return;

            textBlockExpression.Text = GetBaseString() + (sender as Button).Content;
            _lastEnteredToken = TokenType.Constant;
        }

        private void EvaluationError()
        {
            textBlockExpression.Text = "ОШИБКА ПРИ ВЫЧИСЛЕНИИ";
            _shouldEraseText = true;
        }
    }
}
