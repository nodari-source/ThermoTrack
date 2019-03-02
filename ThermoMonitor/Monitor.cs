using System;
using System.Collections.Generic;
using System.Reflection;

using log4net;

using TestData;

namespace ThermoMonitor
{
    public class Monitor : IObservable<Thermometer>
    {
        List<ITransmitter<Thermometer>> transmitters;
        RequestManager requestManager = null;
        TestDataGenerator dataGenerator = null;        
        decimal? previous = null;
        bool start = true;
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Monitor(TestDataGenerator dataGenerator)
        {
            transmitters = new List<ITransmitter<Thermometer>>();
            requestManager = new RequestManager();
            this.dataGenerator = dataGenerator;
            if (dataGenerator != null)
            {
                this.dataGenerator.MeasurementCompleted += MeasurementCompleted;
            }

            log.Debug("Monitor created");
        }

        void MeasurementCompleted(object sender, MeasurementCompletedEventArgs e)
        {
            if(e.Temperature.HasValue)
            {
                if(start)
                {                    
                    start = false;
                    requestManager.SendResponse(transmitters, e.Temperature.Value, null);
                }
                else
                {
                    requestManager.SendResponse(transmitters, previous, e.Temperature.Value);
                }
                previous = e.Temperature.Value;
            }
            else
            {
                foreach (var transmitter in transmitters.ToArray())
                {
                    if (transmitter != null) transmitter.OnCompleted();
                }

                transmitters.Clear();
                dataGenerator.MeasurementCompleted -= MeasurementCompleted;
            }
        }

        public IDisposable Subscribe(IObserver<Thermometer> transmitter)
        {
            ITransmitter<Thermometer> thermoObserver = transmitter as ITransmitter<Thermometer>;
            if (!transmitters.Contains(thermoObserver ))
            {
                transmitters.Add(thermoObserver);
            }

            return new Unsubscriber(transmitters, thermoObserver);
        }

        private class Unsubscriber : IDisposable
        {
            private List<ITransmitter<Thermometer>> transmitters;
            private ITransmitter<Thermometer> transmitter;

            public Unsubscriber(List<ITransmitter<Thermometer>> transmitters, 
                ITransmitter<Thermometer> transmitter)
            {
                this.transmitters = transmitters;
                this.transmitter = transmitter;
            }

            public void Dispose()
            {
                if (transmitter != null)
                {
                    transmitters.Remove(transmitter);
                }                    
            }
        }
    }
}