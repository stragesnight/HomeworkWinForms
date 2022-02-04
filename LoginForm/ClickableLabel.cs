using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoginForm
{
    public class ClickableLabel : Label
    {
        public Color MouseOverBackColor { get; set; }
        public Color MouseOverForeColor { get; set; }
        public Color MouseClickBackColor { get; set; }
        public Color MouseClickForeColor { get; set; }

        private Color _defaultBackColor;
        private Color _defaultForeColor;

        public ClickableLabel() : base()
        {
            base.Layout += OnStart;
        }

        private void OnStart(object sender, EventArgs e)
        {
            base.Layout -= OnStart;

            _defaultBackColor = base.BackColor;
            _defaultForeColor = base.ForeColor;

            base.MouseEnter += OnMouseEnter;
            base.MouseLeave += OnMouseLeave;
            base.MouseDown += OnMouseDown;
            base.MouseUp += OnMouseUp;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            base.BackColor = MouseOverBackColor;
            base.ForeColor = MouseOverForeColor;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            base.BackColor = _defaultBackColor;
            base.ForeColor = _defaultForeColor;
        }

        private void OnMouseDown(object sender, EventArgs e)
        {
            base.BackColor = MouseClickBackColor;
            base.ForeColor = MouseClickForeColor;
        }

        private void OnMouseUp(object sender, EventArgs e)
        {
            base.BackColor = MouseOverBackColor;
            base.ForeColor = MouseOverForeColor;
        }
    }
}
