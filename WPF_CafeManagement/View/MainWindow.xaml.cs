using System.Windows;
using WPF.MDI;
using WPF_CafeManagement.Common;

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

            if (AppStaticDataManagement.TableWindowsList.Count == 0)
            {
                var varWindows = new MdiChild()
                {
                    Title = "Tables",
                    Height = 300,
                    Width = 300,
                    Content = new TableWindow()
                };

                MainMdiContainer.Children.Add(varWindows);
                AppStaticDataManagement.TableWindowsList.Add(varWindows);
            }
            else
            {
                var varWindows = AppStaticDataManagement.TableWindowsList[0];

                foreach (var item in MainMdiContainer.Children)
                {
                    var mdi = item.Content as TableWindow;
                    if (mdi != null)
                    {
                        MainMdiContainer.Children.Remove(item);
                        break;
                    }
                }

                MainMdiContainer.Children.Add(varWindows);
            }
        }

        private void MenuItemAdmin_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.TileHorizontal;

            if (AppStaticDataManagement.AdminWindowsList.Count == 0)
            {
                var varWindows = new MdiChild()
                {
                    Title = "Tables",
                    Height = 300,
                    Width = 300,
                    Content = new AdminWindow()
                };

                MainMdiContainer.Children.Add(varWindows);
                AppStaticDataManagement.AdminWindowsList.Add(varWindows);
            }
            else
            {
                var varWindows = AppStaticDataManagement.AdminWindowsList[0];

                foreach (var item in MainMdiContainer.Children)
                {
                    var mdi = item.Content as AdminWindow;
                    if (mdi != null)
                    {
                        MainMdiContainer.Children.Remove(item);
                        break;
                    }
                }

                MainMdiContainer.Children.Add(varWindows);
            }

            MainMdiContainer.MdiLayout = MdiLayout.Cascade;
        }

        private void MenuItemTable_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.TileHorizontal;

            if (AppStaticDataManagement.TableWindowsList.Count == 0)
            {
                var varWindows = new MdiChild()
                {
                    Title = "Tables",
                    Height = 300,
                    Width = 300,
                    Content = new TableWindow()
                };

                MainMdiContainer.Children.Add(varWindows);
                AppStaticDataManagement.TableWindowsList.Add(varWindows);
            }
            else
            {
                var varWindows = AppStaticDataManagement.TableWindowsList[0];

                foreach (var item in MainMdiContainer.Children)
                {
                    var mdi = item.Content as TableWindow;
                    if (mdi != null)
                    {
                        MainMdiContainer.Children.Remove(item);
                        break;
                    }
                }

                MainMdiContainer.Children.Add(varWindows);
            }

            MainMdiContainer.MdiLayout = MdiLayout.Cascade;
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