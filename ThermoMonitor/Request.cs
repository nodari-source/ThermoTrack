
using ThermoCore;

namespace ThermoMonitor
{
    /// <summary>
    /// Type of change to be monitored
    /// </summary>
    public enum ChangeType
    {
        All,
        Increase,
        Decrease
    }

    /// <summary>
    /// Container for monitoring criteria from caller
    /// </summary>
    public class Request
    {
        #region Private Variables

        private ChangeType observationType = ChangeType.All;
        private decimal? threshold = null;
        private decimal lowerFluctuationLimit = 0m;     //no lower fluctuation limit
        private decimal upperFluctuationLimit = 0m;     //no upper fluctuation limit        
        private ThermoUnit unit = ThermoUnit.C;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Creates request for temperature monitoring
        /// </summary>
        private Request() { }

        /// <summary>
        /// Creates request for temperature monitoring in certain measurement unit
        /// </summary>
        /// <param name="unit">Temperature measurement unit</param>
        private Request(ThermoUnit unit) : this()
        {
            this.unit = unit;
        }

        /// <summary>
        /// Creates request for threshold monitoring
        /// </summary>
        /// <param name="threshold">Threshold value in base measurement unit</param>
        private Request(decimal threshold) : this()
        {
            this.threshold = threshold;
        }

        /// <summary>
        /// Creates request for threshold monitoring in certain measurement unit
        /// </summary>
        /// <param name="threshold">Threshold value in certain measurement unit</param>
        /// <param name="unit">Temperature measurement unit</param>
        private Request(decimal threshold, ThermoUnit unit) : this(threshold)
        {
            this.unit = unit;
        }

        /// <summary>
        /// Creates request for threshold monitoring 
        /// of certain type  in certain measurement unit  
        /// and do not consider fluctuations within certain range
        /// <param name="threshold">
        /// A threshold temperature value in certain measurement unit to monitor
        /// </param>
        /// <param name="thermoUnit">
        /// Temperature measurement unit
        /// </param>
        /// <param name="lowerFluctuationLimit">
        /// Minimum value of temperature decrease that is considered to be a fluctuation
        /// </param>
        /// <param name="upperFluctuationLimit">
        /// Minimum value of temperature increase that is considered to be a fluctuation
        /// </param>
        private Request(
            decimal threshold,
            ThermoUnit thermoUnit = ThermoUnit.C,
            decimal lowerFluctuationLimit = 0m,
            decimal upperFluctuationLimit = 0m
            )
            : this(threshold, thermoUnit)
        {
            this.lowerFluctuationLimit = lowerFluctuationLimit;
            this.upperFluctuationLimit = upperFluctuationLimit;
        }

        /// <summary>
        /// Creates request for threshold monitoring 
        /// of certain type  in certain measurement unit  
        /// and do not consider fluctuations within certain range
        /// <param name="threshold">
        /// A threshold temperature value in certain measurement unit to control
        /// </param>
        /// <param name="unit">
        /// Temperature measurement unit
        /// </param>
        /// <param name="lowerFluctuationLimit">
        /// Minimum value of temperature decrease that is considered to be a fluctuation
        /// </param>
        /// <param name="upperFluctuationLimit">
        /// Minimum value of temperature increase that is considered to be a fluctuation
        /// </param>
        /// <param name="observationType">
        /// Type of temperature observation
        /// </param>
        public Request(
            decimal threshold,
            ThermoUnit unit = ThermoUnit.C,
            decimal lowerFluctuationLimit = 0m,
            decimal upperFluctuationLimit = 0m,
            ChangeType observationType = ChangeType.All
            )
            : this(threshold, unit, lowerFluctuationLimit, upperFluctuationLimit)
        {
            this.observationType = observationType;
        }

        #endregion Constructors

        #region Public Properties

        public decimal? Threshold
        {
            get { return this.threshold; }
        }

        public ChangeType EventType
        {
            get { return observationType; }
        }

        public ThermoUnit Unit
        {
            get { return this.unit; }
        }

        public decimal LowerFluctuationLimit
        {
            get { return lowerFluctuationLimit; }
        }

        public decimal UpperFluctuationLimit
        {
            get { return upperFluctuationLimit; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates request for temperature monitoring in Celsius 
        /// </summary>
        /// <returns>Request for temperature monitoring</returns>
        public static Request TemperatureRequest()
        {
            return new Request();
        }

        /// <summary>
        /// Creates request for temperature monitoring in certain measurement unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>Request for temperature monitoring</returns>
        public static Request TemperatureRequest(ThermoUnit unit)
        {
            return new Request(unit);
        }

        /// <summary>
        /// Creates request for threshold monitoring
        /// </summary>
        /// <param name="threshold">Threshold value in base measurement unit</param>
        /// <returns>Request for threshold monitoring</returns>
        public static Request ThresholdBaseRequest(decimal threshold)
        {
            return new Request( threshold);
        }

        /// <summary>
        /// Creates request for threshold monitoring in certain measurement unit
        /// </summary>
        /// <param name="threshold">Threshold value in certain measurement unit</param>
        /// <param name="unit">Temperature measurement unit</param>
        /// <returns>Request for threshold monitoring</returns>
        public static Request ThresholdUnitRequest(decimal threshold, ThermoUnit unit)
        {
            return new Request(threshold, unit);
        }

        /// <summary>
        /// Creates request for threshold monitoring 
        /// of certain type  in certain measurement unit  
        /// and do not consider fluctuations within certain range
        /// </summary>
        /// <param name="threshold">
        /// A threshold temperature value in certain measurement unit to monitor
        /// </param>
        /// <param name="unit">
        /// Temperature measurement unit
        /// </param>
        /// <param name="lowerFluctuationLimit">
        /// Minimum value of temperature decrease that is considered to be a fluctuation
        /// </param>
        /// <param name="upperFluctuationLimit">
        /// Minimum value of temperature increase that is considered to be a fluctuation
        /// </param>
        /// <returns>Request for threshold monitoring </returns>
        public static Request ThresholdGeneralRequest(decimal threshold, ThermoUnit unit, 
            decimal lowerFluctuationLimit, decimal upperFluctuationLimit)
        {
            return new Request(threshold, unit, lowerFluctuationLimit, upperFluctuationLimit);
        }

        /// <summary>
        /// Creates request for threshold monitoring 
        /// of certain type  in certain measurement unit  
        /// and do not consider fluctuations within certain range
        /// </summary>
        /// <param name="threshold">
        /// A threshold temperature value in certain measurement unit to control
        /// </param>
        /// <param name="unit">
        /// Temperature measurement unit
        /// </param>
        /// <param name="lowerFluctuationLimit">
        /// Minimum value of temperature decrease that is considered to be a fluctuation
        /// </param>
        /// <param name="upperFluctuationLimit">
        /// Minimum value of temperature increase that is considered to be a fluctuation
        /// </param>
        /// <param name="observationType">
        /// Type of temperature observation
        /// </param>
        /// <returns>
        /// Request for threshold monitoring
        /// </returns>
        public static Request ThresholdDirectionalRequest(decimal threshold, ThermoUnit unit, 
            decimal lowerFluctuationLimit, decimal upperFluctuationLimit, ChangeType observationType)
        {
            return new Request(threshold, unit, lowerFluctuationLimit, upperFluctuationLimit, observationType);
        }

        #endregion Public Methods
    }
}
