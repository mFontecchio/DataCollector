using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using Windows.ApplicationModel.Core;
//using Windows.UI.Core;

namespace DataCollector
{
    public class Device
    {
        private Timer timer;

        //This method will return a random integer between 1 and 10 as a measurement of some imaginary object
        int GetMeasurement()
        {
            Random randMeasurement = new Random();
            return randMeasurement.Next(1,11);
        }

        //default constructor for device create timer object
        public Device()
        {
            timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(15).TotalMilliseconds);
        }

        private async void timer_Tick(object state)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    //store data
                });
        }
    }
}
