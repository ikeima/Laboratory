using Laboratory.Classes;
using Laboratory.DbModel;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Windows;

namespace Laboratory.Pages.TechnicianPages
{
    /// <summary>
    /// Логика взаимодействия для EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : Window
    {
        public EditPatientWindow(string patientId)
        {
            InitializeComponent();
            var patient = ApiOperation.GetPatient(patientId);

            companyComboBox.SelectedIndex = 0;
            companyComboBox.ItemsSource = ApiOperation.GetCompanies();

            patientData.DataContext = patient;
            passportSeriesTextBox.Text = patient.Passport_number_series.Substring(0, 4);
            passportNumberTextBox.Text = patient.Passport_number_series.Substring(3, 6);
        }
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            string passportNumber = passportSeriesTextBox.Text + passportNumberTextBox.Text;
            Patients patient = patientData.DataContext as Patients;
            patient.Passport_number_series = passportNumber;
            if (DataCheck(passportNumber))
            {
                string json = JsonConvert.SerializeObject(patient, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                
                ApiOperation.Put(json, "patients", patient.Patient_id.ToString());

                MessageBox.Show("Изменения сохранены");
                this.Close();
            }
            else
            {
                MessageBox.Show("Провертье правильность введенных данных и заполните все поля!");
            }
        }
        private bool DataCheck(string passportNumber)
        {
            foreach (char c in phoneTextBox.Text)
            {
                if (char.IsLetter(c))
                    return false;
            }
            foreach (char c in passportNumber)
            {
                if (char.IsLetter(c))
                    return false;
            }
            foreach (char c in insuranceTextBox.Text)
            {
                if (char.IsLetter(c))
                    return false;
            }
            if (firstNameTextBlock.Text == "" || lastNameTextBlock.Text == "" || emailTextBox.Text == "" || phoneTextBox.Text == "" || birthDatePicker.SelectedDate == null || insuranceTextBox.Text == "" || passportNumber == "")
            {
                return false;
            }
            else if (firstNameTextBlock.Text.Length > 20 || lastNameTextBlock.Text.Length > 50 || patronymicNameTextBlock.Text.Length > 50 || emailTextBox.Text.Length > 40 || insuranceTextBox.Text.Length != 16 || passportNumber.Length != 10 || phoneTextBox.Text.Length > 12)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CancelAndClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
