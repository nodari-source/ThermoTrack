using System;
using System.Reflection;

using log4net;

using ThermoMonitor;

namespace ThermoTransmitter
{
    public class GeneralTransmitter : BaseTransmitter
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Constructors

        public GeneralTransmitter(string name, Request request): base(name, request)  { }

        #endregion Constructors

        #region Public Methods

        public override void OnNext(Thermometer currentResponse)
        {
            string message = string.Format( Name + ": The temperature is {0}°{1} at {2:g}",
                currentResponse.Temperature, Request.Unit.ToString(),  currentResponse.ObservationTime);
            Console.WriteLine(message);
            log.Debug(message);
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
