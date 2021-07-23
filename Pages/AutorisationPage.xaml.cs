using Laboratory.Classes;
using Laboratory.DbModel;
using Laboratory.Pages.AccountantPages;
using Laboratory.Pages.AdminPages;
using Laboratory.Pages.LaborantsPages;
using Newtonsoft.Json;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Laboratory.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutorisationPage.xaml
    /// </summary>
    public partial class AutorisationPage : Page
    {
        private int loginTry;

        public AutorisationPage()
        {
            InitializeComponent();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            var users = ApiOperation.GetUsers();

            if (users != null)
            {
                Users user = users.Find(u => u.Login == loginTextBox.Text && u.Password == passwordBox.Password);
                bool isSuccess;

                if (user != null)
                {
                    if (loginTry <= 0)
                    {
                        MessageBox.Show("Добро пожаловать!");
                        CurrentUser.User = user;
                        isSuccess = true;

                        SetCurrentUserData(user);
                        AddToHistory(user, isSuccess);
                        RedirectToPages();
                    }
                    else
                    {
                        if (Captcha.Text == captchaTextBox.Text)
                        {
                            CurrentUser.User = user;
                            isSuccess = true;

                            SetCurrentUserData(user);
                            RedirectToPages();
                        }
                        else
                        {
                            MessageBox.Show("Неправильно введена капча!");
                            GenerateCaptcha();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                    GenerateCaptcha();
                    loginTry++;

                    captchaTextBlock.Visibility = Visibility.Visible;
                    captchaTextBox.Visibility = Visibility.Visible;
                    updateCaptchaButton.Visibility = Visibility.Visible;
                }
            }
        }

        private static void SetCurrentUserData(Users user)
        {
            var form = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            form.lastName.Text = user.Last_name;
            form.firstName.Text = user.First_name;

            switch (user.Role_code)
            {
                case "A":
                    form.roleTextBlock.Text = ", Администратор";
                    break;
                case "AC":
                    form.roleTextBlock.Text = ", Бухгалтер";
                    break;
                case "T":
                    form.roleTextBlock.Text = ", Лаборант";
                    break;
                default:
                    break;
            }
        }

        private void RedirectToPages()
        {
            switch (CurrentUser.User.Role_code)
            {
                case "A":
                    Navigation.Frame.Navigate(new AdminPage());
                    break;
                case "AC":
                    Navigation.Frame.Navigate(new AccountantPage());
                    break;
                case "T":
                    Navigation.Frame.Navigate(new TechnicianPage());
                    break;
                default:
                    break;
            }

        }

        private void AddToHistory(Users user, bool isSucces)
        {
            History record = new History()
            {
                User_id = user.User_id,
                Enter_date_time = DateTime.Now,
                Is_succes = isSucces,
            };

            string json = JsonConvert.SerializeObject(record);
            ApiOperation.Post(json, "history");
        }

        private void GenerateCaptcha()
        {
            string captchaText = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            Captcha captcha = new Captcha(captchaText, 150, 50);

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapImage image = new BitmapImage();
                captcha.Image.Save(stream, ImageFormat.Jpeg);

                image.BeginInit();
                stream.Position = 0;
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();

                captchaImage.Source = image;
            }
        }

        private void UpdateCaptcha(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
