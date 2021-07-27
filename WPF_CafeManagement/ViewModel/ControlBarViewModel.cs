using System.CodeDom;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_CafeManagement.Common;
using WPF_CafeManagement.View.USC;

namespace WPF_CafeManagement.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region fields

        private bool _enableMaximize = true;

        #endregion fields

        #region properties

        public ControlBarUC controlBarUC { get; set; }

        public bool EnableMaximize
        {
            get => _enableMaximize;
            set
            {
                if (_enableMaximize != value)
                {
                    _enableMaximize = value;
                    OnPropertyChanged("EnableMaximize");
                }
            }
        }

        #endregion properties

        #region commands

        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }

        #endregion commands

        public ControlBarViewModel(ControlBarUC controlBarUC)
        {
            this.controlBarUC = controlBarUC;

            CloseWindowCommand = new RelayCommand((p) =>
            {
                var cbu = p as ControlBarUC;
                FrameworkElement window = GetWindowParent(cbu);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            }, CanExec);

            MaximizeWindowCommand = new RelayCommand((p) =>
            {
                var cbu = p as ControlBarUC;
                FrameworkElement window = GetWindowParent(cbu);
                var w = window as Window;

                var dataContext = w.DataContext as BaseViewModel;

                if (dataContext != null)
                {
                    EnableMaximize = dataContext.Maximize;
                }

                if (w != null && EnableMaximize)
                {
                    if (w.WindowState != WindowState.Maximized)
                        w.WindowState = WindowState.Maximized;
                    else
                        w.WindowState = WindowState.Normal;
                }

            }, CanExec);

            MinimizeWindowCommand = new RelayCommand((p) =>
            {
                var cbu = p as ControlBarUC;
                FrameworkElement window = GetWindowParent(cbu);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                        w.WindowState = WindowState.Minimized;
                    else
                        w.WindowState = WindowState.Maximized;
                }
            }, CanExec);

            MouseMoveWindowCommand = new RelayCommand((p) =>
            {
                var cbu = p as ControlBarUC;
                FrameworkElement window = GetWindowParent(cbu);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            }, CanExec);
        }

        private FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}