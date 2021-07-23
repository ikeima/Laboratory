using Laboratory.Classes;
using Laboratory.DbModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Laboratory.Pages.TechnicianPages
{
    /// <summary>
    /// Логика взаимодействия для BarcodePage.xaml
    /// </summary>
    public partial class BarcodePage : Page
    {
        public BarcodePage()
        {
            InitializeComponent();
            
            // Установка последнего номера заказа + 1
            int barcode = ApiOperation.GetOrders().LastOrDefault().Order_id + 1;
            barcodeBox.Text = barcode.ToString();
        }
        
        private void OrderConfirmationPageOpen(object sender, RoutedEventArgs e)
        {
            bool allDigit = barcodeBox.Text.All(c => char.IsDigit(c));

            if (!string.IsNullOrWhiteSpace(barcodeBox.Text) && allDigit)
            {
                bool isExist = ApiOperation.GetOrders().Exists(o => o.Order_id == Convert.ToInt32(barcodeBox.Text));

                if (!isExist)
                {
                    Navigation.Frame.Navigate(new OrderConfirmation(Convert.ToInt32(barcodeBox.Text)));
                }
                else
                {
                    MessageBox.Show("Заказ с таким номером уже существует!");
                }
            }
            else
            {
                MessageBox.Show("Введите номер заказа!");
            }
        }
    }
}
