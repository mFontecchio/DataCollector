using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace DataCollector
{
    class Device
    {
        //private Timer timer;
        //private int recentMeassurement;

        //This method will return a random integer between 1 and 10 as a measurement of some imaginary object
        public int GetMeasurement()
        {
            Random randMeasurement = new Random();
            return randMeasurement.Next(1,11);
        }

        /*//default constructor for device create timer object
        public Device()
        {
            timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(2).TotalMilliseconds);
        }

        //Timer Event every X seconds
        private async void timer_Tick(object state)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    //store data
                    this.recentMeassurement = GetMeasurement();

                });
        }

        public Timer Timer
        {
            get => this.timer;
        }

        public int RecentMeasurement
        {
            get => this.recentMeassurement;
        }*/
    }
}
