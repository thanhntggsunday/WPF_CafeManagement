using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.MDI;

namespace WPF_CafeManagement.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Tables",
                Height = 300,
                Width = 300,               
                Content = new TableWindow()
            });
        }

        private void MenuItemAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();

            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Admin",
                Height = 300,
                Width = 300,
                Content = adminWindow
            });

        }

        private void MenuItemTable_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Tables",
                Height = 300,
                Width = 300,               
                Content = new TableWindow()
            });
        }

        private void MenuItemCloseAll_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.Children.Clear();
        }

        private void MenuItemCascade_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.Cascade;
        }

        private void MenuItemHorizontally_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.TileHorizontal;
        }

        private void MenuItemVatically_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.TileVertical;
        }
    }
}
