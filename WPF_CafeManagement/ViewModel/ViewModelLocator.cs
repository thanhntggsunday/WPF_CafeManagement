namespace WPF_CafeManagement.ViewModel
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel
        {
            get { return new MainWindowViewModel(); }
        }

        public TableAddNewFoodDialogViewModel TableAddNewFoodDialogViewModel
        {
            get { return new TableAddNewFoodDialogViewModel(); }
        }

        public TableWindowViewModel TableWindowViewModel
        {
            get { return new TableWindowViewModel(); }
        }
        

        public AdminWindowViewModel AdminWindowViewModel
        {
            get { return new AdminWindowViewModel(); }
        }
    }
}