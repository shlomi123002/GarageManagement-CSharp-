using System;
using System.Collections.Generic;

namespace Vehicles
{
    public class ElectricCar : Car
    {
        private ElectricVehicleSystem m_ElectricSystem;

        public ElectricCar()
        {
            this.m_ElectricSystem = new ElectricVehicleSystem(4.8f);
        }

        public ElectricVehicleSystem ElectricSystem
        {
            get { return this.m_ElectricSystem; }
            set { this.m_ElectricSystem = value; }
        }

        public override void GetParameters(List<string> parametersArray)
        {
            base.GetParameters(parametersArray);
            parametersArray.Add("battery time remaining in hours");
        }

        public override void SetParameters(Dictionary<string, string> setParametersDict, List<string> i_SetWheelsAirPressure)
        {
            base.SetParameters(setParametersDict, i_SetWheelsAirPressure);
            this.m_ElectricSystem.SetElectricSystemParameters(setParametersDict);
            this.m_EnergyPercentage = (this.m_ElectricSystem.BatteryTimeRemainingInHours / this.m_ElectricSystem.BatteryTimeMaximumInHours) * 100;
        }

        public override void GetData(Dictionary<string, string> i_DataDict)
        {
            base.GetData(i_DataDict);
            i_DataDict.Add("Maximum battery time remaining in hours", this.m_ElectricSystem.BatteryTimeMaximumInHours.ToString());
            i_DataDict.Add("battery time remaining in hours", this.m_ElectricSystem.BatteryTimeRemainingInHours.ToString());
        }

        public override Type GetSystemType()
        {
            return typeof(ElectricVehicleSystem);
        }

        public override void FillEnergy(List<string> i_FillEnergyList)
        {
            bool parseSucceeded = false;
            float minutesToAdd;
            parseSucceeded = float.TryParse(i_FillEnergyList[0] , out minutesToAdd);
            if(parseSucceeded == false)
            {
                throw new FormatException();
            }
            this.m_ElectricSystem.BatteryCharging(minutesToAdd / 60);
        }
    }
}
