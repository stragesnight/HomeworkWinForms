using System;
using System.Drawing;
using System.Windows.Forms;

namespace WINFORMS_DZ_15
{
    public partial class MainForm : Form
    {
        private string _currentFileNameProxy = String.Empty;
        private string _currentFileName
        {
            get => _currentFileNameProxy;
            set 
            {
                _currentFileNameProxy = value;
                UpdateStatusStrip();
            }
        }

        private bool _hasUnsavedChangesProxy = false;
        private bool _hasUnsavedChanges
        {
            get => _hasUnsavedChangesProxy;
            set
            {
                _hasUnsavedChangesProxy = value;
                UpdateStatusStrip();
            }
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeFileMenuStrip();
            InitializeEditMenuStrip();
            InitializeEditContextMenuFor(textBox1);
            InitializeStatusStrip();
            InitializeCustomizeMenu();
            InitializeAboutMenu();
            ApplyCustomizationOptions(this, new EventArgs());

            textBox1.TextChanged += new EventHandler(textBo1_TextChanged);
            textBox1.KeyDown += new KeyEventHandler((s, e) => UpdateStatusStrip());
            textBox1.MouseDown += new MouseEventHandler((s, e) => UpdateStatusStrip());
            this.Resize += new EventHandler(Form1_Resize);
            this.FormClosed += new FormClosedEventHandler(HandleClosedForm);

            CustomizationOptions.OptionChanged += new EventHandler(ApplyCustomizationOptions);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "ДЗ №15, Шелест";
            textBox1.ScrollBars = ScrollBars.Both;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            textBox1.Size = new Size(this.Size.Width - 40, this.Size.Height - 90);
        }

        private void textBo1_TextChanged(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
            UpdateStatusStrip();
        }

        private void ApplyCustomizationOptions(object sender, EventArgs e)
        {
            CustomizationOptions.ApplyTo(this);
            CustomizationOptions.ApplyTo(textBox1, true);
            CustomizationOptions.ApplyTo(menuStrip1);
            CustomizationOptions.ApplyTo(statusStrip1);
            Form1_Resize(this, new EventArgs());
        }
    }
}
