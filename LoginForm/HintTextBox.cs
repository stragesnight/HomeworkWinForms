using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoginForm
{
    public class HintTextBox : TextBox
    {
        private bool _firstInteraction = true;

        public string HintText { get; set; } = String.Empty;
        public new bool UseSystemPasswordChar { get; set; } = false;


        public HintTextBox() : base()
        {
            base.ParentChanged += OnStart;
            base.LostFocus += OnLostFocus_;
        }

        private void OnStart(object sender, EventArgs e)
        {
            base.Text = HintText;
            base.Font = new Font(base.Font, FontStyle.Italic);
            base.ForeColor = SystemColors.GrayText;
            base.UseSystemPasswordChar = false;

            base.Select(0, 0);

            base.MouseDown += OnFirstInteraction;
            base.KeyDown += OnFirstInteraction;

            _firstInteraction = false;
        }

        private void OnFirstInteraction(object sender, EventArgs e)
        {
            base.LostFocus -= OnLostFocus_;

            base.Text = String.Empty;
            base.Font = new Font(base.Font, FontStyle.Regular);
            base.ForeColor = SystemColors.ControlText;
            base.UseSystemPasswordChar = UseSystemPasswordChar;

            base.MouseDown -= OnFirstInteraction;
            base.KeyDown -= OnFirstInteraction;

            base.LostFocus += OnLostFocus_;
        }

        private void OnLostFocus_(object sender, EventArgs e)
        {
            if (_firstInteraction)
                return;

            if (base.Text == String.Empty)
                OnStart(this, new EventArgs());
        }
    }
}
