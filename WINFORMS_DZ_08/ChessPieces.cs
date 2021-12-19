using System;
using System.Drawing;
using System.Collections.Generic;

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

        public override string ToString()
        {
            return $"Пешка ({(PColor == PieceColor.White ? "Белая" : "Чёрная")})";
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
            Point pos = new Point(Position.X * tileSize.X, Position.Y * tileSize.Y);

            Brush primaryBrush = GetPrimaryBrush();
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                pos.X + 10, pos.Y + 50,
                44, 12
            );
            Rectangle rect2 = new Rectangle(
                pos.X + 18, pos.Y + 44,
                28, 12
            );
            Rectangle rect3 = new Rectangle(
                pos.X + 27, pos.Y + 26,
                10, 26
            );
            Rectangle rect4 = new Rectangle(
                pos.X + 22, pos.Y + 12,
                20, 26
            );
            Rectangle rect5 = new Rectangle(
                pos.X + 27, pos.Y + 6,
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

        public override string ToString()
        {
            return $"Слон ({(PColor == PieceColor.White ? "Белый" : "Чёрный")})";
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
            Point pos = new Point(Position.X * tileSize.X, Position.Y * tileSize.Y);

            Brush primaryBrush = GetPrimaryBrush();
            Brush blackBrush = ChessConstants.PieceBrushBlack;
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                pos.X + 16, pos.Y + 38,
                36, 38
            );
            Rectangle rect2 = new Rectangle(
                pos.X + 8, pos.Y + 12,
                46, 56
            );
            Rectangle rect3 = new Rectangle(
                pos.X + 8, pos.Y + 12,
                38, 40
            );
            Rectangle rect4 = new Rectangle(
                pos.X + 8, pos.Y + 26,
                12, 12
            );
            Rectangle rect5 = new Rectangle(
                pos.X + 16, pos.Y + 16,
                4, 5
            );
            Rectangle rect6 = new Rectangle(
                pos.X + 8, pos.Y + 26,
                3, 2
            );
            Rectangle rect7 = new Rectangle(
               pos.X + 18, pos.Y + 7,
               10, 12
            );
            Rectangle rect8 = new Rectangle(
               pos.X + 30, pos.Y + 7,
               10, 12
            );
            g.FillPie(primaryBrush, rect1, 180, 180);
            g.DrawPie(secondaryPen, rect1, 180, 180);
            g.FillPie(primaryBrush, rect4, 110, 350);
            g.DrawPie(secondaryPen, rect4, 110, 350);
            g.FillPie(primaryBrush, rect3, 180, 115);
            g.DrawPie(secondaryPen, rect3, 180, 115);
            g.FillPie(primaryBrush, rect2, 270, 120);
            g.DrawPie(secondaryPen, rect2, 270, 120);
            g.DrawEllipse(secondaryPen, rect5);
            g.DrawEllipse(secondaryPen, rect6);
            g.FillPie(blackBrush, rect7, 265, 100);
            g.FillPie(blackBrush, rect8, 165, 100);
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

        public override string ToString()
        {
            return $"Конь ({(PColor == PieceColor.White ? "Белый" : "Чёрный")})";
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
            Point pos = new Point(Position.X * tileSize.X, Position.Y * tileSize.Y);

            Brush primaryBrush = GetPrimaryBrush();
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                pos.X + 12, pos.Y + 50,
                40, 6
            );
            Rectangle rect2 = new Rectangle(
                pos.X + 16, pos.Y + 44,
                32, 12
            );
            Rectangle rect3 = new Rectangle(
                pos.X + 20, pos.Y + 26,
                24, 20
            );
            Rectangle rect4 = new Rectangle(
                pos.X + 18, pos.Y + 16,
                28, 12
            );
            Rectangle rect5 = new Rectangle(
                pos.X + 16, pos.Y + 18,
                32, 4
            );
            Rectangle rect6 = new Rectangle(
                pos.X + 17, pos.Y + 12,
                6, 6
            );
            Rectangle rect7 = new Rectangle(
                pos.X + 29, pos.Y + 12,
                6, 6
            );
            Rectangle rect8 = new Rectangle(
                pos.X + 41, pos.Y + 12,
                6, 6
            );

            g.FillRectangle(primaryBrush, rect1);
            g.DrawRectangle(secondaryPen, rect1);
            g.FillPie(primaryBrush, rect2, 180, 180);
            g.DrawPie(secondaryPen, rect2, 180, 180);
            g.FillPie(primaryBrush, rect4, 0, 180);
            g.DrawPie(secondaryPen, rect4, 0, 180);
            g.FillRectangle(primaryBrush, rect3);
            g.DrawRectangle(secondaryPen, rect3);
            g.FillRectangle(primaryBrush, rect5);
            g.DrawRectangle(secondaryPen, rect5);
            g.FillRectangle(primaryBrush, rect6);
            g.DrawRectangle(secondaryPen, rect6);
            g.FillRectangle(primaryBrush, rect7);
            g.DrawRectangle(secondaryPen, rect7);
            g.FillRectangle(primaryBrush, rect8);
            g.DrawRectangle(secondaryPen, rect8);
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

        public override string ToString()
        {
            return $"Ладья ({(PColor == PieceColor.White ? "Белая" : "Чёрная")})";
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
            Point pos = new Point(Position.X * tileSize.X, Position.Y * tileSize.Y);

            Brush primaryBrush = GetPrimaryBrush();
            Pen blackPen = new Pen(ChessConstants.PieceBrushBlack, 2f);
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                pos.X + 12, pos.Y + 48,
                40, 16
            );
            Rectangle rect2 = new Rectangle(
                pos.X + 18, pos.Y + 40,
                28, 16
            );
            Rectangle rect3 = new Rectangle(
                pos.X + 12, pos.Y + 32,
                40, 28
            );
            Point[][] points = {
                new Point[] {
                    new Point(pos.X + 13, pos.Y + 40),
                    new Point(pos.X + 11, pos.Y + 18),
                    new Point(pos.X + 19, pos.Y + 36)
                },
                new Point[] {
                    new Point(pos.X + 22, pos.Y + 36),
                    new Point(pos.X + 21, pos.Y + 14),
                    new Point(pos.X + 28, pos.Y + 34)
                },
                new Point[] {
                    new Point(pos.X + 29, pos.Y + 34),
                    new Point(pos.X + 32, pos.Y + 12),
                    new Point(pos.X + 35, pos.Y + 34)
                },
                new Point[] {
                    new Point(pos.X + 42, pos.Y + 36),
                    new Point(pos.X + 43, pos.Y + 14),
                    new Point(pos.X + 36, pos.Y + 34)
                },
                new Point[] {
                    new Point(pos.X + 51, pos.Y + 40),
                    new Point(pos.X + 53, pos.Y + 18),
                    new Point(pos.X + 45, pos.Y + 36)
                }
            };

            for (int i = 0; i < 5; ++i)
            {
                g.FillPolygon(primaryBrush, points[i]);
                g.DrawPolygon(blackPen, points[i]);
            }

            g.FillPie(primaryBrush, rect3, 190, 160);
            g.DrawPie(secondaryPen, rect3, 190, 160);
            g.FillPie(primaryBrush, rect1, 185, 170);
            g.DrawPie(secondaryPen, rect1, 185, 170);
            g.FillPie(primaryBrush, rect2, 175, 190);
            g.DrawPie(secondaryPen, rect2, 175, 190);
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

        public override string ToString()
        {
            return $"Ферзь ({(PColor == PieceColor.White ? "Белый" : "Чёрный")})";
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
            Point pos = new Point(Position.X * tileSize.X, Position.Y * tileSize.Y);

            Brush primaryBrush = GetPrimaryBrush();
            Pen blackPen = new Pen(ChessConstants.PieceBrushBlack, 2f);
            Pen secondaryPen = GetSecondaryPen();

            Rectangle rect1 = new Rectangle(
                pos.X + 14, pos.Y + 48,
                36, 16
            );
            Rectangle rect2 = new Rectangle(
                pos.X + 16, pos.Y + 42,
                32, 14
            );
            Rectangle rect3 = new Rectangle(
                pos.X + 16, pos.Y + 36,
                32, 14
            );
            Rectangle rect4 = new Rectangle(
                pos.X + 8, pos.Y + 18,
                24, 24
            );
            Rectangle rect5 = new Rectangle(
                pos.X + 32, pos.Y + 18,
                24, 24
            );
            Rectangle rect6 = new Rectangle(
                pos.X + 27, pos.Y + 14,
                10, 20
            );
            Rectangle rect7 = new Rectangle(
                pos.X + 27, pos.Y + 9,
                10, 2
            );
            Rectangle rect8 = new Rectangle(
                pos.X + 31, pos.Y + 6,
                2, 10
            );

            g.DrawEllipse(blackPen, rect7);
            g.DrawEllipse(blackPen, rect8);
            g.FillEllipse(primaryBrush, rect6);
            g.DrawEllipse(blackPen, rect6);
            g.FillEllipse(primaryBrush, rect4);
            g.DrawEllipse(secondaryPen, rect4);
            g.FillEllipse(primaryBrush, rect5);
            g.DrawEllipse(secondaryPen, rect5);
            g.FillPie(primaryBrush, rect1, 190, 160);
            g.DrawPie(blackPen, rect1, 190, 160);
            g.FillPie(primaryBrush, rect2, 170, 200);
            g.DrawPie(secondaryPen, rect2, 170, 200);
            g.FillPie(primaryBrush, rect3, 170, 200);
            g.DrawPie(secondaryPen, rect3, 170, 200);
        }

        public override List<BoardPoint> GetValidMoves()
        {
            List<BoardPoint> moves = new List<BoardPoint>();

            List<BoardPoint> occupiedByTeam = PColor == PieceColor.White
                                         ? Parent.GetOccupiedByWhite()
                                         : Parent.GetOccupiedByBlack();

            for (int y = -1; y <= 1; ++y)
            {
                for (int x = -1; x <= 1; ++x)
                {
                    BoardPoint pos = new BoardPoint(Position.X + x, Position.Y + y);
                    if (occupiedByTeam.Contains(pos))
                        continue;
                    moves.Add(pos);
                }
            }

            return moves;
        }

        public override string ToString()
        {
            return $"Король ({(PColor == PieceColor.White ? "Белый" : "Чёрный")})";
        }
    }
}
