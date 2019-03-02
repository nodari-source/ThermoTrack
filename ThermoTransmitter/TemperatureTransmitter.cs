using System;
using System.Reflection;

using log4net;

using ThermoMonitor;

namespace ThermoTransmitter
{
    public class TemperatureTransmitter : BaseTransmitter
    {
        private bool first = true;
        private Thermometer lastResponse;
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Constructors

        public TemperatureTransmitter(string name, Request request) : base(name, request) { }

        #endregion Constructors

        #region Public Methods

        public override void OnNext(Thermometer currentResponse)
        {
            Console.WriteLine(Name + ": The temperature is {0}°{1} at {2:g}",
                currentResponse.Temperature, Request.Unit.ToString(), currentResponse.ObservationTime);
            if (first)
            {
                lastResponse = currentResponse;
                first = false;
            }
            else
            {
                Console.WriteLine(Name + ": Temperature change is {0}°{1} in {2:g}", 
                    currentResponse.Temperature - lastResponse.Temperature,
                    Request.Unit.ToString(),
                    currentResponse.ObservationTime.ToUniversalTime() - lastResponse.ObservationTime.ToUniversalTime());
            }
        }

        public override void OnCompleted()
        {
            string message = Name + ": Additional temperature data will not be transmitted.";
            Console.WriteLine(message);
            log.Debug(message);
        }

        #endregion Public Methods
    }
}