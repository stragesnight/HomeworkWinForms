using System;

namespace LoginForm
{
    public class User
    {
        public int id { get; private set; }
        public string login { get; private set; }
        public string password { get; private set; }
        public string name { get; private set; }
        public string surname { get; private set; }

        public User(int id, string login, string password, string name, string surname)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
        }

        public User(object[] data)
        {
            id = (int)data[0];
            login = (string)data[1];
            password = (string)data[2];
            name = (string)data[3];
            surname = (string)data[4];
        }

        public bool IsAdmin()
        {
            return login == "db_admin";
        }

        public override string ToString()
        {
            return $"{login} ({surname} {name})";
        }
    }
}
