using System;
using System.Drawing;
using System.Windows.Forms;

namespace WINFORMS_DZ_07
{
    public static class CustomizationOptions
    {
        public static event EventHandler OptionChanged;

        private static Font _editorFont = new Font(FontFamily.GenericSansSerif, 10f);
        public static Font EditorFont
        {
            get => new Font(_editorFont, _editorFont.Style);
            set
            {
                _editorFont = new Font(value, value.Style);
                OptionChanged?.Invoke(null, new EventArgs());
            }
        }

        private static Font _textFont = new Font(FontFamily.GenericMonospace, 10f);
        public static Font TextFont
        {
            get => new Font(_textFont, _textFont.Style);
            set
            {
                _textFont = new Font(value, value.Style);
                OptionChanged?.Invoke(null, new EventArgs());
            }
        }

        private static Color _foreColor = Color.Black;
        public static Color ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                OptionChanged?.Invoke(null, new EventArgs());
            }
        }

        private static Color _backColor = Color.White;
        public static Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                OptionChanged?.Invoke(null, new EventArgs());
            }
        }

        public static void ApplyTo(Control control, bool textFont = false)
        {
            if (textFont)
                control.Font = new Font(TextFont, TextFont.Style);
            else
                control.Font = new Font(EditorFont, EditorFont.Style);
            control.ForeColor = ForeColor;
            control.BackColor = BackColor;
        }
    }
}
