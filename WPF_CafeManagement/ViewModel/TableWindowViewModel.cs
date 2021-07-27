using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WPF_CafeManagement.Common;
using WPF_CafeManagement.Common.Messaging;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.Service;
using WPF_CafeManagement.View;

namespace WPF_CafeManagement.ViewModel
{
    public class TableWindowViewModel : BaseViewModel
    {
        private IEventAggregator _eventAggregator;

        #region Commands

        public ICommand GetTableStatusToEmptyCommand { get; private set; }
        public ICommand GetTableStatusToFullCommand { get; private set; }
        public ICommand GetTableAddNewFoodCommand { get; private set; }
        public ICommand CheckoutCommand { get; private set; }

        #endregion

        public TableWindowViewModel()
        {
            GetInits();
        }

        #region Fields

        private List<Menu> _billList;
        private string _tableCurrentId = "";
        private CboKeyValue _tableSelected;
        private ObservableCollection<CboKeyValue> _tablesList;

        #endregion

        #region Properties

        public ObservableCollection<CommandWrapper> MyCommands { get; set; }
        public List<Table> Tables { get; set; }

        public CboKeyValue TableSelected
        {
            get { return _tableSelected; }
            set
            {
                if (_tableSelected != value)
                {
                    _tableSelected = value;
                    OnPropertyChanged("TableSelected");
                    int id = 0;
                    if (value != null)
                    {
                        Int32.TryParse(value.Key, out id);
                        ShowBill(id);
                    }
                    else
                    {
                        BillList = null;
                       
                    }
                }
            }
        }

        public ObservableCollection<CboKeyValue> TablesList
        {
            get { return _tablesList; }
            set
            {
                if (_tablesList != value)
                {
                    _tablesList = value;
                    OnPropertyChanged("TablesList");
                }
            }
        }

        public List<Menu> BillList
        {
            get { return _billList; }
            set
            {
                if (_billList != value)
                {
                    _billList = value;
                    OnPropertyChanged("BillList");
                }
            }
        }


        public ObservableCollection<CboKeyValue> CategoriesList
        {
            get; set;
        }

        public ObservableCollection<CboKeyValue> FoodsList
        {
            get;
            set;
        }

        #endregion

        #region Methodes

        private void GetInits()
        {
            MyCommands = new ObservableCollection<CommandWrapper>();
            _tablesList = new ObservableCollection<CboKeyValue>();

            InitCommands();
            GetInitControls();
        }

        private void InitCommands()
        {
            GetTableStatusToFullCommand = new RelayCommand(UpdateTableStatusToFull, CanExec);
            GetTableStatusToEmptyCommand = new RelayCommand(UpdateTableStatusToEmpty, CanExec);
            GetTableAddNewFoodCommand = new RelayCommand(GetTableAddNewFood, CanExec);
            CheckoutCommand = new RelayCommand(Checkout, CanExec);
        }

        private void GetInitControls()
        {
            _eventAggregator = App.EventAggregator;
            LoadData();
            GetButtonForDisplay();
            GetTableSelectedById();
        }

        private void GetButtonForDisplay()
        {
            MyCommands.Clear();

            if (Tables != null)
            {
                int count = 0;

                foreach (Table item in Tables)
                {
                    count++;

                    var button = new CommandWrapper
                    {
                        DisplayName = item.Name,
                        Command = new RelayCommand(ExecTodo, CanExec),
                        Status = item.Status,
                        Id = item.Id.ToString(CultureInfo.InvariantCulture)
                    };

                    if (button.Status == Constants.TABLE_EMPTY_STATUS)
                    {
                        button.BgColor = new SolidColorBrush(Color.FromArgb(100, 100, 200, 10));
                    }
                    else
                    {
                        button.BgColor = new SolidColorBrush(Color.FromArgb(100, 100, 10, 10));
                    }

                    //if (count == 3 || count == 4)
                    //{
                    //    button.BgColor = new SolidColorBrush(Color.FromArgb(100, 100, 10, 10));
                    //}

                    MyCommands.Add(button);
                }
            }
        }

        private void LoadData()
        {
            Tables = TableService.Instance.LoadTableList();
            var categories = CategoryService.Instance.GetListCategory();
            var foods = FoodService.Instance.GetListFood();
            CategoriesList = new ObservableCollection<CboKeyValue>();
            FoodsList = new ObservableCollection<CboKeyValue>();

            if (categories != null)
            {
                foreach (var item in categories)
                {
                    CategoriesList.Add(new CboKeyValue() { Key = item.Id.ToString(CultureInfo.InvariantCulture), Value = item.Name });
                }

            }

            if (foods != null)
            {
                foreach (var item in foods)
                {
                    FoodsList.Add(new CboKeyValue() { Key = item.Id.ToString(CultureInfo.InvariantCulture), Value = item.Name });
                }
            }

            TableAddNewFoodDialogViewModel.StaticCategoriesList = CategoriesList;
            TableAddNewFoodDialogViewModel.StaticFoodsList = FoodsList;
            TableAddNewFoodDialogViewModel.StaticTableList = TablesList;

            GetCmbTables();
        }

        private void GetCmbTables()
        {
            _tablesList.Clear();

            if (Tables != null)
            {
                foreach (Table tbl in Tables)
                {
                    var item = new CboKeyValue { Key = tbl.Id.ToString(CultureInfo.InvariantCulture), Value = tbl.Name };

                    _tablesList.Add(item);
                }
            }
        }

        private void ShowBill(int id)
        {
            BillList = MenuService.Instance.GetListMenuByTable(id);           
        }

        private void UpdateTableStatus(object id, bool isFull = true)
        {
            int intId = 0;
            var strId = id as String;

            Int32.TryParse(strId, out intId);
            _tableCurrentId = strId;
            TableService.Instance.UpdateTableStatus(intId, isFull);
            GetInitControls();
        }

        private void GetTableSelectedById()
        {
            if (_tablesList != null && !String.IsNullOrEmpty(_tableCurrentId))
            {
                foreach (CboKeyValue item in _tablesList)
                {
                    if (item.Key == _tableCurrentId)
                    {
                        TableSelected = item;
                    }
                }
            }
        }

        private void GetTableAddNewFood(object o)
        {
            if (TableSelected != null)
            {
                Table tableSelected = Tables.FirstOrDefault(item => item.Id.ToString(CultureInfo.InvariantCulture) == TableSelected.Key);

                if (tableSelected != null)
                {
                    var tableAddNewFood = new TableAddNewFoodDialog(TableSelected.Key);

                    tableAddNewFood.ShowDialog();
                    int id = 0;
                    Int32.TryParse(TableSelected.Key, out id);
                    ShowBill(id);

                }
            }
        }

        #endregion

        #region Command delegate

      
        //This goes in Initialization/constructor
        private void ExecTodo(object obj)
        {
            var id = obj as string;

            if (TablesList != null)
            {
                foreach (CboKeyValue item in TablesList)
                {
                    if (item.Key == id)
                    {
                        TableSelected = item;
                        break;
                    }
                }
            }
        }

        private void UpdateTableStatusToEmpty(object o)
        {
            if (TableSelected != null)
            {
                string id = TableSelected.Key;
                UpdateTableStatus(id, false);
            }
        }

        private void UpdateTableStatusToFull(object o)
        {
            if (TableSelected != null)
            {
                string id = TableSelected.Key;
                UpdateTableStatus(id, true);
            }
        }

        private void Checkout(object o)
        {
            try
            {
                if (TableSelected != null)
                {
                    int id = 0;
                    Int32.TryParse(TableSelected.Key, out id);

                    int billId = BillService.Instance.GetUncheckBillIdByTableId(id);

                    BillService.Instance.CheckOut(billId);

                    UpdateTableStatusToEmpty(new object());
                    GetInitControls();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        #endregion
    }
}