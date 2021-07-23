using Laboratory.Classes;
using Laboratory.Pages.LaborantsPages;
using System;
using System.Windows;

namespace Laboratory
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Navigation.Frame = frame;
        }
        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            Navigation.Frame.GoBack();
        }

        private void FrameContentRender(object sender, System.EventArgs e)
        {
            if (Navigation.Frame.CanGoBack)
            {
                backButton.Visibility = Visibility.Visible;
                logoutButton.Visibility = Visibility.Visible;
            }
            else
            {
                backButton.Visibility = Visibility.Hidden;
                logoutButton.Visibility = Visibility.Hidden;
                ClearUserData();
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            while (Navigation.Frame.CanGoBack)
                Navigation.Frame.GoBack();
            ClearUserData();
            Navigation.Frame.RemoveBackEntry();
        }

        private void ClearUserData()
        {
            lastName.Text = "";
            firstName.Text = "";
            roleTextBlock.Text = "";
            
        }
    }
}
