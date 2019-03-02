using System.Collections.Generic;

namespace ThermoMonitor
{
    /// <summary>
    /// Responsible for defining execution scenario based on caller's request 
    /// and sending response to the caller
    /// </summary>
    public class RequestManager
    {
        public void SendResponse(List<ITransmitter<Thermometer>> transmitters, decimal? startTemperature, decimal? endTemperature )
        {
            foreach (ITransmitter<Thermometer> transmitter in transmitters)
            {
                Request request = transmitter.Request;
                IScenario strategy = GetScenario(request, startTemperature, endTemperature);
                Thermometer response = strategy.CreateResponse(request, startTemperature, endTemperature);
                if(response != null)
                {
                    transmitter.OnNext(response);
                }
            }
        }

        private IScenario GetScenario(Request request, decimal? startTemperature, decimal? endTemperature)
        {
            IScenario scenario = null;

            if (ThresholdDefined(request.Threshold))
            {
                //Monitor threshold
                scenario = (FluctuationLimitsDefined(
                    request.UpperFluctuationLimit, request.LowerFluctuationLimit)) ?
                    ((IScenario)new MonitorThresholdWithoutFluctuations()) :
                    ((IScenario)new MonitorThreshold());
                return scenario;
            }
            else
            {
                //Monitor temperature
                scenario = new MonitorTemperature();
                return scenario;
            }
        }

        private bool FluctuationLimitsDefined(decimal upperLimit, decimal lowerLimit)
        {
            return (upperLimit > 0 || lowerLimit > 0);
        } 
        
        private bool ThresholdDefined(decimal? threshold)
        {
            return threshold.HasValue;
        }
    }
}
