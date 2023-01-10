using Sikreys;
using System;
using System.Collections.Generic;
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
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            textBoxLogin.ToolTip = "";
            textBoxLogin.Background = Brushes.Transparent;
            passBox.ToolTip = "";
            passBox.Background = Brushes.Transparent;

            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();

            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Слишком короткий логин";
                textBoxLogin.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                passBox.ToolTip = "Слишком короткий пароль";
                passBox.Background = Brushes.DarkRed;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();
                }

                if (authUser != null)
                {
                    if (authUser.Login == "admin" && authUser.Pass == "76211" && authUser.Email == "admin@gmail.com")
                    {
                        AdminPageWindow mainWindow = new AdminPageWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        UserPageWindow mainWindow = new UserPageWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Вы ввели что-то некорректно");
            }
        }

        private void Button_Window_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
