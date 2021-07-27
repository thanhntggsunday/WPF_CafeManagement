using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using WPF_CafeManagement.Common;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.Service;
using WPF_CafeManagement.View;

namespace WPF_CafeManagement.ViewModel
{
    public class TableAddNewFoodDialogViewModel : BaseViewModel
    {
        #region Commands

        public ICommand SaveCommand { get; private set; }

        #endregion

        public TableAddNewFoodDialogViewModel()
        {
            Maximize = false;
        }

        public TableAddNewFoodDialogViewModel(string tableId)
        {
            Maximize = false;
            InitCommand();

            _categoriesList = StaticCategoriesList;
            // _foodsList = StaticFoodsList;
            _tablesList = StaticTableList;

            if (_tablesList != null)
            {
                foreach (CboKeyValue item in _tablesList)
                {
                    if (item.Key == tableId)
                    {
                        TableSelected = item;
                    }
                }
            }
        }

        #region Fields

        public TableAddNewFoodDialog TableAddNewFoodDialog;
        private ObservableCollection<CboKeyValue> _categoriesList;
        private CboKeyValue _categorySelected;
        private string _foodCount;
        private CboKeyValue _foodSelected;
        private ObservableCollection<CboKeyValue> _foodsList;
        private CboKeyValue _tableSelected;
        private ObservableCollection<CboKeyValue> _tablesList;

        public static ObservableCollection<CboKeyValue> StaticCategoriesList { get; set; }
        public static ObservableCollection<CboKeyValue> StaticFoodsList { get; set; }
        public static ObservableCollection<CboKeyValue> StaticTableList { get; set; }

        #endregion

        #region Propeties

        public CboKeyValue TableSelected
        {
            get { return _tableSelected; }
            set
            {
                _tableSelected = value;
                OnPropertyChanged("TableSelected");
            }
        }

        public ObservableCollection<CboKeyValue> TablesList
        {
            get { return _tablesList; }
            set
            {
                _tablesList = value;
                OnPropertyChanged("TablesList");
            }
        }

        public CboKeyValue CategorySelected
        {
            get { return _categorySelected; }
            set
            {
                _categorySelected = value;
                OnPropertyChanged("CategorySelected");

                if (value != null)
                {
                    int id = 0;
                    Int32.TryParse(value.Key, out id);
                    LoadFoodListByCategoryId(id);
                }
                
            }
        }

        public ObservableCollection<CboKeyValue> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
                OnPropertyChanged("CategoriesList");
            }
        }

        public CboKeyValue FoodSelected
        {
            get { return _foodSelected; }
            set
            {
                _foodSelected = value;
                OnPropertyChanged("FoodSelected");
            }
        }

        public ObservableCollection<CboKeyValue> FoodsList
        {
            get { return _foodsList; }
            set
            {
                _foodsList = value;
                OnPropertyChanged("FoodsList");
            }
        }

        public string FoodCount
        {
            get { return _foodCount; }
            set
            {
                _foodCount = value;
                OnPropertyChanged("FoodCount");
            }
        }

        #endregion

        #region Methodes

        private bool CanExec(object obj)
        {
            return (true);
        }

        private void InitCommand()
        {
            SaveCommand = new RelayCommand(SaveData, CanExec);
        }

        private void SaveData(object o)
        {
            try
            {
                int tableId = 0;
                int foodId = 0;
                Int32.TryParse(TableSelected.Key, out tableId);

                int idBill = BillService.Instance.GetUncheckBillIdByTableId(tableId);

                if (FoodSelected != null)
                {
                    Int32.TryParse(FoodSelected.Key, out foodId);
                }

                int count = 0;
                Int32.TryParse(FoodCount, out count);

                if (idBill == -1)
                {
                    BillService.Instance.InsertBill(tableId);
                    BillInfoService.Instance.InsertBillInfo(BillService.Instance.GetMaxIdBill(), foodId, count);
                }
                else
                {
                    BillInfoService.Instance.InsertBillInfo(idBill, foodId, count);
                }

                this.TableAddNewFoodDialog.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void LoadFoodListByCategoryId(int id)
        {
            List<Food> listFood = FoodService.Instance.GetFoodByCategoryId(id);
            FoodsList = new ObservableCollection<CboKeyValue>();

            if (listFood != null)
            {
                foreach (var item in listFood)
                {
                    FoodsList.Add(new CboKeyValue() {Key = item.Id.ToString(CultureInfo.InvariantCulture), Value = item.Name});
                }
            }
        }

        #endregion
    }
}