using System;
using System.Reflection;

using log4net;

using ThermoMonitor;

namespace ThermoTransmitter
{
    public abstract class BaseTransmitter : ITransmitter<Thermometer>
    {
        private string name = string.Empty;
        private IDisposable unsubscriber;
        private Request request;
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Constructors

        public BaseTransmitter(string name, Request request)
        {
            this.name = name;
            this.request = request;
        }

        #endregion Constructors

        #region Public Properties

        public string Name
        {
            get { return name; }
        }

        public Request Request
        {
            get { return request; }
        }

        #endregion Public Properties

        #region Public Methods

        public virtual void Subscribe(IObservable<Thermometer> monitor)
        {
            unsubscriber = monitor.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
        
        public abstract void OnNext(Thermometer currentResponse);

        public abstract void OnCompleted();

        public virtual void OnError(Exception error)
        {
            log.Error(error.Message);
        }
        
        #endregion Public Methods
    }
}
