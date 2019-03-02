using System;

namespace ThermoMonitor
{
    /// <summary>
    /// Represents the scenario to detect if threshold reached from both directions 
    /// (i.e. when temperature decreases and temperature increases) and 
    /// fluctuations are not to be considered 
    /// </summary>
    public class MonitorThresholdWithoutFluctuations : BaseScenario
    {
        private static bool thresholdReached = false;

        public override Thermometer CreateResponse(Request request, decimal? startTemperature, decimal? endTemperature)
        {
            Thermometer response = null;
            decimal? temperature = GetCurentReductedTemperature(startTemperature, endTemperature, request.Unit);
            if (!temperature.HasValue) return response;

            //Fluctuation limits are defined, so inform just one time threshold is reached 
            //within the given fluctuation limits
            if (!thresholdReached)
            {
                if (ThresholdReached(temperature, request.Threshold))
                {
                    //Consider direction of temperature change
                    if (TemperatureChangedInDefinedDirection(request.EventType, startTemperature, endTemperature))
                    {
                        response = Thermometer.ThresholdResponse(DateTime.UtcNow);
                        thresholdReached = true;
                    }
                }
            }
            else
            {
                if(!WithinFluctuationLimits(temperature.Value, request.Threshold.Value, 
                    request.UpperFluctuationLimit, request.LowerFluctuationLimit))
                {
                    thresholdReached = false;
                }
            }
            return response; 
        }
    }
}
