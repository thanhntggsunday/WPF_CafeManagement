using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WPF_CafeManagement.Common;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.Service;

namespace WPF_CafeManagement.ViewModel
{
    public class AdminWindowViewModel : BaseViewModel
    {
        #region Command

        public ICommand GoDetailsCmd { get; private set; }
        public ICommand SaveCategoryCommand { get; private set; }
        public ICommand AddCategoryCommand { get; private set; }
        public ICommand CancelEditeCategoryCommand { get; private set; }
        public ICommand EditCategoryCommand { get; private set; }
        public ICommand DeleteCategoryCommand { get; private set; }
        public ICommand StatisticalBillCommand { get; private set; }
        public ICommand SearchCategoryCommand { get; private set; }

        public ICommand SaveTableCommand { get; private set; }
        public ICommand AddTableCommand { get; private set; }
        public ICommand CancelEditeTableCommand { get; private set; }
        public ICommand EditTableCommand { get; private set; }
        public ICommand DeleteTableCommand { get; private set; }
        public ICommand SearchTableCommand { get; private set; }

        public ICommand SaveFoodCommand { get; private set; }
        public ICommand AddFoodCommand { get; private set; }
        public ICommand CancelEditeFoodCommand { get; private set; }
        public ICommand EditFoodCommand { get; private set; }
        public ICommand DeleteFoodCommand { get; private set; }
        public ICommand SearchFoodCommand { get; private set; }

        public ICommand SaveUserCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand CancelEditeUserCommand { get; private set; }
        public ICommand EditUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }
        public ICommand SearchUserCommand { get; private set; }

        #endregion Command

        #region Fields

        private bool _isEdit = false;
        private List<Category> _categories;
        private Category _categorySelected;
        private Category _categorySelectedEdit = new Category();
        private Visibility _isVisibleBtnSave;
        private Visibility _isVisibleBtnAdd;
        private Visibility _isVisibleBtnEdit;
        private Visibility _isVisibleBtnCancel;
        private List<Bill> _billList;
        private string _txtCategoryName;

        private bool _isEditTable = false;
        private List<Table> _tables;
        private Table _tableSelected;
        private Table _tableSelectedEdit = new Table();
        private Visibility _isVisibleBtnSaveTable;
        private Visibility _isVisibleBtnAddTable;
        private Visibility _isVisibleBtnEditTable;
        private Visibility _isVisibleBtnCancelTable;
        private string _txtTableName;

        private bool _isEditFood = false;
        private List<Food> _foods;
        private Food _foodSelected;
        private Food _foodSelectedEdit = new Food();
        private Visibility _isVisibleBtnSaveFood;
        private Visibility _isVisibleBtnAddFood;
        private Visibility _isVisibleBtnEditFood;
        private Visibility _isVisibleBtnCancelFood;
        private string _txtFoodName;
        private Category _foodCategorySelected;

        private bool _isEditUser = false;
        private List<Account> _users;
        private Account _userSelected;
        private Account _userSelectedEdit = new Account();
        private Visibility _isVisibleBtnSaveUser;
        private Visibility _isVisibleBtnAddUser;
        private Visibility _isVisibleBtnEditUser;
        private Visibility _isVisibleBtnCancelUser;
        private string _txtUserName;

        #endregion Fields

        public AdminWindowViewModel()
        {
            Init();
        }

        #region Properities

        #region Category

        public List<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged("Categories");
                }
            }
        }

        public Category CategorySelected
        {
            get { return _categorySelected; }
            set
            {
                if (_categorySelected != value)
                {
                    _categorySelected = value;
                    OnPropertyChanged("CategorySelected");
                }
            }
        }

        public Category CategorySelectedEdit
        {
            get { return _categorySelectedEdit; }
            set
            {
                if (_categorySelectedEdit != value)
                {
                    _categorySelectedEdit = value;
                    OnPropertyChanged("CategorySelectedEdit");
                }
            }
        }

        public Visibility IsVisibleBtnSave
        {
            get { return _isVisibleBtnSave; }
            set
            {
                if (_isVisibleBtnSave != value)
                {
                    _isVisibleBtnSave = value;
                    OnPropertyChanged("IsVisibleBtnSave");
                }
            }
        }

        public Visibility IsVisibleBtnAdd
        {
            get { return _isVisibleBtnAdd; }
            set
            {
                if (_isVisibleBtnAdd != value)
                {
                    _isVisibleBtnAdd = value;
                    OnPropertyChanged("IsVisibleBtnAdd");
                }
            }
        }

        public Visibility IsVisibleBtnEdit
        {
            get { return _isVisibleBtnEdit; }
            set
            {
                if (_isVisibleBtnEdit != value)
                {
                    _isVisibleBtnEdit = value;
                    OnPropertyChanged("IsVisibleBtnEdit");
                }
            }
        }

        public Visibility IsVisibleBtnCancel
        {
            get { return _isVisibleBtnCancel; }
            set
            {
                if (_isVisibleBtnCancel != value)
                {
                    _isVisibleBtnCancel = value;
                    OnPropertyChanged("IsVisibleBtnCancel");
                }
            }
        }

        private DateTime _dtpStart = DateTime.Now;

        public DateTime DtpStart
        {
            get { return _dtpStart; }
            set
            {
                _dtpStart = value;
                OnPropertyChanged("DtpStart");
            }
        }

        private DateTime _dtpEnd = DateTime.Now;

        public DateTime DtpEnd
        {
            get { return _dtpEnd; }
            set
            {
                _dtpEnd = value;
                OnPropertyChanged("DtpEnd");
            }
        }

        public List<Bill> BillList
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

        public string TxtCategoryName
        {
            get { return _txtCategoryName; }
            set
            {
                if (_txtCategoryName != value)
                {
                    _txtCategoryName = value;
                    OnPropertyChanged("TxtCategoryName");
                }
            }
        }

        #endregion Category

        #region Table

        public List<Table> Tables
        {
            get => _tables;
            set
            {
                if (_tables != value)
                {
                    _tables = value;
                    OnPropertyChanged("Tables");
                }
            }
        }

        public Table TableSelected
        {
            get => _tableSelected;
            set
            {
                if (_tableSelected != value)
                {
                    _tableSelected = value;
                    OnPropertyChanged("TableSelected");
                }
            }
        }

        public Table TableSelectedEdit
        {
            get => _tableSelectedEdit;
            set
            {
                if (_tableSelectedEdit != value)
                {
                    _tableSelectedEdit = value;
                    OnPropertyChanged("TableSelectedEdit");
                }
            }
        }

        public Visibility IsVisibleBtnSaveTable
        {
            get => _isVisibleBtnSaveTable;
            set
            {
                if (_isVisibleBtnSaveTable != value)
                {
                    _isVisibleBtnSaveTable = value;
                    OnPropertyChanged("IsVisibleBtnSaveTable");
                }
            }
        }

        public Visibility IsVisibleBtnAddTable
        {
            get => _isVisibleBtnAddTable;
            set
            {
                if (_isVisibleBtnAddTable != value)
                {
                    _isVisibleBtnAddTable = value;
                    OnPropertyChanged("IsVisibleBtnAddTable");
                }
            }
        }

        public Visibility IsVisibleBtnEditTable
        {
            get => _isVisibleBtnEditTable;
            set
            {
                if (_isVisibleBtnEditTable != value)
                {
                    _isVisibleBtnEditTable = value;
                    OnPropertyChanged("IsVisibleBtnEditTable");
                }
            }
        }

        public Visibility IsVisibleBtnCancelTable
        {
            get => _isVisibleBtnCancelTable;
            set
            {
                if (_isVisibleBtnCancelTable != value)
                {
                    _isVisibleBtnCancelTable = value;
                    OnPropertyChanged("IsVisibleBtnCancelTable");
                }
            }
        }

        public string TxtTableName
        {
            get => _txtTableName;
            set
            {
                if (_txtTableName != value)
                {
                    _txtTableName = value;
                    OnPropertyChanged("TxtTableName");
                }
            }
        }

        #endregion Table

        #region Food

        public List<Food> Foods
        {
            get => _foods;
            set
            {
                if (_foods != value)
                {
                    _foods = value;
                    OnPropertyChanged("Foods");
                }
            }
        }

        public Food FoodSelected
        {
            get => _foodSelected;
            set
            {
                if (_foodSelected != value)
                {
                    _foodSelected = value;
                    OnPropertyChanged("FoodSelected");
                }
            }
        }

        public Food FoodSelectedEdit
        {
            get => _foodSelectedEdit;
            set
            {
                if (_foodSelectedEdit != value)
                {
                    _foodSelectedEdit = value;
                    OnPropertyChanged("FoodSelectedEdit");
                }
            }
        }

        public Visibility IsVisibleBtnSaveFood
        {
            get => _isVisibleBtnSaveFood;
            set
            {
                if (_isVisibleBtnSaveFood != value)
                {
                    _isVisibleBtnSaveFood = value;
                    OnPropertyChanged("IsVisibleBtnSaveFood");
                }
            }
        }

        public Visibility IsVisibleBtnAddFood
        {
            get => _isVisibleBtnAddFood;
            set
            {
                if (_isVisibleBtnAddFood != value)
                {
                    _isVisibleBtnAddFood = value;
                    OnPropertyChanged("IsVisibleBtnAddFood");
                }
            }
        }

        public Visibility IsVisibleBtnEditFood
        {
            get => _isVisibleBtnEditFood;
            set
            {
                if (_isVisibleBtnEditFood != value)
                {
                    _isVisibleBtnEditFood = value;
                    OnPropertyChanged("IsVisibleBtnEditFood");
                }
            }
        }

        public Visibility IsVisibleBtnCancelFood
        {
            get => _isVisibleBtnCancelFood;
            set
            {
                if (_isVisibleBtnCancelFood != value)
                {
                    _isVisibleBtnCancelFood = value;
                    OnPropertyChanged("IsVisibleBtnCancelFood");
                }
            }
        }

        public string TxtFoodName
        {
            get => _txtFoodName;
            set
            {
                if (_txtFoodName != value)
                {
                    _txtFoodName = value;
                    OnPropertyChanged("TxtFoodName");
                }
            }
        }

        public Category FoodCategorySelected
        {
            get { return _foodCategorySelected; }
            set
            {
                if (_foodCategorySelected != value)
                {
                    _foodCategorySelected = value;
                    OnPropertyChanged("FoodCategorySelected");
                }
            }
        }

        #endregion Food

        #region Users
        public List<Account> Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged("Users");
                }
            }
        }

        public Account UserSelected
        {
            get => _userSelected;
            set
            {
                if (_userSelected != value)
                {
                    _userSelected = value;
                    OnPropertyChanged("UserSelected");
                }
            }
        }

        public Account UserSelectedEdit
        {
            get => _userSelectedEdit;
            set
            {
                if (_userSelectedEdit != value)
                {
                    _userSelectedEdit = value;
                    OnPropertyChanged("UserSelectedEdit");
                }
            }
        }

        public Visibility IsVisibleBtnSaveUser
        {
            get => _isVisibleBtnSaveUser;
            set
            {
                if (_isVisibleBtnSaveUser != value)
                {
                    _isVisibleBtnSaveUser = value;
                    OnPropertyChanged("IsVisibleBtnSaveUser");
                }
            }
        }
        public Visibility IsVisibleBtnAddUser
        {
            get => _isVisibleBtnAddUser;
            set
            {
                if (_isVisibleBtnAddUser != value)
                {
                    _isVisibleBtnAddUser = value;
                    OnPropertyChanged("IsVisibleBtnAddUser");
                }
            }
        }
        public Visibility IsVisibleBtnEditUser
        {
            get => _isVisibleBtnEditUser;
            set
            {
                if (_isVisibleBtnEditUser != value)
                {
                    _isVisibleBtnEditUser = value;
                    OnPropertyChanged("IsVisibleBtnEditUser");
                }
            }
        }
        public Visibility IsVisibleBtnCancelUser
        {
            get => _isVisibleBtnCancelUser;
            set
            {
                if (_isVisibleBtnCancelUser != value)
                {
                    _isVisibleBtnCancelUser = value;
                    OnPropertyChanged("IsVisibleBtnCancelUser");
                }
            }
        }
        public string TxtUserName
        {
            get => _txtUserName;
            set
            {
                if (_txtUserName != value)
                {
                    _txtUserName = value;
                    OnPropertyChanged("TxtUserName");
                }
            }
        }
        #endregion

        #endregion Properities

        #region Methodes

        private void GoDetails(object o)
        {
        }

        private void Init()
        {
            // Category
            GoDetailsCmd = new RelayCommand(GoDetails, CanExec);
            AddCategoryCommand = new RelayCommand(AddCategory, CanExec);
            EditCategoryCommand = new RelayCommand(EditCategory, CanExec);
            SaveCategoryCommand = new RelayCommand(SaveCategory, CanExec);
            CancelEditeCategoryCommand = new RelayCommand(CancelEditeCategory, CanExec);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory, CanExec);
            StatisticalBillCommand = new RelayCommand(StatisticalBill, CanExec);
            SearchCategoryCommand = new RelayCommand(SearchCategoryByName, CanExec);

            GetAllCategories();
            IsVisibleBtnAdd = Visibility.Visible;
            IsVisibleBtnSave = Visibility.Hidden;
            IsVisibleBtnEdit = Visibility.Visible;
            IsVisibleBtnCancel = Visibility.Hidden;

            // Table
            IsVisibleBtnAddTable = Visibility.Visible;
            IsVisibleBtnSaveTable = Visibility.Hidden;
            IsVisibleBtnEditTable = Visibility.Visible;
            IsVisibleBtnCancelTable = Visibility.Hidden;

            AddTableCommand = new RelayCommand(AddTable, CanExec);
            EditTableCommand = new RelayCommand(EditTable, CanExec);
            SaveTableCommand = new RelayCommand(SaveTable, CanExec);
            CancelEditeTableCommand = new RelayCommand(CancelEditeTable, CanExec);
            DeleteTableCommand = new RelayCommand(DeleteTable, CanExec);
            SearchTableCommand = new RelayCommand(SearchTableByName, CanExec);
            GetAllTables();

            // Food
            IsVisibleBtnAddFood = Visibility.Visible;
            IsVisibleBtnSaveFood = Visibility.Hidden;
            IsVisibleBtnEditFood = Visibility.Visible;
            IsVisibleBtnCancelFood = Visibility.Hidden;

            AddFoodCommand = new RelayCommand(AddFood, CanExec);
            EditFoodCommand = new RelayCommand(EditFood, CanExec);
            SaveFoodCommand = new RelayCommand(SaveFood, CanExec);
            CancelEditeFoodCommand = new RelayCommand(CancelEditFood, CanExec);
            DeleteFoodCommand = new RelayCommand(DeleteFood, CanExec);
            SearchFoodCommand = new RelayCommand(SearchFoodByName, CanExec);
            GetAllFoods();

            // Users
            IsVisibleBtnAddUser = Visibility.Visible;
            IsVisibleBtnSaveUser = Visibility.Hidden;
            IsVisibleBtnEditUser = Visibility.Visible;
            IsVisibleBtnCancelUser = Visibility.Hidden;

            AddUserCommand = new RelayCommand(AddUser, CanExec);
            EditUserCommand = new RelayCommand(EditUser, CanExec);
            SaveUserCommand = new RelayCommand(SaveUser, CanExec);
            CancelEditeUserCommand = new RelayCommand(CancelEditUser, CanExec);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanExec);
            SearchUserCommand = new RelayCommand(SearchUserByName, CanExec);
            GetAllUsers();
        }

        #region Bill

        private void GetBillsListByDateRange(DateTime dtStart, DateTime dtEnd)
        {
            BillList = BillService.Instance.GetBillListByDate(dtStart, dtEnd);
        }

        private void StatisticalBill(object o)
        {
            try
            {
                GetBillsListByDateRange(DtpStart, DtpEnd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion Bill

        #region Category

        private void SaveCategory(object o)
        {
            try
            {
                IsVisibleBtnAdd = Visibility.Visible;
                IsVisibleBtnSave = Visibility.Hidden;
                IsVisibleBtnEdit = Visibility.Visible;
                IsVisibleBtnCancel = Visibility.Hidden;

                var category = o as Category;

                if (_isEdit)
                {
                    CategoryService.Instance.UpdateCategory(category);
                }
                else
                {
                    CategoryService.Instance.InsertCategory(category);
                }

                _isEdit = false;
                GetAllCategories();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditCategory(object o)
        {
            try
            {
                _isEdit = true;
                IsVisibleBtnAdd = Visibility.Hidden;
                IsVisibleBtnSave = Visibility.Visible;
                IsVisibleBtnEdit = Visibility.Hidden;
                IsVisibleBtnCancel = Visibility.Visible;

                if (CategorySelected != null)
                {
                    CategorySelectedEdit = new Category(CategorySelected.Id, CategorySelected.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddCategory(object o)
        {
            try
            {
                _isEdit = false;
                IsVisibleBtnAdd = Visibility.Hidden;
                IsVisibleBtnSave = Visibility.Visible;
                IsVisibleBtnEdit = Visibility.Hidden;
                IsVisibleBtnCancel = Visibility.Visible;

                CategorySelectedEdit = new Category();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CancelEditeCategory(object o)
        {
            try
            {
                _isEdit = false;
                IsVisibleBtnAdd = Visibility.Visible;
                IsVisibleBtnSave = Visibility.Hidden;
                IsVisibleBtnEdit = Visibility.Visible;
                IsVisibleBtnCancel = Visibility.Hidden;

                CategorySelectedEdit = new Category();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DeleteCategory(object o)
        {
            try
            {
                CategorySelectedEdit = new Category(CategorySelected.Id, CategorySelected.Name);

                var result = MessageBox.Show("You are sure delete item?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                _isEdit = false;
                IsVisibleBtnAdd = Visibility.Visible;
                IsVisibleBtnSave = Visibility.Hidden;
                IsVisibleBtnEdit = Visibility.Visible;
                IsVisibleBtnCancel = Visibility.Hidden;

                if (result == MessageBoxResult.Yes)
                {
                    CategoryService.Instance.DeleteCategory(CategorySelectedEdit.Id);

                    CategorySelectedEdit = new Category();
                    GetAllCategories();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void GetAllCategories()
        {
            try
            {
                Categories = CategoryService.Instance.GetListCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SearchCategoryByName(object o)
        {
            try
            {
                Categories = CategoryService.Instance.GetListCategoryByName(TxtCategoryName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Category

        #region Table

        private void SaveTable(object o)
        {
            try
            {
                IsVisibleBtnAddTable = Visibility.Visible;
                IsVisibleBtnSaveTable = Visibility.Hidden;
                IsVisibleBtnEditTable = Visibility.Visible;
                IsVisibleBtnCancelTable = Visibility.Hidden;

                var table = o as Table;

                if (_isEditTable)
                {
                    TableService.Instance.UpdateTable(table);
                }
                else
                {
                    table.Status = Constants.TABLE_EMPTY_STATUS;
                    TableService.Instance.InsertTable(table);
                }

                _isEditTable = false;
                GetAllTables();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditTable(object o)
        {
            try
            {
                _isEditTable = true;
                IsVisibleBtnAddTable = Visibility.Hidden;
                IsVisibleBtnSaveTable = Visibility.Visible;
                IsVisibleBtnEditTable = Visibility.Hidden;
                IsVisibleBtnCancelTable = Visibility.Visible;

                if (TableSelected != null)
                {
                    TableSelectedEdit = new Table(TableSelected.Id, TableSelected.Name, TableSelected.Status);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddTable(object o)
        {
            try
            {
                _isEdit = false;
                IsVisibleBtnAddTable = Visibility.Hidden;
                IsVisibleBtnSaveTable = Visibility.Visible;
                IsVisibleBtnEditTable = Visibility.Hidden;
                IsVisibleBtnCancelTable = Visibility.Visible;

                TableSelectedEdit = new Table();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CancelEditeTable(object o)
        {
            try
            {
                _isEdit = false;
                IsVisibleBtnAddTable = Visibility.Visible;
                IsVisibleBtnSaveTable = Visibility.Hidden;
                IsVisibleBtnEditTable = Visibility.Visible;
                IsVisibleBtnCancelTable = Visibility.Hidden;

                TableSelectedEdit = new Table();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DeleteTable(object o)
        {
            try
            {
                TableSelectedEdit = new Table(TableSelected.Id, TableSelected.Name, TableSelected.Status);

                var result = MessageBox.Show("You are sure delete item?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                _isEdit = false;
                IsVisibleBtnAdd = Visibility.Visible;
                IsVisibleBtnSave = Visibility.Hidden;
                IsVisibleBtnEdit = Visibility.Visible;
                IsVisibleBtnCancel = Visibility.Hidden;

                if (result == MessageBoxResult.Yes)
                {
                    TableService.Instance.DeleteTable(TableSelectedEdit.Id);
                    TableSelectedEdit = new Table();
                    GetAllTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void GetAllTables()
        {
            try
            {
                Tables = TableService.Instance.LoadTableList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SearchTableByName(object o)
        {
            try
            {
                Tables = TableService.Instance.GetManyTableByName(TxtTableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Table

        #region Food

        private void SaveFood(object o)
        {
            try
            {
                IsVisibleBtnAddFood = Visibility.Visible;
                IsVisibleBtnSaveFood = Visibility.Hidden;
                IsVisibleBtnEditFood = Visibility.Visible;
                IsVisibleBtnCancelFood = Visibility.Hidden;

                var food = o as Food;

                if (_isEditFood)
                {
                    FoodService.Instance.UpdateFood(food.Id, food.Name, food.CategoryId, food.Price);
                }
                else
                {
                    FoodService.Instance.InsertFood(food.Name, FoodCategorySelected.Id, food.Price);
                }

                _isEditFood = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditFood(object o)
        {
            try
            {
                _isEditFood = true;
                IsVisibleBtnAddFood = Visibility.Hidden;
                IsVisibleBtnSaveFood = Visibility.Visible;
                IsVisibleBtnEditFood = Visibility.Hidden;
                IsVisibleBtnCancelFood = Visibility.Visible;

                if (FoodSelected != null)
                {
                    FoodSelectedEdit = new Food(FoodSelected.Id, FoodSelected.Name, FoodSelected.CategoryId, FoodSelected.Price);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddFood(object o)
        {
            try
            {
                _isEdit = false;
                IsVisibleBtnAddFood = Visibility.Hidden;
                IsVisibleBtnSaveFood = Visibility.Visible;
                IsVisibleBtnEditFood = Visibility.Hidden;
                IsVisibleBtnCancelFood = Visibility.Visible;

                FoodSelectedEdit = new Food();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CancelEditFood(object o)
        {
            try
            {
                _isEdit = false;
                IsVisibleBtnAddTable = Visibility.Visible;
                IsVisibleBtnSaveTable = Visibility.Hidden;
                IsVisibleBtnEditTable = Visibility.Visible;
                IsVisibleBtnCancelTable = Visibility.Hidden;

                TableSelectedEdit = new Table();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DeleteFood(object o)
        {
            try
            {
                FoodSelectedEdit = new Food(FoodSelected.Id, FoodSelected.Name, FoodSelected.CategoryId, FoodSelected.Price);

                var result = MessageBox.Show("You are sure delete item?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

                _isEditFood = false;
                IsVisibleBtnAddFood = Visibility.Visible;
                IsVisibleBtnSaveFood = Visibility.Hidden;
                IsVisibleBtnEditFood = Visibility.Visible;
                IsVisibleBtnCancelFood = Visibility.Hidden;

                if (result == MessageBoxResult.Yes)
                {
                    FoodService.Instance.DeleteFood(FoodSelectedEdit.Id);

                    FoodSelectedEdit = new Food();
                    GetAllFoods();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void GetAllFoods()
        {
            try
            {
                // Tables = TableService.Instance.LoadTableList();
                Foods = FoodService.Instance.GetListFood();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SearchFoodByName(object o)
        {
            try
            {
                Foods = FoodService.Instance.SearchFoodByName(TxtFoodName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Food

        #region Account

        private void SaveUser(object o)
        {
            try
            {
                IsVisibleBtnAddUser = Visibility.Visible;
                IsVisibleBtnSaveUser = Visibility.Hidden;
                IsVisibleBtnEditUser = Visibility.Visible;
                IsVisibleBtnCancelUser = Visibility.Hidden;

                var acc = o as Account;

                if (_isEditUser)
                {
                    AccountService.Instance.UpdateAccount(acc.UserName, acc.DisplayName, acc.Type);
                }
                else
                {
                    AccountService.Instance.InsertAccount(acc.UserName, acc.DisplayName, acc.Type);
                }

                _isEditFood = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditUser(object o)
        {
            try
            {
                _isEditUser = true;
                IsVisibleBtnAddUser = Visibility.Hidden;
                IsVisibleBtnSaveUser = Visibility.Visible;
                IsVisibleBtnEditUser = Visibility.Hidden;
                IsVisibleBtnCancelUser = Visibility.Visible;

                if (UserSelected != null)
                {
                    UserSelectedEdit = new Account(UserSelected.UserName, UserSelected.DisplayName, UserSelected.Type, UserSelected.Password);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddUser(object o)
        {
            try
            {
                _isEditUser = true;
                IsVisibleBtnAddUser = Visibility.Hidden;
                IsVisibleBtnSaveUser = Visibility.Visible;
                IsVisibleBtnEditUser = Visibility.Hidden;
                IsVisibleBtnCancelUser = Visibility.Visible;

                UserSelectedEdit = new Account();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CancelEditUser(object o)
        {
            try
            {
                _isEditUser = false;
                IsVisibleBtnAddUser = Visibility.Visible;
                IsVisibleBtnSaveUser = Visibility.Hidden;
                IsVisibleBtnEditUser = Visibility.Visible;
                IsVisibleBtnCancelUser = Visibility.Hidden;

                UserSelectedEdit = new Account();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DeleteUser(object o)
        {
            try
            {
                UserSelectedEdit = new Account(UserSelected.UserName, UserSelected.DisplayName, UserSelected.Type, UserSelected.Password);

                var result = MessageBox.Show("You are sure delete item?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

                _isEditUser = false;
                IsVisibleBtnAddUser = Visibility.Visible;
                IsVisibleBtnSaveUser = Visibility.Hidden;
                IsVisibleBtnEditUser = Visibility.Visible;
                IsVisibleBtnCancelUser = Visibility.Hidden;

                if (result == MessageBoxResult.Yes)
                {
                    AccountService.Instance.DeleteAccount(UserSelectedEdit.UserName);

                    UserSelectedEdit = new Account();
                    GetAllUsers();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void GetAllUsers()
        {
            try
            {
                // Tables = TableService.Instance.LoadTableList();
                Users = AccountService.Instance.GetListAccount();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SearchUserByName(object o)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Food

        #endregion Methodes
    }
}