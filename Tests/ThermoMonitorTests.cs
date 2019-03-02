using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThermoCore;
using TestData;
using ThermoMonitor;

namespace Tests
{
    [TestClass]
    public class ThermoMonitorTests
    {
        #region RequestManager

        [TestMethod]
        public void RequestManager_Use_MonitorTemperature_Scenario_When_Threshold_Undefined()
        {            
            //Arrange
            RequestManager requestManager = new RequestManager();
            Request request = Request.TemperatureRequest();
            object[] args = new object[3] { request, null, null };
            PrivateObject obj = new PrivateObject(requestManager);
            //Act
            var retVal = obj.Invoke("GetScenario", args);
            //Assert
            Assert.IsInstanceOfType(retVal, typeof(MonitorTemperature));
        }

        [TestMethod]
        public void RequestManager_Use_MonitorThreshold_Scenario_When_Threshold_Defined_And_Fluctuation_Limits_Undefined()
        {
            //Arrange
            RequestManager requestManager = new RequestManager();
            Request request = Request.ThresholdBaseRequest(777.0m);
            object[] args = new object[3] { request, null, null };
            PrivateObject obj = new PrivateObject(requestManager);
            //Act
            var retVal = obj.Invoke("GetScenario", args);
            //Assert
            Assert.IsInstanceOfType(retVal, typeof(MonitorThreshold));
        }

        [TestMethod]
        public void RequestManager_Use_MonitorThresholdWithoutFluctuations_Scenario_When_Threshold_And_Fluctuation_Limits_Defined()
        {
            //Arrange
            RequestManager requestManager = new RequestManager();
            Request request = Request.ThresholdGeneralRequest(777.0m, ThermoUnit.F, 0.1m, 0.1m);
            object[] args = new object[3] { request, null, null };
            PrivateObject obj = new PrivateObject(requestManager);
            //Act
            var retVal = obj.Invoke("GetScenario", args);
            //Assert
            Assert.IsInstanceOfType(retVal, typeof(MonitorThresholdWithoutFluctuations));
        }

        #endregion RequestManager

        #region MonitorTemperature

        [TestMethod]
        public void MonitorTemperature_Scenario_Can_Creates_Reponse()
        {
            //Arrange

            Request request = Request.TemperatureRequest(ThermoUnit.F);
            MonitorTemperature monitorTemperatureScenario = new MonitorTemperature();
            
            //Act
            var result = monitorTemperatureScenario.CreateResponse(request, 10.0m, null);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Thermometer));
            Assert.AreEqual(result.Temperature.Value, 50.0m);
        }

        [TestMethod]
        public void MonitorThreshold_Scenario_Can_Creates_Reponse_For_ThresholdBaseRequest()
        {
            //Arrange
            Request request = Request.ThresholdBaseRequest(100.0m);
            MonitorThreshold monitorTemperatureScenario = new MonitorThreshold();

            //Act
            var result = monitorTemperatureScenario.CreateResponse(request, 100.0m, null);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Thermometer));
            Assert.AreEqual(result.ObservationTime, DateTime.UtcNow);
        }

        [TestMethod]
        public void MonitorThreshold_Scenario_Does_Not_Create_Reponse_If_Treshold_Not_Reached()
        {
            //Arrange
            Request request = Request.ThresholdBaseRequest(100.0m);
            MonitorThreshold monitorTemperatureScenario = new MonitorThreshold();

            //Act
            var result = monitorTemperatureScenario.CreateResponse(request, 99.9m, null);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MonitorThresholdWithoutFluctuations_Scenario_Can_Creates_Reponse_For_ThresholdGeneralRequest()
        {
            //Arrange
            Request request = Request.ThresholdGeneralRequest(212.0m, ThermoUnit.F, 0.5m, 0.5m);
            MonitorThreshold monitorTemperatureScenario = new MonitorThreshold();

            //Act
            var result = monitorTemperatureScenario.CreateResponse(request, 100.0m, null);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Thermometer));
            Assert.AreEqual(result.ObservationTime, DateTime.UtcNow);
        }

        #endregion MonitorTemperature
    }
}

