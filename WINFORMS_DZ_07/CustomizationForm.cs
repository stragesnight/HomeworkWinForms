using System;
using System.Drawing;
using System.Windows.Forms;

namespace WINFORMS_DZ_07
{
    public partial class CustomizationForm : Form
    {
        private Font _editorFont;
        private Font _textFont;
        private Color _foreColor;
        private Color _backColor;

        public CustomizationForm()
        {
            InitializeComponent();
            CustomizationOptions.ApplyTo(this);

            _editorFont = CustomizationOptions.EditorFont;
            _textFont = CustomizationOptions.TextFont;
            _foreColor = CustomizationOptions.ForeColor;
            _backColor = CustomizationOptions.BackColor;
        }

        private void Customization_Load(object sender, EventArgs e)
        {
            groupBox1.Text = "Customization";
            label1.Text = "Editor font";
            label2.Text = "Foreground color";
            label3.Text = "Background color";
            label4.Text = "Text font";
            button1.Text = "Cancel";
            button2.Text = "OK";
            button3.Text = "Choose font...";
            button4.Text = "Choose color...";
            button5.Text = "Choose color...";
            button6.Text = "Choose font...";

            textBox1.Text = "This is example text. You can type here\n"
                + " to test the new feel of the text editor.\n";

            CustomizationOptions.OptionChanged += new EventHandler(ApplyCustomizationOptions);
        }

        private void ApplyCustomizationOptions(object sender, EventArgs e)
        {
            CustomizationOptions.ApplyTo(this);
            CustomizationOptions.ApplyTo(textBox1, true);
            CustomizationOptions.ApplyTo(groupBox1);
        }

        private void ApplyStyle()
        {
            this.Font = new Font(_editorFont, _editorFont.Style);
            this.ForeColor = _foreColor;
            this.BackColor = _backColor;

            textBox1.Font = new Font(_textFont, _textFont.Style);
            textBox1.ForeColor = _foreColor;
            textBox1.BackColor = _backColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomizationOptions.EditorFont = _editorFont;
            CustomizationOptions.TextFont = _textFont;
            CustomizationOptions.ForeColor = _foreColor;
            CustomizationOptions.BackColor = _backColor;

            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                _editorFont = new Font(fd.Font, fd.Font.Style);
                ApplyStyle();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                _foreColor = cd.Color;
                ApplyStyle();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                _backColor = cd.Color;
                ApplyStyle();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                _textFont = new Font(fd.Font, fd.Font.Style);
                ApplyStyle();
            }
        }
    }
}
