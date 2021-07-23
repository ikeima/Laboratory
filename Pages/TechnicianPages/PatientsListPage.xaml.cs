using Laboratory.Classes;
using Laboratory.DbModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Laboratory.Pages.TechnicianPages
{
    /// <summary>
    /// Логика взаимодействия для PatientsListPage.xaml
    /// </summary>
    public partial class PatientsListPage : Page
    {
        public PatientsListPage()
        {
            InitializeComponent();
            patientsList.ItemsSource = ApiOperation.GetPatients();
        }

        private void OpenAddPatientPage(object sender, System.Windows.RoutedEventArgs e)
        {
            AddPatientWindow windows = new AddPatientWindow();
            windows.ShowDialog();
            patientsList.ItemsSource = ApiOperation.GetPatients();
        }

        private bool direction = false;
        private void SlideMenu(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!direction)
            {
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(0, 0, -228, 0);
                anim.To = new Thickness(0, 0, 0, 0);
                anim.Duration = TimeSpan.FromSeconds(0.5);
                panel.BeginAnimation(MarginProperty, anim);
                menuButton.Content = "→";

                direction = true;
            }
            else
            {
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(0, 0, 0, 0);
                anim.To = new Thickness(0, 0, -228, 0);
                anim.Duration = TimeSpan.FromSeconds(0.5);
                panel.BeginAnimation(MarginProperty, anim);
                menuButton.Content = "←";

                direction = false;
            }
        }
        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            Patients patient = patientsList.SelectedItem as Patients;

            if (patient != null)
            {
                if (MessageBox.Show("Удалить пациента и все данные о его услугах?", "Удаление пациента", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ApiOperation.Delete("patients", patient.Patient_id.ToString());
                    patientsList.ItemsSource = ApiOperation.GetPatients();
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента!");
            }
        }



        private void OpenEditPatientPage(object sender, RoutedEventArgs e)
        {
            Patients patient = patientsList.SelectedItem as Patients;

            if (patient != null)
            {
                EditPatientWindow window = new EditPatientWindow(patient.Patient_id.ToString());
                window.ShowDialog();
                patientsList.ItemsSource = ApiOperation.GetPatients();
            }
            else
            {
                MessageBox.Show("Выберите пациента!");
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var search = from p in ApiOperation.GetPatients()
                         where (p.Last_name.Contains(searchBox.Text) || p.First_name.Contains(searchBox.Text)
                         || p.Patronymic.Contains(searchBox.Text) || p.Passport_number_series.Contains(searchBox.Text)
                         || p.Email.Contains(searchBox.Text) || p.Phone.Contains(searchBox.Text) || p.Insurance_companies.Title.Contains(searchBox.Text))
                         select p;

            if (search.Count() == 0)
            {
                MessageBox.Show("Результатов не найдено");
                patientsList.ItemsSource = ApiOperation.GetPatients();
                searchBox.Text = "";
            }
            else
            {
                patientsList.ItemsSource = search.ToList();

            }
        }

        private void DetailInformationOpen(object sender, RoutedEventArgs e)
        {
            Patients patient = patientsList.SelectedItem as Patients;

            if (patient != null)
            {
                PatientInformationWindow window = new PatientInformationWindow(patient.Patient_id.ToString());
                window.ShowDialog();
            }

            else
                MessageBox.Show("Выберите пациента!");


        }
    }
}
