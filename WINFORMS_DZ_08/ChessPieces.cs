using System;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace WINFORMS_DZ_08.ChessPieces
{
    public enum PieceColor { White, Black };

    public abstract class Piece
    { 
        public PieceColor PColor { get; set; }
        public BoardPoint Position { get; protected set; }
        public ChessBoard Parent { get; protected set; }

        public Piece(PieceColor color, BoardPoint pos, ChessBoard parent)
        {
            PColor = color;
            Position = pos;
            Parent = parent;
        }

        protected Brush GetPrimaryBrush()
        {
            return PColor == PieceColor.White
                   ? ChessConstants.PieceBrushWhite
                   : ChessConstants.PieceBrushBlack;
        }

        protected Brush GetSecondaryBrush()
        {
            return PColor == PieceColor.White
                   ? ChessConstants.PieceBrushBlack
                   : ChessConstants.PieceBrushWhite;
        }

        protected Pen GetPrimaryPen() => new Pen(GetPrimaryBrush(), 2f);
        protected Pen GetSecondaryPen() => new Pen(GetSecondaryBrush(), 2f);
        
        public abstract void Draw(Graphics g);
        public abstract List<BoardPoint> GetValidMoves();

        public virtual void Move(BoardPoint pos)
        {
            Position = pos;
        }
    }

    public class Pawn : Piece
    {
        private bool _moved;

        public Pawn(PieceColor color, BoardPoint pos, ChessBoard parent) 
            : base(color, pos, parent)
        {
            _moved = false;
        }

        public override void Draw(Graphics g)
        {
            Point tileSize = ChessConstants.TileSize;

            Brush primaryBrush = GetPrimaryBrush();
            Pen secondaryPen = GetSecondaryPen();

            Point pos = new Point(Position.X * tileSize.X, Position.Y * tileSize.Y);

            Rectangle rect1 = new Rectangle(
                pos.X + 16, pos.Y + 28,
                32, 54
            );
            Rectangle rect2 = new Rectangle(
                pos.X + 20, pos.Y + 22,
                24, 10
            );
            Rectangle rect3 = new Rectangle(
                pos.X + 24, pos.Y + 8,
                16, 16
            );
            g.FillPie(primaryBrush, rect1, 180, 180);
            g.DrawPie(secondaryPen, rect1, 180, 180);
            g.FillEllipse(primaryBrush, rect2);
            g.DrawEllipse(secondaryPen, rect2);
            g.FillEllipse(primaryBrush, rect3);
            g.DrawEllipse(secondaryPen, rect3);
        }

        public override List<BoardPoint> GetValidMoves()
        {
            List<BoardPoint> moves = new List<BoardPoint>();

            List<BoardPoint> occupiedByTeam = PColor == PieceColor.White
                                         ? Parent.GetOccupiedByWhite()
                                         : Parent.GetOccupiedByBlack();
            List<BoardPoint> occupiedByEnemies = PColor == PieceColor.White
                                            ? Parent.GetOccupiedByBlack()
                                            : Parent.GetOccupiedByWhite();
            int mul = PColor == PieceColor.White ? -1 : 1;

            BoardPoint p1 = new BoardPoint(Position.X, Position.Y + (1 * mul));
            BoardPoint p2 = new BoardPoint(Position.X, Position.Y + (2 * mul));
            BoardPoint ap1 = new BoardPoint(Position.X - 1, Position.Y + (1 * mul));
            BoardPoint ap2 = new BoardPoint(Position.X + 1, Position.Y + (1 * mul));

            if (!occupiedByTeam.Contains(p1) && !occupiedByEnemies.Contains(p1))
                moves.Add(p1);
            if (!occupiedByTeam.Contains(p2) && !occupiedByEnemies.Contains(p2) && !_moved)
                moves.Add(p2);
            if (occupiedByEnemies.Contains(ap1))
                moves.Add(ap1);
            if (occupiedByEnemies.Contains(ap2))
                moves.Add(ap2);

            return moves;
        }

        public override void Move(BoardPoint pos)
        {
            base.Move(pos);
            _moved = true;
        }
    }

    public class Bishop : Piece
    {
        public Bishop(PieceColor color, BoardPoint pos, ChessBoard parent)
            : base(color, pos, parent)
        {}

        public override void Draw(Graphics g)
        {
            Point tileSize = ChessConstants.TileSize;

            Brush primaryBrush = GetPrimaryBrush();
            Pen secondaryPen = GetSecondaryPen();

            Point pos = new Point(Position.X * tileSize.X, Position.Y * tileSize.Y);

            Rectangle rect1 = new Rectangle(
                pos.X + 10, pos.Y + 48,
                44, 12
            );
            Rectangle rect2 = new Rectangle(
                pos.X + 18, pos.Y + 42,
                28, 12
            );
            Rectangle rect3 = new Rectangle(
                pos.X + 27, pos.Y + 24,
                10, 26
            );
            Rectangle rect4 = new Rectangle(
                pos.X + 22, pos.Y + 10,
                20, 26
            );
            Rectangle rect5 = new Rectangle(
                pos.X + 27, pos.Y + 4,
                10, 16
            );

            g.FillRectangle(primaryBrush, rect3);
            g.DrawRectangle(secondaryPen, rect3);
            g.FillPie(primaryBrush, rect1, 180, 180);
            g.DrawPie(secondaryPen, rect1, 180, 180);
            g.FillPie(primaryBrush, rect2, 180, 180);
            g.DrawPie(secondaryPen, rect2, 180, 180);
            g.FillEllipse(primaryBrush, rect5);
            g.DrawEllipse(secondaryPen, rect5);
            g.FillPie(primaryBrush, rect4, 320, 340);
            g.DrawPie(secondaryPen, rect4, 320, 340);
        }

        public override List<BoardPoint> GetValidMoves()
        {
            List<BoardPoint> moves = new List<BoardPoint>();

            List<BoardPoint> occupiedByTeam = PColor == PieceColor.White
                                         ? Parent.GetOccupiedByWhite()
                                         : Parent.GetOccupiedByBlack();
            List<BoardPoint> occupiedByEnemies = PColor == PieceColor.White
                                            ? Parent.GetOccupiedByBlack()
                                            : Parent.GetOccupiedByWhite();

            for (int y = -1; y <= 1; y += 2)
            {
                for (int x = -1; x <= 1; x += 2)
                {
                    for (int i = 1; i < 8; ++i)
                    {
                        BoardPoint pos = new BoardPoint(Position.X + (i * x), Position.Y + (i * y));
                        if (occupiedByTeam.Contains(pos))
                            break;
                        moves.Add(pos);
                        if (occupiedByEnemies.Contains(pos))
                            break;
                    }
                }
            }

            return moves;
        }
    }

    public class Knight : Piece
    {
        public Knight(PieceColor color, BoardPoint pos, ChessBoard parent)
            : base(color, pos, parent)
        { }

        public override void Draw(Graphics g)
        {
            Point tileSize = ChessConstants.TileSize;

            Brush primaryBrush = GetPrimaryBrush();
            Brush secondaryBrush = GetSecondaryBrush();
            Pen primaryPen = GetPrimaryPen();
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                Position.X * tileSize.X, Position.Y * tileSize.Y,
                tileSize.X, tileSize.Y
            );
            g.FillPie(primaryBrush, rect1, 270, 180);
            g.DrawPie(secondaryPen, rect1, 270, 180);
        }

        public override List<BoardPoint> GetValidMoves()
        {
            List<BoardPoint> moves = new List<BoardPoint>();

            List<BoardPoint> occupiedByTeam = PColor == PieceColor.White
                                         ? Parent.GetOccupiedByWhite()
                                         : Parent.GetOccupiedByBlack();

            BoardPoint[] possibleMoves = {
                new BoardPoint(Position.X + 1, Position.Y - 2),
                new BoardPoint(Position.X - 1, Position.Y - 2),
                new BoardPoint(Position.X + 1, Position.Y + 2),
                new BoardPoint(Position.X - 1, Position.Y + 2),

                new BoardPoint(Position.X - 2, Position.Y + 1),
                new BoardPoint(Position.X - 2, Position.Y - 1),
                new BoardPoint(Position.X + 2, Position.Y + 1),
                new BoardPoint(Position.X + 2, Position.Y - 1)
            };

            for (int i = 0; i < 8; ++i)
            {
                if (!occupiedByTeam.Contains(possibleMoves[i]))
                    moves.Add(possibleMoves[i]);
            }

            return moves;
        }
    }

    public class Rook : Piece
    {
        public Rook(PieceColor color, BoardPoint pos, ChessBoard parent)
            : base(color, pos, parent)
        { }

        public override void Draw(Graphics g)
        {
            Point tileSize = ChessConstants.TileSize;

            Brush primaryBrush = GetPrimaryBrush();
            Brush secondaryBrush = GetSecondaryBrush();
            Pen primaryPen = GetPrimaryPen();
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                Position.X * tileSize.X, Position.Y * tileSize.Y,
                tileSize.X, tileSize.Y
            );
            g.FillPie(primaryBrush, rect1, 270, 180);
            g.DrawPie(secondaryPen, rect1, 270, 180);
        }

        public override List<BoardPoint> GetValidMoves()
        {
            List<BoardPoint> moves = new List<BoardPoint>();

            List<BoardPoint> occupiedByTeam = PColor == PieceColor.White
                                         ? Parent.GetOccupiedByWhite()
                                         : Parent.GetOccupiedByBlack();
            List<BoardPoint> occupiedByEnemies = PColor == PieceColor.White
                                            ? Parent.GetOccupiedByBlack()
                                            : Parent.GetOccupiedByWhite();

            for (int y = -1; y <= 1; y += 2)
            {
                for (int i = 1; i < 8; ++i)
                {
                    BoardPoint pos = new BoardPoint(Position.X, Position.Y + (i * y));
                    if (occupiedByTeam.Contains(pos))
                        break;
                    moves.Add(pos);
                    if (occupiedByEnemies.Contains(pos))
                        break;
                }
            }

            for (int x = -1; x <= 1; x += 2)
            {
                for (int i = 1; i < 8; ++i)
                {
                    BoardPoint pos = new BoardPoint(Position.X + (i * x), Position.Y);
                    if (occupiedByTeam.Contains(pos))
                        break;
                    moves.Add(pos);
                    if (occupiedByEnemies.Contains(pos))
                        break;
                }
            }

            return moves;
        }
    }

    public class Queen : Piece
    {
        public Queen(PieceColor color, BoardPoint pos, ChessBoard parent)
            : base(color, pos, parent)
        { }

        public override void Draw(Graphics g)
        {
            Point tileSize = ChessConstants.TileSize;

            Brush primaryBrush = GetPrimaryBrush();
            Brush secondaryBrush = GetSecondaryBrush();
            Pen primaryPen = GetPrimaryPen();
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                Position.X * tileSize.X, Position.Y * tileSize.Y,
                tileSize.X, tileSize.Y
            );
            g.FillPie(primaryBrush, rect1, 270, 180);
            g.DrawPie(secondaryPen, rect1, 270, 180);
        }

        public override List<BoardPoint> GetValidMoves()
        {
            List<BoardPoint> moves = new List<BoardPoint>();

            List<BoardPoint> occupiedByTeam = PColor == PieceColor.White
                                         ? Parent.GetOccupiedByWhite()
                                         : Parent.GetOccupiedByBlack();
            List<BoardPoint> occupiedByEnemies = PColor == PieceColor.White
                                            ? Parent.GetOccupiedByBlack()
                                            : Parent.GetOccupiedByWhite();

            for (int y = -1; y <= 1; ++y)
            {
                for (int x = -1; x <= 1; ++x)
                {
                    for (int i = 1; i < 8; ++i)
                    {
                        BoardPoint pos = new BoardPoint(Position.X + (i * x), Position.Y + (i * y));
                        if (occupiedByTeam.Contains(pos))
                            break;
                        moves.Add(pos);
                        if (occupiedByEnemies.Contains(pos))
                            break;
                    }
                }
            }

            return moves;
        }
    }

    public class King : Piece
    {
        public King(PieceColor color, BoardPoint pos, ChessBoard parent)
            : base(color, pos, parent)
        { }

        public override void Draw(Graphics g)
        {
            Point tileSize = ChessConstants.TileSize;

            Brush primaryBrush = GetPrimaryBrush();
            Brush secondaryBrush = GetSecondaryBrush();
            Pen primaryPen = GetPrimaryPen();
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                Position.X * tileSize.X, Position.Y * tileSize.Y, 
                tileSize.X, tileSize.Y
            );
            g.FillPie(primaryBrush, rect1, 270, 180);
            g.DrawPie(secondaryPen, rect1, 270, 180);
        }

        public override List<BoardPoint> GetValidMoves()
        {
            List<BoardPoint> moves = new List<BoardPoint>();

            List<BoardPoint> occupiedByTeam = PColor == PieceColor.White
                                         ? Parent.GetOccupiedByWhite()
                                         : Parent.GetOccupiedByBlack();
            List<BoardPoint> occupiedByEnemies = PColor == PieceColor.White
                                            ? Parent.GetOccupiedByBlack()
                                            : Parent.GetOccupiedByWhite();

            for (int y = -1; y <= 1; ++y)
            {
                for (int x = -1; x <= 1; ++x)
                {
                    if (x == 0 && y == 0)
                        continue;

                    BoardPoint pos = new BoardPoint(Position.X + x, Position.Y + y);
                    if (occupiedByTeam.Contains(pos))
                        break;
                    moves.Add(pos);
                    if (occupiedByEnemies.Contains(pos))
                        break;
                }
            }

            return moves;
        }
    }
}
