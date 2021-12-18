using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using WINFORMS_DZ_08.ChessPieces;

namespace WINFORMS_DZ_08
{
    public class ChessBoard
    {
        private List<Piece> _pieces;
        private Piece _selectedPiece = null;

        public ChessBoard()
        {
            _pieces = new List<Piece>();

            AddPieces(PieceColor.White);
            AddPieces(PieceColor.Black);
        }

        private void AddPawns(PieceColor color)
        {
            int ypos = color == PieceColor.White
                       ? ChessConstants.BoardSize.Y - 2
                       : 1;
            for (int i = 0; i < ChessConstants.BoardSize.X; ++i)
            {
                BoardPoint pos = new BoardPoint(i, ypos);
                _pieces.Add(new Pawn(color, pos, this));
            }
        }

        private void AddPieces(PieceColor color)
        {
            AddPawns(color);

            int ypos = color == PieceColor.White
                       ? ChessConstants.BoardSize.Y - 1
                       : 0;

            _pieces.Add(new Rook(color, new BoardPoint(0, ypos), this));
            _pieces.Add(new Knight(color, new BoardPoint(1, ypos), this));
            _pieces.Add(new Bishop(color, new BoardPoint(2, ypos), this));
            _pieces.Add(new Queen(color, new BoardPoint(3, ypos), this));
            _pieces.Add(new King(color, new BoardPoint(4, ypos), this));
            _pieces.Add(new Bishop(color, new BoardPoint(5, ypos), this));
            _pieces.Add(new Knight(color, new BoardPoint(6, ypos), this));
            _pieces.Add(new Rook(color, new BoardPoint(7, ypos), this));
        }

        public List<BoardPoint> GetOccupiedByWhite()
        {
            List<BoardPoint> points = new List<BoardPoint>();

            foreach (Piece piece in _pieces)
            {
                if (piece.PColor == PieceColor.White)
                    points.Add(piece.Position);
            }

            return points;
        }

        public List<BoardPoint> GetOccupiedByBlack()
        {
            List<BoardPoint> points = new List<BoardPoint>();

            foreach (Piece piece in _pieces)
            {
                if (piece.PColor == PieceColor.Black)
                    points.Add(piece.Position);
            }

            return points;
        }

        private void HandlePieceMove(BoardPoint bp)
        {
            List<BoardPoint> opposite = _selectedPiece.PColor == PieceColor.White
                           ? GetOccupiedByBlack()
                           : GetOccupiedByWhite();

            if (!_selectedPiece.GetValidMoves().Contains(bp))
                return;

            if (opposite.Contains(bp))
                _pieces.Remove(GetPiece(bp));

            _selectedPiece.Move(bp);
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            BoardPoint bp = new BoardPoint(
                e.Location.X / ChessConstants.TileSize.X, 
                e.Location.Y / ChessConstants.TileSize.Y
            );

            if (_selectedPiece == null)
                _selectedPiece = GetPiece(bp);
            else
            {
                HandlePieceMove(bp);
                _selectedPiece = null;
            }
        }

        private Piece GetPiece(BoardPoint point)
        {
            foreach (Piece p in _pieces)
            {
                if (p.Position == point)
                    return p;
            }

            return null;
        }

        private void DrawBoard(Graphics g)
        {
            Point boardSize = ChessConstants.BoardSize;
            Point tileSize = ChessConstants.TileSize;

            for (int y = 0; y < boardSize.Y; ++y)
            {
                for (int x = 0; x < boardSize.X; ++x)
                {
                    Brush brush = ((y + x) & 1) == 0
                                  ? ChessConstants.BoardBrushWhite
                                  : ChessConstants.BoardBrushBlack;
                    Rectangle rect = new Rectangle(tileSize.X * x, tileSize.Y * y,
                        tileSize.X, tileSize.Y);
                    g.FillRectangle(brush, rect);
                }
            }
        }

        private void DrawSelectedPiece(Graphics g)
        {
            Point boardSize = ChessConstants.BoardSize;
            Point tileSize = ChessConstants.TileSize;
            List<BoardPoint> moves = _selectedPiece.GetValidMoves();
            foreach (BoardPoint point in moves)
            {
                Rectangle rect = new Rectangle(tileSize.X * point.X, tileSize.Y * point.Y,
                    tileSize.X, tileSize.Y);
                g.FillRectangle(Brushes.DarkOliveGreen, rect);
            }
        }

        public void Draw(Graphics g)
        {
            DrawBoard(g);
            if (_selectedPiece != null)
                DrawSelectedPiece(g);

            foreach (Piece p in _pieces)
                p.Draw(g);
        }
    }
}
