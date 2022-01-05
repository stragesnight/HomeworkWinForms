using System;
using System.Windows;
using System.Collections.Generic;

namespace WPF_DZ_04
{
    public static class ExpressionParser
    {
        private static NumericToken EvaluateOperation(double x, double y, char op)
        {
            switch (op)
            {
                case (char)0x2b:
                    return new NumericToken(x + y);
                case (char)0x2212:
                    return new NumericToken(x - y);
                case (char)0xd7:
                    return new NumericToken(x * y);
                case (char)0xf7:
                    return new NumericToken(x / y);
                case '^':
                    return new NumericToken(Math.Pow(x, y));
                case '%':
                    return new NumericToken((int)x % (int)y);
                default:
                    return null;
            }
        }

        private static NumericToken EvaluateFunction(double a, string str)
        {
            if (str == "sin")
                return new NumericToken(Math.Sin(a));
            else if (str == "cos")
                return new NumericToken(Math.Cos(a));
            else if (str == "tan")
                return new NumericToken(Math.Tan(a));
            else if (str == "ln")
                return new NumericToken(Math.Log(a));
            else if (str == "lg")
                return new NumericToken(Math.Log10(a));
            throw new Exception("Invalid function name");
        }

        private static double Evaluate(Queue<Token> tokens)
        {
            Stack<Token> stack = new Stack<Token>();

            while (tokens.Count > 0)
            {
                Token t = tokens.Dequeue();
                stack.Push(t);

                if (t.Type == TokenType.Operation)
                {
                    if (stack.Count < 3)
                        throw new Exception("Invalid stack");

                    char c = (stack.Pop() as OperationToken).C;
                    double a = (stack.Pop() as NumericToken).Data;
                    double b = (stack.Pop() as NumericToken).Data;

                    stack.Push(EvaluateOperation(b, a, c));
                }

                if (t.Type == TokenType.Function)
                {
                    if (stack.Count < 3)
                        throw new Exception("Invalid stack");

                    string str = (stack.Pop() as FunctionToken).Name;
                    double a = (stack.Pop() as NumericToken).Data;

                    stack.Push(EvaluateFunction(a, str));
                }
            }

            if (stack.Count != 1)
                throw new Exception("Invalid result stack");

            return (stack.Pop() as NumericToken).Data;
        }

        public static void CheckParents(List<Token> tokens)
        {
            Stack<ParenToken> parenStack = new Stack<ParenToken>();

            foreach (Token t in tokens)
            {
                if (t.Type != TokenType.Paren)
                    continue;

                ParenToken pt = t as ParenToken;
                if (pt.Open)
                    parenStack.Push(t as ParenToken);
                else if (parenStack.Count < 1)
                    throw new Exception("Unmatched parens");
                else
                    parenStack.Pop();
            }

            if (parenStack.Count > 0)
                throw new Exception("Unmatched parens");
        }

        public static Queue<Token> ConvertToRPN(List<Token> tokens)
        {
            Queue<Token> result = new Queue<Token>();
            Stack<Token> operatorStack = new Stack<Token>();

            foreach (Token t in tokens)
            {
                switch (t.Type)
                {
                case TokenType.Numeric:
                case TokenType.Constant:
                    result.Enqueue(t);
                    break;
                case TokenType.Function:
                    operatorStack.Push(t);
                    break;
                case TokenType.Operation:
                    if (operatorStack.Count > 0)
                    {
                        Token top = operatorStack.Peek();
                        while (top.Type == TokenType.Operation
                            && (top as OperationToken).Precedence >= (t as OperationToken).Precedence)
                        {
                            result.Enqueue(operatorStack.Pop());
                            if (operatorStack.Count == 0)
                                break;
                            top = operatorStack.Peek();
                        }
                    }
                    operatorStack.Push(t);
                    break;
                case TokenType.Paren:
                    if ((t as ParenToken).Open)
                    {
                        operatorStack.Push(t);
                    }
                    else
                    {
                        Token top = operatorStack.Pop();
                        while (!(top.Type == TokenType.Paren && (top as ParenToken).Open))
                        {
                            result.Enqueue(top);
                            top = operatorStack.Pop();
                        }
                    }
                    break;
                default:
                    throw new Exception("Invalid token type");
                }
            }

            while (operatorStack.Count > 0)
                result.Enqueue(operatorStack.Pop());

            return result;
        }

        public static bool TryParse(string expression, out double result)
        {
            result = 0;

            try
            {
                List<Token> tokens = ExpressionLexer.Tokenize(expression);
                CheckParents(tokens);
                result = Evaluate(ConvertToRPN(tokens));
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }
    }
}
