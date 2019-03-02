using System;

using ThermoCore;

namespace ThermoMonitor
{
    public class Thermometer
    {
        #region Private Variables

        private DateTime observationDateAndTime = DateTime.UtcNow;
        private decimal? temperature = null;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Creates response for threshold monitoring
        /// </summary>
        /// <param name="observationDateAndTime">
        /// Date and time when temperature threshold was reached
        /// </param>
        public Thermometer(DateTime observationDateAndTime)
        {
            this.observationDateAndTime = observationDateAndTime;
        }

        /// <summary>
        /// Creates response for temperature monitoring 
        /// </summary>
        /// <param name="temperature">
        /// Temperature value in the given time
        /// </param>
        /// <param name="observationDateAndTime">
        /// Date and time for the given temperature value
        /// </param>
        public Thermometer(DateTime observationDateAndTime, decimal temperature)
            : this(observationDateAndTime)
        {
            this.temperature = temperature;
        } 

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Date and time of temperature observation
        /// </summary>
        public DateTime ObservationTime
        {
            get { return observationDateAndTime; }
        }

        /// <summary>
        /// Temperature value
        /// </summary>
        public decimal? Temperature
        {
            get { return this.temperature; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates response for temperature monitoring
        /// </summary>
        /// <param name="observationDateAndTime">
        /// Date and time of measurement
        /// </param>
        /// <param name="temperature">Temperature value</param>
        /// <returns>Response for temperature monitoring</returns>
        public static Thermometer TemperatureResponse(DateTime observationDateAndTime,
            decimal temperature)
        {
            return new Thermometer(observationDateAndTime, temperature);
        }

        /// <summary>
        /// Creates response for threshold monitoring
        /// </summary>
        /// <param name="observationDateAndTime">
        /// Date and time when threshold was reached
        /// </param>
        /// <returns>Response for threshold monitoring</returns>
        public static Thermometer ThresholdResponse(DateTime observationDateAndTime)
        {
            return new Thermometer(observationDateAndTime);
        }

        #endregion Public Methods
    }
}
