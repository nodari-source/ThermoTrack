namespace ThermoMonitor
{
    public interface IScenario
    {
        Thermometer CreateResponse(Request request, 
            decimal? startTemperature, decimal? endTemperature);
    }
}
