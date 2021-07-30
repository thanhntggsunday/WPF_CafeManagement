using System;
using System.Windows;
using WPF_CafeManagement.Service;
using WPF_CafeManagement.ViewModel;

namespace WPF_CafeManagement.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            new SplashWindow().ShowDialog();

            txtUsername.Text = "k9";
            txtPassword.Password = "1";

            var context = new LoginWindowViewModel();
            context.Maximize = false;
            this.DataContext = context;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = AccountService.Instance.Login(txtUsername.Text.Trim(), txtPassword.Password.ToString().Trim());

                if (result)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    // lbtResult.Text = "Email or password invalid!";
                    MessageBox.Show("Email or password invalid!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}