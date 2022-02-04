using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using LoginForm.Database1DataSetTableAdapters;

namespace LoginForm
{
    public partial class AuthForm : Form
    {
        private Point _prevMousePosition;

        public AuthForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string pass = textBoxPass.Text;

            DataTable dt = new DataTable();
            UsersTableAdapter usersTable = new UsersTableAdapter();
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM [users] " +
                "WHERE [login] = @l " +
                "AND [password] = @p ",
                usersTable.Connection
                
            );
            cmd.Parameters.Add("@l", SqlDbType.NVarChar).Value = login;
            cmd.Parameters.Add("@p", SqlDbType.NVarChar).Value = pass;

            usersTable.Adapter.SelectCommand = cmd;
            usersTable.Adapter.Fill(dt);

            if (dt.Rows.Count != 1)
            {
                MessageBox.Show($"Неправильный логин и/или пароль", "Ошибка при авторизации");
                return;
            }

            object[] data = dt.Rows[0].ItemArray;
            new MainForm(new User(data)).Show();
            this.Hide();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _prevMousePosition.X;
                this.Top += e.Y - _prevMousePosition.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            _prevMousePosition = new Point(e.X, e.Y);
        }

        private void clickableLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clickableLabel2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Обратитесь к системному администратору\n"
                    + "за помощью в регистрации нового пользователя в сети.");
        }
    }
}
