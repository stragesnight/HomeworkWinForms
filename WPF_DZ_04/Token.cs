using System;

namespace WPF_DZ_04
{
    public enum TokenType
    {
        Numeric,
        Operation,
        Function,
        Constant,
        Paren,
        Invalid
    };

    public abstract class Token
    {
        public TokenType Type { get; set; }

        public Token()
        {
            Type = TokenType.Invalid;
        }

        public Token(TokenType type)
        {
            Type = type;
        }
    }

    public class NumericToken : Token
    {
        public double Data { get; set; }

        public NumericToken(double data) : base(TokenType.Numeric)
        {
            Data = data;
        }
    }

    public class OperationToken : Token
    {
        public char C { get; set; }
        public int Precedence { get; private set; }

        public OperationToken(char c) : base(TokenType.Operation)
        {
            C = c;
            Precedence = (c == (char)0x2b || c == (char)0x2212) ? 0 : 1;
        }
    }

    public class FunctionToken : Token
    {
        public string Name { get; set; }

        public FunctionToken(string name) : base(TokenType.Function)
        {
            Name = name;
        }
    }

    public class ConstantToken : NumericToken
    { 
        public char C { get; set; }

        private static double EvaluateConstant(char c)
        {
            switch ((KnownConstant)c)
            {
                case KnownConstant.Pi:
                    return ConstantHandler.Pi.Value;
                case KnownConstant.E:
                    return ConstantHandler.E.Value;
                case KnownConstant.Phi:
                    return ConstantHandler.Phi.Value;
                case KnownConstant.Psi:
                    return ConstantHandler.Psi.Value;
                case KnownConstant.Delta:
                    return ConstantHandler.Delta.Value;
                case KnownConstant.Sigma:
                    return ConstantHandler.Sigma.Value;
            }

            throw new Exception("Invalid constant name");
        }

        public ConstantToken(char c) : base(EvaluateConstant(c))
        {
            C = c;
        }
    }

    public class ParenToken : Token
    {
        public bool Open { get; set; }

        public ParenToken(bool open) : base(TokenType.Paren)
        {
            Open = open;
        }
    }
}
