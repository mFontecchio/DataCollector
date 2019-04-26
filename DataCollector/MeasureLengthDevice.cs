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
        //**FIELDS**\\
        private Units unitsToUse;               //Units - This field will determine whether the generated measurements are metric or imperial. Its value will be determined from user input.
        private int[] dataCaptured;             //This field will store a history of a limited set of recently captured measurements. Once the array is full, the class should start overwriting the oldest elements while continuing to record the newest captures.
        private int mostRecentMeasure;          //This field will store the most recent measurement captured for convenience of display
        //
        private Timer timer;
        private decimal convertedValue;
        private int queueCount;
        private string foramattedHistory;
        private string[] collectedHistory;
        private string timeStamp;
        private string unitsUsed;
        private string unitsConverted;
        

        //**PROPERTIES**\\
        //Property for unitsToUse
        public Units UnitsToUse { get; set; }
        public decimal ConvertedValue { get; set; }
        public int MostRecentMeasure {get; set;}
        public int QueueCount { get; }
        public string TimeStamp {get; set;}
        public string ForamattedHistory
        {
            get => this.PrintValues(GetRawData()).ToString();
        }
        public string[] CollectedHistory { get; set; }
        public string UnitsUsed { get; set; }
        public string UnitsConverted { get; set; }

        //**CONSTRUCTORS**\\
        public MeasureLengthDevice()
        {
            this.UnitsToUse = Units.Metric;
            this.dataCaptured = new int[10];
            this.mostRecentMeasure = 0;
            this.timer = null;
            this.timeStamp = null;
            this.queueCount = 0;
            this.convertedValue = 0;
            this.foramattedHistory = null;
            this.collectedHistory = new string[10];
        }


        //**INTERFACE METHODS**\\
        //Return the contents of the dataCapturedarray.
        public int[] GetRawData()
        {
            return this.dataCaptured;
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Imperial.
        public decimal ImperialValue(decimal capturedValue)
        {
            decimal newValue = capturedValue;

            if (this.UnitsToUse != Units.Imperial)
            {
                newValue *= 0.3937m;
                UnitsUsed = "cm";
                UnitsConverted = "in";
            }
            return newValue;
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Metric.
        public decimal MetricValue(decimal capturedValue)
        {
            decimal newValue = capturedValue;

            if (this.UnitsToUse != Units.Metric)
            {
                newValue *= 2.54m;
                UnitsUsed = "in";
                UnitsConverted = "cm";
            }
            return newValue;
        }

        /*Start timer to collect data from Device.GetMeasurement() every 15 seconds,
        set the value to mostRecentMeasure and store it to dataCaptured array.*/
        public void StartCollecting()
        {
            timer = new Timer(Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(15).TotalMilliseconds);
        }

        //Stop the timer that started in StartCollecting().
        public void StopCollecting()
        {
            this.timer.Dispose();
        }


        //**METHODS**\\
        //Store DataCaptured array
        //This will write until the array is full. 
        //Upon a full array it will then shift all values down 1 index and overwrite index 9 or the max index.
        private void StoreHistory(int measurement)
        {
            if (this.queueCount < 10)
            {
                this.dataCaptured[this.queueCount] = measurement;
                this.collectedHistory[this.queueCount] = $" {UnitsUsed}\t\t{ConvertedValue} {UnitsConverted}\t\t\t{TimeStamp}";
                this.queueCount++;

            }

            if (this.queueCount >= 10)
            {
                for (int i = 0; i < this.dataCaptured.Length - 1; i++)
                {
                    if (i < this.dataCaptured.Length - 1)
                    {
                        this.dataCaptured[i] = this.dataCaptured[i + 1];
                        this.collectedHistory[i] = this.collectedHistory[i + 1];
                    }
                }
                this.dataCaptured[dataCaptured.Length - 1] = measurement;
                this.collectedHistory[collectedHistory.Length - 1] = $" {UnitsUsed}\t\t{ConvertedValue} {UnitsConverted}\t\t\t{TimeStamp}";
            }
        }

        //Generate String Timestamp to display in textblock.
        private static String GetTimeStamp(DateTime date)
        {
            return date.ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        //Tick Event
        private async void Tick(object state)
        {
            //code to randomly generate a new value and update GetMeasurement.
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

                //Method to store and continually populate the array with current measurements.
                StoreHistory(this.MostRecentMeasure);
            });
        }

        //Create History string with measurements, units and timestamps.
        public string PrintValues(int[] dataCaptured)
        { //code to return history from ConcurrentQueue
            StringBuilder myString = new StringBuilder();
            myString.AppendLine($"Device\t\tConverted Units\t\tTimestamp");
            for (int i = dataCaptured.Length - 1; i >= 0; i--)
                if (dataCaptured[i] != 0)
                {
                    myString.AppendLine($"{dataCaptured[i].ToString()}{collectedHistory[i]}");
                }
            return myString.ToString();
        }
    }
}
