using System;

namespace ThermoMonitor
{
    public interface ITransmitter<T> : IObserver<T> where T: Thermometer
    {
        Request Request { get; }
    }
}
