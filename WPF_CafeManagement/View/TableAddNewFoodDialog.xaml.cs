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
using WPF_CafeManagement.ViewModel;

namespace WPF_CafeManagement.View
{
    /// <summary>
    /// Interaction logic for TableAddNewFoodDialog.xaml
    /// </summary>
    public partial class TableAddNewFoodDialog : Window
    {
        public TableAddNewFoodDialog(string tableId)
        {
            var tableAddNewFoodDialogViewModel = new TableAddNewFoodDialogViewModel(tableId);
            tableAddNewFoodDialogViewModel.TableAddNewFoodDialog = this;
            this.DataContext = tableAddNewFoodDialogViewModel;
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;

        }
    }
}
