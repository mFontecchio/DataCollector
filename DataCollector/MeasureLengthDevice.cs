using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DataCollector
{
    class MeasureLengthDevice : Device, IMeasuringDevice
    {
        private UnitsEnumeration unitsToUse;    //Units - This field will determine whether the generated measurements are metric or imperial. Its value will be determined from user input.
        private int[] dataCaptured;             //This field will store a history of a limited set of recently captured measurements. Once the array is full, the class should start overwriting the oldest elements while continuing to record the newest captures.
        private int mostRecentMeasure;          //This field will store the most recent measurement captured for convenience of display

        public MeasureLengthDevice()
        {
            this.unitsToUse = UnitsEnumeration.Imperial;
            this.dataCaptured = new int[0];
            this.mostRecentMeasure = 0;
        }

        /*public MeasureLengthDevice(UnitsEnumeration unitsToUse)
        {
            this.unitsToUse = unitsToUse;
            this.dataCaptured = new int[0];
            this.mostRecentMeasure = 0;
        }// may want to use setter getter format*/

        //Property for unitsToUse
        UnitsEnumeration UnitsToUse
        {
            get => this.unitsToUse;
            set => this.unitsToUse = value;
        }

        //Return the contents of the dataCapturedarray.
        public int[] GetRawData()
        {
            throw new NotImplementedException();
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Imperial.
        public double ImperialValue(double capturedValue)
        {
            double convertedValue = capturedValue;

            if (this.unitsToUse != UnitsEnumeration.Imperial)
            {
                convertedValue = convertedValue * 0.3937;
            }
            return convertedValue;
        }

        //Return the current value from mostRecentMeasure- convert it if unitsToUse is not Metric.
        public double MetricValue(double capturedValue)
        {
            double convertedValue = capturedValue;

            if (this.unitsToUse != UnitsEnumeration.Metric)
            {
                convertedValue = convertedValue * 2.54;
            }
            return convertedValue;
        }

        /*Start timer to collect data from Device.GetMeasurement() every 15 seconds,
        set the value to mostRecentMeasure and store it to dataCaptured array.*/
        public void StartCollecting()
        {
            //Timer timer;

            //Create timer object
            //timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(15).TotalMilliseconds);

            //tick handler and getMeasure ? this would need to be a method called in device?
        }

        //Stop the timer that started in StartCollecting().
        public void StopCollecting()
        {
            throw new NotImplementedException();
        }
    }
}
