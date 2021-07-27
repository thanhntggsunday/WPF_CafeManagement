using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_CafeManagement.Common.Messaging;

namespace WPF_CafeManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IEventAggregator EventAggregator { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            EventAggregator = new EventAggregator();
            base.OnStartup(e);
        }
    }
}
