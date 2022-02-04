using System;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class MainForm : Form
    {
        private User _user;

        public MainForm(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.usersTableAdapter.Fill(this.database1DataSet.Users);
            label2.Text = _user.ToString();
            buttonReg.Visible = _user.IsAdmin();
        }

        private void clickableLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            this.Hide();
            new RegistrationForm().ShowDialog();
            this.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new AuthForm().Show();
            this.Close();
        }

        private void clickableLabelRefresh_Click(object sender, EventArgs e)
        {
            this.usersTableAdapter.Fill(this.database1DataSet.Users);
        }
    }
}
