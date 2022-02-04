using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using LoginForm.Database1DataSetTableAdapters;

namespace LoginForm
{
    public partial class RegistrationForm : Form
    {
        private Point _prevMousePosition;

        public RegistrationForm()
        {
            InitializeComponent();
            label1.MouseMove += label1_MouseMove;
            label1.MouseDown += label1_MouseDown;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _prevMousePosition.X;
                this.Top += e.Y - _prevMousePosition.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            _prevMousePosition = e.Location;
        }

        private bool RegisterUser(string login, string pass, string name, string surname)
        {
            UsersTableAdapter usersTable = new UsersTableAdapter();
            usersTable.Connection.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO [users] ([login], [password], [name], [surname]) " +
                "VALUES (@l, @p, @n, @s)",
                usersTable.Connection);

            cmd.Parameters.Add("@l", SqlDbType.NVarChar).Value = login;
            cmd.Parameters.Add("@p", SqlDbType.NVarChar).Value = pass;
            cmd.Parameters.Add("@n", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@s", SqlDbType.NVarChar).Value = surname;

            int res = cmd.ExecuteNonQuery();

            if (res != 1) 
            {
                MessageBox.Show("Внутренняя ошибка", "Ошибка при регистрации");
                return false;
            }

            return true;
        }

        private bool LoginPresentInDatabase(string login)
        {
            try
            {
                UsersTableAdapter usersTable = new UsersTableAdapter();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM [users] "
                    + "WHERE [login] = @l",
                    usersTable.Connection
                );
                cmd.Parameters.Add("@l", SqlDbType.NVarChar).Value = login;

                return cmd.ExecuteReader().HasRows;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string surname = textBoxSurname.Text;
            string login = textBoxLogin.Text;
            string pass = textBoxPass.Text;
            string pass2 = textBoxPass2.Text;
            string errmsg = "";

            bool success = true;

            if (name == String.Empty || name == textBoxName.HintText)
            {
                errmsg += "Имя обязательно\n";
                success = false;
            }

            if (name.Length > 32)
            {
                errmsg += "Длина имени должна быть меньше 32-х символов\n";
                success = false;
            }


            if (surname == String.Empty || surname == textBoxSurname.HintText)
            {
                errmsg += "Фамилия обязательна\n";
                success = false;
            }

            if (surname.Length > 32)
            {
                errmsg += "Длина фамилии должна быть меньше 32-х символов\n";
                success = false;
            }

            if (login == String.Empty || login == textBoxLogin.HintText)
            {
                errmsg += "Логин обязателен\n";
                success = false;
            }

            if (login.Length > 32)
            {
                errmsg += "Длина логина должна быть меньше 32-х символов\n";
                success = false;
            }

            if (pass.Length < 8 || pass.Length > 32 || pass == textBoxPass.HintText)
            {
                errmsg += "Длина пароля должна быть диапазоне от 8-ми до 32-х символов\n";
                success = false;
            }

            if (pass != pass2 || pass2 == textBoxPass2.HintText)
            {
                errmsg += "Пароли должны совпадать";
                success = false;
            }

            if (LoginPresentInDatabase(login))
            {
                errmsg += "Пользователь с таким логином уже зарегистрирован";
                success = false;
            }

            if (!success)
            {
                MessageBox.Show(errmsg, "Ошибка при регистрации");
                return;
            }

            if (RegisterUser(login, pass, name, surname))
                MessageBox.Show("Регистрация прошла успешно");
        }

        private void clickableLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clickableLabel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
