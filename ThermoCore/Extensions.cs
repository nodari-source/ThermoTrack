namespace ThermoCore
{
    public static class Extensions
    {
        public static decimal ToFarenheit(this decimal celcius)
        {
            decimal farenheit = ((9.0m / 5.0m) * celcius) + 32;
            return farenheit;
        }
    }
}
