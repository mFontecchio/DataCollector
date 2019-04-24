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
        private int queueCount;
        private decimal convertedValue;

        public MeasureLengthDevice()
        {
            this.unitsToUse = Units.Metric;
            this.dataCaptured = new int[10];
            this.mostRecentMeasure = 0;
            this.timer = null;
            this.timeStamp = null;
            this.queueCount = 0;
            this.convertedValue = 0;
        }

        //Property for unitsToUse
        public Units UnitsToUse
        {
            get => this.unitsToUse;
            set => this.unitsToUse = value;
        }

        public int MostRecentMeasure {get; set;}

        public string TimeStamp {get; set;}

        public int QueueCount { get; }

        public decimal ConvertedValue { get; set; }

        //Return the contents of the dataCapturedarray.
        public int[] GetRawData()
        {
            return this.dataCaptured;
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Imperial.
        public decimal ImperialValue(decimal capturedValue)
        {
            decimal newValue = capturedValue;

            if (this.unitsToUse != Units.Imperial)
            {
                newValue *= 0.3937m;
            }
            return newValue;
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Metric.
        public decimal MetricValue(decimal capturedValue)
        {
            decimal newValue = capturedValue;

            if (this.unitsToUse != Units.Metric)
            {
                newValue *= 2.54m;
            }
            return newValue;
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

                //ConvertValue
                if (this.UnitsToUse == Units.Metric)
                {
                    this.ConvertedValue = this.ImperialValue(this.MostRecentMeasure);
                }
                else
                {
                    this.ConvertedValue = this.MetricValue(this.MostRecentMeasure);
                }

                //Method to store and continually populate the array with current measurements
                StoreHistory(this.MostRecentMeasure);
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

        //Store DataCaptured array
        //This will write until the array is full. 
        //Upon a full array it will then shift all values down 1 index and overwrite index 9 or the max index.
        private void StoreHistory(int measurement)
        {
            if (this.queueCount < 10)
            {
                this.dataCaptured[this.queueCount] = measurement;
                this.queueCount++;

            }

            if (this.queueCount >= 10)
            {
                for (int i = 0; i < this.dataCaptured.Length - 1; i++)
                {
                    if (i < this.dataCaptured.Length - 1)
                    {
                        this.dataCaptured[i] = this.dataCaptured[i + 1];
                    }
                }
                this.dataCaptured[dataCaptured.Length - 1] = measurement;
            }
        }
    }
}
