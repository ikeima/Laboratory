
using Laboratory.Classes;
using Laboratory.Pages.TechnicianPages;
using System.Windows.Controls;

namespace Laboratory.Pages.LaborantsPages
{
    /// <summary>
    /// Логика взаимодействия для LaborantPage.xaml
    /// </summary>
    public partial class TechnicianPage : Page
    {
        public TechnicianPage()
        {
            InitializeComponent();
        }

        private void OpenBiomaterialAddPage(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigation.Frame.Navigate(new BarcodePage());
        }

        private void OpenPatientsListPage(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigation.Frame.Navigate(new PatientsListPage());
        }

        private void OpenServicesPage(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigation.Frame.Navigate(new ServicesPage());
        }
    }
}
