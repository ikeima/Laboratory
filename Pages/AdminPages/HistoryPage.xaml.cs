using Laboratory.Classes;
using Laboratory.DbModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Laboratory.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();

            historyDataGrid.ItemsSource = ApiOperation.GetHistory();
            loginComboBox.ItemsSource = ApiOperation.GetUsers();                
        }


            
   

        private void Filtration(object sender, SelectionChangedEventArgs e)
        {
            var history = ApiOperation.GetHistory();
            Users selectedUser = loginComboBox.SelectedItem as Users;

            history = history.FindAll(u => u.Users.Login == selectedUser.Login);

            if (history.Count() == 0)
            {
                MessageBox.Show("Нет результатов");
                historyDataGrid.ItemsSource = ApiOperation.GetHistory();
            }
            else
            {
                historyDataGrid.ItemsSource = history.ToList();

            }

        }

        private void ResetFiltration(object sender, System.Windows.RoutedEventArgs e)
        {
            historyDataGrid.ItemsSource = ApiOperation.GetHistory();
        }
    }
}
