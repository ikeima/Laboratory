using Laboratory.Classes;
using Laboratory.DbModel;
using Newtonsoft.Json;
using System.Windows;

namespace Laboratory.Pages.TechnicianPages
{
    /// <summary>
    /// Логика взаимодействия для AddPatientWindow.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        public AddPatientWindow()
        {
            InitializeComponent();

            companyComboBox.ItemsSource = ApiOperation.GetCompanies();
        }
        private void AddPatient(object sender, RoutedEventArgs e)
        {
            Insurance_companies selectedCompany = companyComboBox.SelectedItem as Insurance_companies;
            string passportNumber = passportSeriesTextBox.Text + passportNumberTextBox.Text;

            if (firstNameTextBlock.Text == "" || lastNameTextBlock.Text == "" || emailTextBox.Text == "" || phoneTextBox.Text == "" ||
                birthDatePicker.SelectedDate == null || insuranceTextBox.Text == "" || selectedCompany == null || passportNumber == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                var patient = new Patients()
                {
                    First_name = firstNameTextBlock.Text,
                    Last_name = lastNameTextBlock.Text,
                    Patronymic = patronymicNameTextBlock.Text,
                    Email = emailTextBox.Text,
                    Phone = phoneTextBox.Text,
                    Birth_date = (System.DateTime)birthDatePicker.SelectedDate,
                    Insurance_policy_number = insuranceTextBox.Text,
                    Insurance_company_id = selectedCompany.Insurance_company_id,
                    Passport_number_series = passportNumber
                };

                if (DataCheck(passportNumber))
                {
                    string jsonPatient = JsonConvert.SerializeObject(patient);
                    ApiOperation.Post(jsonPatient, "patients");

                    MessageBox.Show("Регистрация пациента успешно завершена!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Провертье правильность введенных данных и заполните все поля!");
                }
            }
        }

        private bool DataCheck(string passportNumber)
        {
            foreach(char c in phoneTextBox.Text)
            {
                if (char.IsLetter(c))
                    return false;
            }
            foreach (char c in passportNumber)
            {
                if (char.IsLetter(c))
                    return false;
            }
            foreach(char c in insuranceTextBox.Text)
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
