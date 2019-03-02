using System;

namespace ThermoMonitor
{
    /// <summary>
    /// Represents the scenario to monitor all temperature values
    /// </summary>
    public class MonitorTemperature : BaseScenario
    {
        public override Thermometer CreateResponse(Request request, decimal? startTemperature, decimal? endTemperature)
        {
            Thermometer response = null;
            decimal? temperature = GetCurentReductedTemperature( startTemperature, endTemperature, request.Unit);

            if(temperature.HasValue)
            {
                response = Thermometer.TemperatureResponse(DateTime.UtcNow, temperature.Value);
            }
           return response;
        }
    }
}
