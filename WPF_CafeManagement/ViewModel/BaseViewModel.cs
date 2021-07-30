using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_CafeManagement.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public bool Maximize { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanExec(object obj)
        {
            return (true);
        }
    }
}