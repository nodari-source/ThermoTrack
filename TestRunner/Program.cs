using System;
using System.Reflection;

using log4net;

using ThermoCore;
using TestData;
using ThermoMonitor;
using ThermoTransmitter;

namespace TestRunner
{
    class Program
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {      
            try
            {
                log.Info("Runner start");
                TestDataGenerator dataGenerator = new TestDataGenerator();
                log.Info("Data Generator 1:");
                Monitor monitor = new Monitor(dataGenerator);

                #region To subscribe transmitter please uncoment the code within the region
                Request temperatureMeasurementRequest = Request.TemperatureRequest();
                GeneralTransmitter transmitter = new GeneralTransmitter("Temperature Transmitter", temperatureMeasurementRequest);
                transmitter.Subscribe(monitor);
                log.Debug("Temperature Transmitter subscribed");
                #endregion

                #region To subscribe transmitter please uncoment the code within the region
                Request thresholdControlRequestWithFluctuationRanges =
                    Request.ThresholdDirectionalRequest(0.0m, ThermoUnit.C, 0.5m, 0.5m, ChangeType.All);
                ThresholdTransmitter thresholdTransmitterWithFluctuationRanges = new ThresholdTransmitter("Treshold Transmitter 1", thresholdControlRequestWithFluctuationRanges);
                thresholdTransmitterWithFluctuationRanges.Subscribe(monitor);
                log.Debug("Treshold Transmitter 1 subscribed");
                #endregion

                #region To subscribe transmitter please uncoment the code within the region
                //Request thresholdControlRequestWithoutFluctuationRanges =
                //    Request.ThresholdDirectionalRequest(0.0m, ThermoUnit.C, 0.0m, 0.0m, ChangeType.All);
                //TresholdTransmitter thresholdTransmitterWithoutFluctuationRanges = new TresholdTransmitter("Treshold Transmitter 2", thresholdControlRequestWithoutFluctuationRanges);
                //thresholdTransmitterWithoutFluctuationRanges.Subscribe(monitor);
                //log.Debug("Threshold Transmitter 2 subscribed");
                #endregion

                dataGenerator.RunGenerator();
                log.Info("Data Transmission from Generator 1 ended");

                //Control threshold when temperature increases
                TestDataGenerator anotherDataGenerator = new TestDataGenerator();
                log.Info("Data Generator 2:");
                Monitor anotherMonitor = new Monitor(anotherDataGenerator);

                #region To subscribe transmitter please uncoment the code within the region
                Request oneDirectionThresholdControlRequest = Request.ThresholdDirectionalRequest(0.0m, ThermoUnit.C, 0.5m, 0.5m, ChangeType.Increase);
                ThresholdTransmitter thresholdTransmitter = new ThresholdTransmitter("Threshold Increase Transmitter", oneDirectionThresholdControlRequest);
                thresholdTransmitter.Subscribe(anotherMonitor);
                log.Debug("Threshold Increase Transmitter");
                #endregion

                anotherDataGenerator.RunGenerator();
                log.Info("Data Transmission from Generator 2 ended");
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
            finally
            {
                log.Info("Runner end");
                log = null;
                Console.ReadLine();
            }                         
        }
    }
}
