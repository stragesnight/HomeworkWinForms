using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WINFORMS_DZ_08
{
    public class BoardPoint
    {
        private int _x = 0;
        public int X
        {
            get => _x;
            set
            {
                //_x = Math.Min(Math.Max(value, 0), ChessConstants.BoardSize.X);
                _x = value;
            }
        }

        private int _y = 0;
        public int Y
        {
            get => _y;
            set
            {
                //_y = Math.Min(Math.Max(value, 0), ChessConstants.BoardSize.Y);
                _y = value;
            }
        }

        public BoardPoint(int v)
        {
            X = v;
            Y = v;
        }

        public BoardPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public BoardPoint(System.Drawing.Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public override bool Equals(object obj)
        {
            return this == (obj as BoardPoint);
        }
        public static bool operator ==(BoardPoint p, BoardPoint other)
        {
            return (p.X == other.X && p.Y == other.Y);
        }

        public static bool operator !=(BoardPoint p, BoardPoint other)
        {
            return !(p == other);
        }

        public override string ToString()
        {
            return Convert.ToChar(X + (int)'A') 
                + (ChessConstants.BoardSize.Y - Y).ToString();
        }
    }
}
