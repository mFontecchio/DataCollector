using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        UnitsEnumeration UnitsToUse
        {
            get => this.unitsToUse;
            set => this.unitsToUse = value;
        }

        public int[] GetRawData()
        {
            throw new NotImplementedException();
        }

        public double ImperialValue(double capturedValue)
        {
            double convertedValue = capturedValue;

            if (this.unitsToUse != UnitsEnumeration.Imperial)
            {
                convertedValue = convertedValue * 0.3937;
            }
            return convertedValue;
        }

        public double MetricValue(double capturedValue)
        {
            double convertedValue = capturedValue;

            if (this.unitsToUse != UnitsEnumeration.Metric)
            {
                convertedValue = convertedValue * 2.54;
            }
            return convertedValue;
        }

        public void StartCollecting()
        {
            throw new NotImplementedException();
        }

        public void StopCollecting()
        {
            throw new NotImplementedException();
        }
    }
}
