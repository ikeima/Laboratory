using iTextSharp.text;
using iTextSharp.text.pdf;
using Laboratory.Classes;
using Laboratory.DbModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Laboratory.Pages.AccountantPages
{
    /// <summary>
    /// Логика взаимодействия для AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        public AddAccountWindow()
        {
            InitializeComponent();

            companiesComboBox.ItemsSource = ApiOperation.GetCompanies();
        }

        private void CancelAndClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var company = companiesComboBox.SelectedItem as Insurance_companies;
           
            if (company == null)
            {
                System.Windows.MessageBox.Show("Выберите страховую компанию!");
            }
            else if ((this.beginDate.SelectedDate > this.endDate.SelectedDate) || (this.beginDate.SelectedDate == null || this.endDate.SelectedDate == null))
            {
                System.Windows.MessageBox.Show("Выберите правильные даты!");
            }
            else
            {
                DateTime beginDate = (DateTime)this.beginDate.SelectedDate;
                DateTime endDate = (DateTime)this.endDate.SelectedDate;

                var patients = ApiOperation.GetPatients();
                var services = ApiOperation.GetServices();
                var patientsServices = ApiOperation.GetPatientsServices();

                var result = from p in patients
                             join ps in patientsServices on p.Patient_id equals ps.Patient_id
                             join s in services on ps.Service_code equals s.Service_code
                             where p.Insurance_company_id == company.Insurance_company_id
                             select new
                             {
                                 PatientId = p.Patient_id,
                                 Cost = s.Cost
                             };

                var patientsId = from p in patients
                                 join ps in patientsServices on p.Patient_id equals ps.Patient_id
                                 where p.Insurance_company_id == company.Insurance_company_id && ps.Patient_id == p.Patient_id
                                 select new
                                 {
                                     PatientId = p.Patient_id,
                                 };

                var servicesSum = from p in patients
                                  join ps in patientsServices on p.Patient_id equals ps.Patient_id
                                  join s in services on ps.Service_code equals s.Service_code
                                  where p.Insurance_company_id == company.Insurance_company_id
                                  select new
                                  {
                                      PatientId = p.Patient_id,
                                      ServiceSum = s.Cost
                                  };

                var distinct = patientsId.Distinct();
                Dictionary<int, decimal> patientsServicesSum = new Dictionary<int, decimal>();

                foreach (var item in distinct)
                {
                    patientsServicesSum.Add(result.Where(p => p.PatientId == item.PatientId).FirstOrDefault().PatientId, result.Where(p => p.PatientId == item.PatientId).Sum(p => p.Cost));
                }

                var accountant = ApiOperation.GetAccountants().Where(a => a.User_id == CurrentUser.User.User_id).FirstOrDefault();
                var lastAccount = ApiOperation.GetAccounts().OrderByDescending(a => a.Account_id).FirstOrDefault();

                int accountNumber = Convert.ToInt32(lastAccount.Number) + 1;
                Accounts newAccount = new Accounts()
                {
                    Accountant_id = accountant.Accountant_id,
                    Insurance_company_id = company.Insurance_company_id,
                    Number = accountNumber.ToString()
                };

                string json = JsonConvert.SerializeObject(newAccount);
                ApiOperation.Post(json, "accounts");

                AccountantOperation.GeneratePDF(patientsServicesSum, company, beginDate, endDate, newAccount);

                this.Close();
            }
        }

        
       
    }
}
