using Laboratory.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Laboratory.Pages.AccountantPages
{
    /// <summary>
    /// Логика взаимодействия для AccountantPage.xaml
    /// </summary>
    public partial class AccountantPage : Page
    {
        public AccountantPage()
        {
            InitializeComponent();
        }

        private void OpenCompaniesPage(object sender, RoutedEventArgs e)
        {
            Navigation.Frame.Navigate(new CompaniesPage());
        }

        private void OpenAccountsPage(object sender, RoutedEventArgs e)
        {
            Navigation.Frame.Navigate(new AccountsPage());
        }
    }
}
