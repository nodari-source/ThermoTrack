using System;

namespace ThermoMonitor
{
    /// <summary>
    /// Represents the scenario to detect if threshold has been reached  
    /// when fluctuation levels were undefined
    /// </summary>
    public class MonitorThreshold : BaseScenario
    {
        public override Thermometer CreateResponse(Request request, decimal? startTemperature, decimal? endTemperature)
        {
            Thermometer response = null;
            decimal? temperature = GetCurentReductedTemperature(startTemperature, endTemperature, request.Unit);
            if (!temperature.HasValue) return response;

            //Fluctuation limits are undefined, so inform each time threshold is reached 
            if (ThresholdReached(temperature, request.Threshold.Value))
            {
                //Consider direction of temperature change
                if (TemperatureChangedInDefinedDirection(request.EventType, startTemperature, endTemperature))
                {
                    response = Thermometer.ThresholdResponse(DateTime.UtcNow);
                }                
            }
            return response;
        }
    }
}
