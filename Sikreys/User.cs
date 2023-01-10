using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserApp
{
    internal class User
    {
        public int id { get; set; }
        string login, pass, email;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        [DataType(DataType.Password)]
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        [EmailAddress]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public User() { }
        public User(string login, string pass, string email)
        {
            this.login = login;
            this.pass = pass;
            this.email = email;
        }
    }
}
