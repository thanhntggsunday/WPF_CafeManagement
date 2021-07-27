using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_CafeManagement.Common
{
    public class CommandWrapper
    {
        public ICommand Command { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public SolidColorBrush BgColor { get; set; }
    }
}