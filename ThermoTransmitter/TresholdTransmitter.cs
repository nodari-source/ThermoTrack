using System;
using System.Reflection;

using log4net;

using ThermoMonitor;

namespace ThermoTransmitter
{
    public class  ThresholdTransmitter : BaseTransmitter
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Constructors

        public ThresholdTransmitter(string name, Request request) : base(name, request) { }

        #endregion Constructors

        #region Public Methods

        public override void OnNext(Thermometer currentResponse)
        {
            string message = string.Format(Name + ": The threshold {0}°{1} was reached at {2:g}",
                Request.Threshold, Request.Unit.ToString(), currentResponse.ObservationTime);
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