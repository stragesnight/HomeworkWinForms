using System;
using System.Linq;
using System.Windows;
using System.Threading;

namespace WPF_DZ_04
{
    public enum KnownConstant
    {
        Pi = 960,
        E = 101,
        Phi = 981,
        Psi = 968,
        Delta = 948,
        Sigma = 962
    };

    public class MathConstant
    {
        public delegate double ConstCalcFunc();
        private double _value = 0;
        public double Value
        {
            get
            {
                while (!IsCalculated)
                    Thread.Sleep(10);
                return _value;
            }
            private set => _value = value;
        }

        public bool IsCalculated { get; private set; } = false;
        private ConstCalcFunc _calcFunc;

        public MathConstant(ConstCalcFunc calcFunc)
        {
            _calcFunc = calcFunc;
            new Thread(new ThreadStart(Calculate)).Start();
        }

        private void Calculate()
        {
            if (IsCalculated)
                return;

            Value = _calcFunc();
            IsCalculated = true;
        }
    }

    public static class ConstantHandler
    {
        public static MathConstant Pi;
        public static MathConstant E;
        public static MathConstant Phi;
        public static MathConstant Psi;
        public static MathConstant Delta;
        public static MathConstant Sigma;

        public static bool IsValidConstant(char c)
        {
            return ((KnownConstant[])Enum.GetValues(typeof(KnownConstant)))
                .Contains((KnownConstant)c);
        }

        public static void InitializeConstants()
        {
            Pi = new MathConstant(CalculatePi);
            E = new MathConstant(CalculateE);
            Phi = new MathConstant(CalculatePhi);
            Psi = new MathConstant(CalcualtePsi);
            Delta = new MathConstant(CalculateDelta);
            Sigma = new MathConstant(CalculateSigma);
        }

        private static double Factorial(double v)
        {
            if (v == 0)
                return 1;

            for (double i = v - 1; i > 0; --i)
                v *= i;

            return v;
        }

        private static double CalculatePi()
        {
            double pi = 0;
            const ulong n = 16;

            for (ulong i = 0; i < n; ++i)
            {
                pi += Factorial(4 * i) * (1103 + 26390 * i)
                    / (Math.Pow(Factorial(i), 4) * Math.Pow(396, 4 * i));
            }

            return 1.0 / (2 * Math.Sqrt(2) / 9801 * pi);
        }

        private static double CalculateE()
        {
            const ulong n = 0x1fff;
            double e = 0;

            for (ulong i = 0; i < n; ++i)
            {
                double tmp = 1;
                for (ulong j = 1; j <= i; ++j)
                    tmp *= j;
                e += 1.0 / tmp;
            }

            return e;
        }

        private static void Fibonacci(int n, out int a, out int b)
        {
            a = 1;
            b = 1;

            for (int i = 0; i < n; ++i)
            {
                int tmp = a;
                a += b;
                b = tmp;
            }
        }

        private static double CalculatePhi()
        {
            Fibonacci(16, out int a, out int b);
            return 1.0 + (double)b / a;
        }

        private static double CalcualtePsi()
        {
            const uint n = 256;
            int a = 1;
            int b = 1;
            double psi = 1;


            for (uint i = 0; i < n; ++i)
            {
                psi += 1.0 / a;
                int tmp = a;
                a += b;
                b = tmp;
            }

            return psi;
        }

        public static double CalculateDelta()
        {
            const int n = 16;
            double a1 = 1;
            double a2 = 0;
            double delta = 3.2;

            for (int i = 2; i <= n; ++i)
            {
                double a = a1 + (a1 - a2) / delta;

                for (int j = 1; j <= n; ++j)
                {
                    double x = 0;
                    double y = 0;

                    for (int k = 1; k <= 1 << i; ++k)
                    {
                        y = 1.0 - 2.0 * y * x;
                        x = a - x * x;
                    }

                    a -= x / y;
                }

                delta = (a1 - a2) / (a - a1);
                a2 = a1;
                a1 = a;
            }

            return delta;
        }

        private static double CalculateSigma()
        {
            ulong n = 256000;
            double sigma = 1;
            double tmp = 1;

            for (ulong i = 0; i < n; ++i)
            {
                sigma += sigma / tmp;
                tmp *= 2;
            }

            return sigma;
        }
    }
}
