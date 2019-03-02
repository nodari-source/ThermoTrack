using ThermoCore;

namespace ThermoMonitor
{
    public abstract class BaseScenario : IScenario
    {
        public abstract Thermometer CreateResponse(Request request, 
            decimal? startTemperature, decimal? endTemperature);

        /// <summary>
        /// Returns the latest temperature in the requested measument unit
        /// </summary>
        /// <param name="startTemperature">Initial temperature value</param>
        /// <param name="endTemperature">End temperature value if exists</param>
        /// <param name="unit">The requested measurement unit</param>
        /// <returns>The latest temperature value reducted to the requested measurement unit</returns>
        public decimal? GetCurentReductedTemperature(decimal? startTemperature, decimal? endTemperature,
            ThermoUnit unit)
        {
            decimal? currentTemperature = startTemperature;
            if(endTemperature.HasValue)
            {
                currentTemperature = endTemperature;
            }

            if (currentTemperature.HasValue)
            {
                if(unit != Consts.BASE_TEMPERATURE_MEASUREMENT_UNIT)
                {
                    currentTemperature = currentTemperature.Value.ToFarenheit();
                }
            }
            return currentTemperature;
        }

        /// <summary>
        /// Returns true if threshold reached otherwise false
        /// </summary>
        /// <param name="temperature">Temperature value in the threshold unit</param>
        /// <param name="threshold">The temperature threshold value</param>
        /// <returns>True if threshold reached otherwise false</returns>
        protected static bool ThresholdReached(decimal? temperature, decimal? threshold)
        {
            bool thresholdReached = false;
            if(temperature.HasValue && threshold.HasValue)
            {
                thresholdReached = (temperature.Value == threshold.Value);
            }         
            return thresholdReached;
        }        

        protected static bool WithinFluctuationLimits(decimal thisTemperature, decimal? thatTemperature,
            decimal upperFluctuationLimit, decimal lowerFluctuationLimit)
        {
            bool withinFluctuationLimits = true;

            if ((!thatTemperature.HasValue) ||
                //Fluctuation limits undefined
                (upperFluctuationLimit == 0 && lowerFluctuationLimit == 0) ||
                //Beyond the upper fluctuation limit
                ((upperFluctuationLimit > 0) && (thisTemperature > thatTemperature + upperFluctuationLimit)) ||
                //Beyond the lower fluctuation limit
                ((lowerFluctuationLimit > 0) && (thisTemperature < thatTemperature - lowerFluctuationLimit)))
            {
                withinFluctuationLimits = false;
            }
            return withinFluctuationLimits;
        }

        protected bool TemperatureDecrease(decimal? startTemperature, decimal? endTemperature)
        {
            return (startTemperature.HasValue && endTemperature.HasValue &&
                startTemperature.Value > endTemperature.Value);
        }

        protected bool TemperatureIncrease(decimal? startTemperature, decimal? endTemperature)
        {
            return (startTemperature.HasValue && endTemperature.HasValue &&
                startTemperature.Value < endTemperature.Value);
        }

        protected bool TemperatureChangedInDefinedDirection(ChangeType eventType, 
            decimal? startTemperature, decimal? endTemperature)
        {
            return ((eventType == ChangeType.Decrease &&
                TemperatureDecrease(startTemperature, endTemperature)) ||
                (eventType == ChangeType.Increase &&
                TemperatureIncrease(startTemperature, endTemperature)) ||
                (eventType == ChangeType.All));
        }
    }
}
