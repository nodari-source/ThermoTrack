namespace ThermoCore
{
    /// <summary>
    /// The supported temperature measurement units (Celcius and Farenheit )
    /// </summary>
    public enum ThermoUnit { C, F }

    public static class Consts
    {
        public const ThermoUnit BASE_TEMPERATURE_MEASUREMENT_UNIT = ThermoUnit.C;
    }
}
