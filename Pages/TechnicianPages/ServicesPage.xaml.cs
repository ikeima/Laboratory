using Laboratory.Classes;
using System.Linq;
using System.Windows.Controls;

namespace Laboratory.Pages.TechnicianPages
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();

            servicesList.ItemsSource = ApiOperation.GetServices();
            sortComboBox.SelectedIndex = 0;
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            servicesList.ItemsSource = from s in ApiOperation.GetServices()
                                       where (s.Title.Contains(searchBox.Text) || (s.Service_code.ToString().Contains(searchBox.Text)))
                                       select s;
        }
        private void SortingByCost(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)sortComboBox.SelectedItem;
            string direction = item.Content.ToString();

            if (direction == "Убыванию")
            {
                servicesList.ItemsSource = from s in ApiOperation.GetServices()
                                           orderby s.Cost descending
                                           select s;
            }
            else
            {
                servicesList.ItemsSource = from s in ApiOperation.GetServices()
                                           orderby s.Cost ascending
                                           select s;
            }
        }
    }
}
