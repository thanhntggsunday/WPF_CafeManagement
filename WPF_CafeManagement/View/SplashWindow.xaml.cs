using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_CafeManagement.View
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        private Thread loadingThread;      
        private delegate void ShowDelegate(string txt);
        private delegate void HideDelegate();
        private ShowDelegate showDelegate;       

        public SplashWindow()
        {
            InitializeComponent();
            showDelegate = new ShowDelegate(this.showText);         
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadingThread = new Thread(load);
            loadingThread.IsBackground = false;
           
            loadingThread.Start();
        }

        private void load()
        {                
            this.Dispatcher.Invoke(showDelegate, "last data loading");
            //close the window   
            //Thread.Sleep(2000);
            //this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate () { 
            //    Close();
            //    loadingThread.Abort();
            //});
        }

        private void showText(string txt)
        {
            Window_ContentRendered();
        }

       
        private void Window_ContentRendered()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += worker_ProgressComplated;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 200; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(10);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }

        void worker_ProgressComplated(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate () {
                Close();
                loadingThread.Abort();
            });
        }

    }
}