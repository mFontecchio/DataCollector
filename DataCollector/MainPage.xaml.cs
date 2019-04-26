using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataCollector
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MeasureLengthDevice newDevice = null;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            newDevice = new MeasureLengthDevice();

            //Initialize RadioButton DefaultCheck
            this.metricRadioButton.IsChecked = true;
        }



        private void Start_Click(object sender, RoutedEventArgs e)
        {
            newDevice.StartCollecting();
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            newDevice.StopCollecting();
            timer.Stop();
        }

        void timer_Tick(object sender, object e)
        {
            //Time since last tick should be very very close to Interval
            DataContext = null;
            DataContext = this.newDevice;

            /*measureListView.Items.Clear();
            int[] history = newDevice.GetRawData();

            for (int i = history.Length - 1; i >= 0; i--)
            {
                if (history[i] != 0)
                {
                    measureListView.Items.Add(history[i]);
                }
            }*/
        }

        private void MetricRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.newDevice.UnitsToUse = Units.Metric;
        }

        private void ImperialRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.newDevice.UnitsToUse = Units.Imperial;
        }

        private void HistoryToggle_Checked(object sender, RoutedEventArgs e)
        {
            //measureListView.Visibility = Visibility.Visible;
            historyTextblock.Visibility = Visibility.Visible;

            historyToggle.Content = "Hide History";
        }

        private void HistoryToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            //measureListView.Visibility = Visibility.Collapsed;
            historyTextblock.Visibility = Visibility.Collapsed;

            historyToggle.Content = "View History";
        }

        private void StartStopToggle_Checked(object sender, RoutedEventArgs e)
        {
            newDevice.StartCollecting();
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            startStopToggle.Content = "Stop";
        }

        private void StartStopToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            newDevice.StopCollecting();
            timer.Stop();

            startStopToggle.Content = "Start";
        }
    }
}
