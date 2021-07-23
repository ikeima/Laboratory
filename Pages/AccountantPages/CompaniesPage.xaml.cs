using Laboratory.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Laboratory.Pages.AccountantPages
{
    /// <summary>
    /// Логика взаимодействия для CompaniesPage.xaml
    /// </summary>
    public partial class CompaniesPage : Page
    {
        public CompaniesPage()
        {
            InitializeComponent();

            companiesList.ItemsSource = ApiOperation.GetCompanies();
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            companiesList.ItemsSource = from c in ApiOperation.GetCompanies()
                                        where (c.Title.Contains(searchBox.Text) || c.BIC.Contains(searchBox.Text)
                                       || c.TIN.Contains(searchBox.Text) || c.Adress.Contains(searchBox.Text)
                                       || c.Settlement_account.Contains(searchBox.Text))
                                        select c;
        }
    }
}
