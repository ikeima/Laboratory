using Laboratory.Classes;
using Laboratory.DbModel;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Windows;

namespace Laboratory.Pages.TechnicianPages
{
    /// <summary>
    /// Логика взаимодействия для PatientInformationWindow.xaml
    /// </summary>
    public partial class PatientInformationWindow : Window
    {
        public PatientInformationWindow(string patientId)
        {
            InitializeComponent();

            var patient = ApiOperation.GetPatient(patientId);

            var company = ApiOperation.GetCompanies().Find(c => c.Insurance_company_id == patient.Insurance_company_id);
            companieTextBlock.Text = company.Title;
            patientData.DataContext = patient;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
