using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserApp;

namespace Sikreys
{
    static class Extend
    {
        public static bool TestEmail(this string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith(".") || email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                return true;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address != trimmedEmail;
            }
            catch
            {
                return true;
            }
        }
    }

    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 200;
            btnAnimation.Duration = TimeSpan.FromSeconds(1);
            regButton.BeginAnimation(Button.WidthProperty, btnAnimation);
        }

        

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            textBoxLogin.ToolTip = "";
            textBoxLogin.Background = Brushes.Transparent;
            passBox.ToolTip = "";
            passBox.Background = Brushes.Transparent;
            passBox_2.ToolTip = "";
            passBox_2.Background = Brushes.Transparent;
            textBoxEmail.ToolTip = "";
            textBoxEmail.Background = Brushes.Transparent;

            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
            string pass_2 = passBox_2.Password.Trim();
            string email = textBoxEmail.Text.Trim().ToLower();

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
            else if (pass != pass_2)
            {
                passBox_2.ToolTip = "Пароли не совпадают";
                passBox_2.Background = Brushes.DarkRed;
            }
            else if (email.TestEmail())
            {
                textBoxEmail.ToolTip = "Некоректная электронная почта";
                textBoxEmail.Background = Brushes.DarkRed;
            }
            else
            {

                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(b => b.Email == email).FirstOrDefault();
                }

                if (authUser != null)
                {
                    textBoxEmail.ToolTip = "Такая почта уже существует";
                    textBoxEmail.Background = Brushes.DarkRed;
                    return;
                }

                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                passBox_2.ToolTip = "";
                passBox_2.Background = Brushes.Transparent;
                textBoxEmail.ToolTip = "";
                textBoxEmail.Background = Brushes.Transparent;

                User user = new User(login, pass, email);
                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Успешная регистрация!");

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                this.Close();
            }
        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            authWindow.Show();
            this.Close();
        }
    }
}