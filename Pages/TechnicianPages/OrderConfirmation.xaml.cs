using Laboratory.Classes;
using Laboratory.DbModel;
using Laboratory.Pages.LaborantsPages;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Laboratory.Pages.TechnicianPages
{
    /// <summary>
    /// Логика взаимодействия для OrderConfirmation.xaml
    /// </summary>
    public partial class OrderConfirmation : Page
    {
        private int orderId;
        public OrderConfirmation(int orderId)
        {
            InitializeComponent();

            this.orderId = orderId;
            patientsComboBox.ItemsSource = ApiOperation.GetPatients();
            servicesComboBox.ItemsSource = ApiOperation.GetServices();
            datePicker.SelectedDate = DateTime.Now;
        }
        private void CreateOrder(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedPatient = patientsComboBox.SelectedItem as Patients;
            var selectedService = servicesComboBox.SelectedItem as Services;

            if (selectedPatient != null && selectedService != null)
            {
                int days = 0;

                if (!int.TryParse(completeDaysTextBox.Text, out days))
                    MessageBox.Show("Введите только числа!");
                else
                {
                    // Создание и заполнения нового заказа
                    var newOrder = new Orders
                    {
                        Order_id = orderId,
                        Patient_id = selectedPatient.Patient_id,
                        Complete_time_in_days = days,
                        Create_date = (DateTime)datePicker.SelectedDate,
                    };

                    var patientService = new Patients_services
                    {
                        Patient_id = selectedPatient.Patient_id,
                        Service_code = selectedService.Service_code,
                    };

                    string jsonOrder = JsonConvert.SerializeObject(newOrder);
                    string jsonPatientService = JsonConvert.SerializeObject(patientService);

                    ApiOperation.Post(jsonOrder, "orders");
                    ApiOperation.Post(jsonPatientService, "patientsServices");

                    MessageBox.Show("Успешно создано!");
                    Navigation.Frame.Navigate(new TechnicianPage());
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }


    }
}
