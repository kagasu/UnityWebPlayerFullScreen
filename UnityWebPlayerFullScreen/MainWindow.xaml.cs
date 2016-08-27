using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnityWebPlayerFullScreen
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var screens = Screen.AllScreens;
            listViewDisplays.DataContext = screens;
            
            Task.Run(() =>
            {
                while (true)
                {
                    listViewUnityWebPlayerWindows.Dispatcher.Invoke(() =>
                    {
                        var selectedIndex = listViewUnityWebPlayerWindows.SelectedIndex;
                        listViewUnityWebPlayerWindows.DataContext = Process.GetProcesses().Where(x => x.MainWindowTitle.Contains("Unity Web Player")).ToArray();
                        listViewUnityWebPlayerWindows.SelectedIndex = selectedIndex;
                    });
                    
                    Thread.Sleep(1000);
                }
            });

            button.Click += delegate(object sender, RoutedEventArgs e)
            {
                var display = (Screen)listViewDisplays.SelectedItem;
                var process = (Process)listViewUnityWebPlayerWindows.SelectedItem;

                if (display != null && process != null)
                {
                    WindowManager.ChangeBorderless(process.MainWindowHandle);
                    WindowManager.ChangeFullScreen(process.MainWindowHandle, display.DeviceName);
                }
            };
        }
    }
}