using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace DataCollector
{
    class MeasureLengthDevice : Device, IMeasuringDevice
    {
        private Units unitsToUse;    //Units - This field will determine whether the generated measurements are metric or imperial. Its value will be determined from user input.
        private int[] dataCaptured;             //This field will store a history of a limited set of recently captured measurements. Once the array is full, the class should start overwriting the oldest elements while continuing to record the newest captures.
        private int mostRecentMeasure;          //This field will store the most recent measurement captured for convenience of display
        private Timer timer;
        private string timeStamp;

        public MeasureLengthDevice()
        {
            this.unitsToUse = Units.Imperial;
            this.dataCaptured = new int[0];
            this.mostRecentMeasure = 0;
            this.timer = null;
            this.timeStamp = null;
        }
        
        //Set EnumUnits
        Units UnitsToUse {get; set;}

        public int MostRecentMeasure {get; set;}

        public string TimeStamp {get; set;}

        //Return the contents of the dataCapturedarray.
        public int[] GetRawData()
        {
            throw new NotImplementedException();
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Imperial.
        public double ImperialValue(double capturedValue)
        {
            double convertedValue = capturedValue;

            if (this.unitsToUse != Units.Imperial)
            {
                convertedValue = convertedValue * 0.3937;
            }
            return convertedValue;
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Metric.
        public double MetricValue(double capturedValue)
        {
            double convertedValue = capturedValue;

            if (this.unitsToUse != Units.Metric)
            {
                convertedValue = convertedValue * 2.54;
            }
            return convertedValue;
        }

        /*Start timer to collect data from Device.GetMeasurement() every 15 seconds,
        set the value to mostRecentMeasure and store it to dataCaptured array.*/
        public void StartCollecting()
        {
            timer = new Timer(Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(1).TotalMilliseconds);
        }

        private async void Tick(object state)
        {
            //code to randomly generate a new value and update GetMeasurement
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => {
                this.MostRecentMeasure = this.GetMeasurement();
                this.TimeStamp = GetTimeStamp(DateTime.Now);
            });
        }

        //Stop the timer that started in StartCollecting().
        public void StopCollecting()
        {
            this.timer.Dispose();
        }

        //Generate String Timestamp to display in textblock
        private static String GetTimeStamp(DateTime date)
        {
            return date.ToString("MM/dd/yyyy hh:mm:ss tt");
        }
    }
}
