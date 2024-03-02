using System;
using System.Collections.Generic;

namespace Vehicles
{
    public class ElectricVehicleSystem
    {
        private float m_BatteryTimeRemainingInHours;
        private readonly float r_BatteryTimeMaximumInHours;

        public ElectricVehicleSystem(float BatteryLife)
        {
            this.r_BatteryTimeMaximumInHours = BatteryLife;
        }

        public float BatteryTimeRemainingInHours
        {
            get { return this.m_BatteryTimeRemainingInHours; }
            set { this.m_BatteryTimeRemainingInHours = value; }
        }

        public float BatteryTimeMaximumInHours
        {
            get { return this.r_BatteryTimeMaximumInHours; }
        }

        public void BatteryCharging(float i_HoursToAdd)
        {
            if (i_HoursToAdd + this.m_BatteryTimeRemainingInHours > this.r_BatteryTimeMaximumInHours)
            {
                throw new ValueOutOfRangeException("Charge", this.r_BatteryTimeMaximumInHours - this.BatteryTimeRemainingInHours, 0f);
            }
            else
            {
                this.m_BatteryTimeRemainingInHours += i_HoursToAdd;
            }
        }

        public void SetElectricSystemParameters(Dictionary<string, string> i_SetParametersDict)
        {
            float batteryTime = 0;
            bool parseSucceeded = false;

            foreach (string param in i_SetParametersDict.Keys)
            {
                switch (param)
                {
                    case "battery time remaining in hours":
                        parseSucceeded = float.TryParse(i_SetParametersDict[param], out batteryTime);
                        if(parseSucceeded == false)
                        {
                            throw new FormatException();
                        }
                        else if (batteryTime < 0 || batteryTime > this.r_BatteryTimeMaximumInHours)
                        {
                            throw new ValueOutOfRangeException("Battery time", this.r_BatteryTimeMaximumInHours, 0);
                        }
                        else
                        {
                            this.m_BatteryTimeRemainingInHours = batteryTime;
                        }
                        break;
                }
            }
        }
    }
}
