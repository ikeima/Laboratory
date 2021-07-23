using Laboratory.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Laboratory.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void OpenHistoryPage(object sender, RoutedEventArgs e)
        {
            Navigation.Frame.Navigate(new HistoryPage());
        }

    }
}
