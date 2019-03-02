using System;
using System.Collections.Generic;
using System.Threading;

namespace TestData
{
    public delegate void MeasurementCompletedEventHandler(Object sender, MeasurementCompletedEventArgs e);

    public class TestDataGenerator
    {
        public event MeasurementCompletedEventHandler MeasurementCompleted;

        public void RunGenerator()
        {
            List<decimal?> data = GetTestData();

            foreach(decimal? measurement in data)
            {
                MeasurementCompletedEventArgs args =
                    new MeasurementCompletedEventArgs(measurement, DateTime.UtcNow);
                OnMeasurementCompleted(args);
                Thread.Sleep(2000);
            }
        }

        public List<decimal?> GetTestData()
        {
            List<decimal?> data = 
                new List<decimal?> { 1.5m, 1.0m, 0.5m, 0.0m, -0.5m, 0.0m, -0.5m, 0.0m, 0.5m, 0.0m, 0.5m, 1.0m, 1.5m, 1.0m, 0.5m, 0.0m, -0.5m, -1.0m, -1.5m, -1.0m, -0.5m, 0.0m, null };
            return data;
        }

        protected virtual void OnMeasurementCompleted(MeasurementCompletedEventArgs e)
        {
            MeasurementCompletedEventHandler handler = MeasurementCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class MeasurementCompletedEventArgs : EventArgs
    {
        private decimal? temperature = null;
        private DateTime timeReached;

        public MeasurementCompletedEventArgs(decimal? temperature, DateTime timeReached) :base()
        {
            this.temperature = temperature;
            this.timeReached = timeReached;
        }
        public decimal? Temperature 
        {
            get { return temperature; }
        }

        public DateTime TimeReached 
        {
            get { return timeReached; }
        }
    }
}
