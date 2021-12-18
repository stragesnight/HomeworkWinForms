using System;
using System.Drawing;

namespace WINFORMS_DZ_08
{
    public static class ChessConstants
    {
        public static readonly Size CanvasSize = new Size(512, 512);
        public static readonly Point TileSize = new Point(64, 64);
        public static readonly Point BoardSize = new Point(8, 8);

        public static readonly Brush BoardBrushWhite = Brushes.AntiqueWhite;
        public static readonly Brush BoardBrushBlack = Brushes.RosyBrown;

        public static readonly Brush PieceBrushWhite = Brushes.White;
        public static readonly Brush PieceBrushBlack = Brushes.Black;
    }
}
