using System;
using System.Linq;
using System.Collections.Generic;

namespace WPF_DZ_04
{
    public static class ExpressionLexer
    {
        private static bool IsNumeric(char c)
        {
            return (c >= '0' && c <= '9') || c == ',' || c == '-';
        }

        private static bool IsFunction(char c)
        {
            return ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                && !IsConstant(c);
        }

        private static bool IsConstant(char c)
        {
            return ConstantHandler.IsValidConstant(c);
        }

        private static bool IsParen(char c)
        {
            return c == '(' || c == ')';
        }

        private static bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\t';
        }

        private static bool IsOperation(char c)
        {
            return !(IsNumeric(c) || IsFunction(c) || IsParen(c) || IsWhitespace(c) || IsConstant(c));
        }

        private static double ReadNumeric(string str, ref int index)
        {
            int beforeComma = 0;
            int afterComma = 0;
            int afterCommaLen = 0;
            int mod = 1;
            int len = str.Length;
            bool encounteredComma = false;

            if (str[index] == '-')
            {
                mod = -1;
                ++index;
            }

            while (index < len && IsNumeric(str[index]))
            {
                if (str[index] == ',')
                {
                    ++index;
                    encounteredComma = true;
                    break;
                }

                beforeComma *= 10;
                beforeComma += str[index] - '0';
                ++index;
            }

            if (!encounteredComma)
                return mod * beforeComma;

            while (index < len && IsNumeric(str[index]))
            {
                afterComma *= 10;
                afterComma += str[index] - '0';
                ++index;
                ++afterCommaLen;
            }

            return mod * (beforeComma + (afterComma / Math.Pow(10, afterCommaLen)));
        }

        private static char ReadOperation(string str, ref int index)
        {
            return str[index++];
        }

        private static string ReadFunction(string str, ref int index)
        {
            string result = "";
            int len = str.Length;
            
            while (index < len && IsFunction(str[index]))
            {
                result += str[index];
                ++index;
            }

            return result;
        }

        private static char ReadConstant(string str, ref int index)
        {
            return str[index++];
        }

        private static bool ReadParen(string str, ref int index)
        {
            return str[index++] == '(';
        }

        private static void SkipWhitespace(string str, ref int index)
        {
            while (IsWhitespace(str[index]))
                ++index;
        }

        private static Token ReadToken(string str, ref int index)
        {
            SkipWhitespace(str, ref index);

            if (IsNumeric(str[index]))
                return new NumericToken(ReadNumeric(str, ref index));
            else if (IsOperation(str[index]))
                return new OperationToken(ReadOperation(str, ref index));
            else if (IsFunction(str[index]))
                return new FunctionToken(ReadFunction(str, ref index));
            else if (IsConstant(str[index]))
                return new ConstantToken(ReadConstant(str, ref index));
            else if (IsParen(str[index]))
                return new ParenToken(ReadParen(str, ref index));

            throw new Exception("Unrecognized token");
        }

        public static List<Token> Tokenize(string expression)
        {
            List<Token> result = new List<Token>();
            string str = expression.Trim();
            int len = str.Length;
            int index = 0;

            while (index < len)
                result.Add(ReadToken(str, ref index));

            return result;
        }
    }
}
