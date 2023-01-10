using Sikreys;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserApp;

namespace Sikreys
{
    public partial class AdminPageWindow : Window
    {
        ApplicationContext db;
        string email = "";
        public AdminPageWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
            List<User> users = db.Users.ToList();
            listOfUsers.ItemsSource = users;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadio = (RadioButton)e.Source;
            email = selectedRadio.Content.ToString();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (email == "")
            {
                MessageBox.Show("Вы никого не выбрали");
                return;
            }
            User user = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.Where(b => b.Email == email).FirstOrDefault();
                if (user.Email == "admin@gmail.com")
                {
                    MessageBox.Show("Вы не можете удалить основной админский аккаунт");
                    return;
                }
                db.Users.Remove(user);
                db.SaveChanges();
            }
            List<User> users = db.Users.ToList();
            listOfUsers.ItemsSource = users;
        }
    }
}
