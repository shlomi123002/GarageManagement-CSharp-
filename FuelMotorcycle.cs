using System;
using System.Collections.Generic;

namespace Vehicles
{
    public class FuelMotorcycle : Motorcycle
    {
        private FuelVehicleSystem m_FuelSystem;

        public FuelMotorcycle()
        {
            this.m_FuelSystem = new FuelVehicleSystem(eFuelType.Octan98, 5.8f);
        }

        public FuelVehicleSystem FuelSystem
        {
            get { return this.m_FuelSystem; }
            set { this.m_FuelSystem = value; }
        }

        public override void GetParameters(List<string> i_ParametersArray)
        {
            base.GetParameters(i_ParametersArray);
            i_ParametersArray.Add("amount fuel in liters");
        }

        public override void SetParameters(Dictionary<string, string> i_SetParametersDict, List<string> i_SetWheelsAirPressure)
        {
            base.SetParameters(i_SetParametersDict, i_SetWheelsAirPressure);
            this.m_FuelSystem.SetFuelSystemParameters(i_SetParametersDict);
            this.m_EnergyPercentage = (this.m_FuelSystem.CurrentAmountFuelInLiters / this.m_FuelSystem.MaximumAmountFuelInLiters) * 100;
        }

        public override void GetData(Dictionary<string, string> i_DataDict)
        {
            base.GetData(i_DataDict);
            string fuelType = "";
            switch (this.m_FuelSystem.FuelType)
            {
                case eFuelType.Octan95:
                    fuelType = "Octan95";
                    break;
                case eFuelType.Octan96:
                    fuelType = "Octan96";
                    break;
                case eFuelType.Octan98:
                    fuelType = "Octan98";
                    break;
                case eFuelType.Soler:
                    fuelType = "Soler";
                    break;
            }

            i_DataDict.Add("Maximum amount fuel in liters", this.m_FuelSystem.MaximumAmountFuelInLiters.ToString());
            i_DataDict.Add("amount fuel in liters", this.m_FuelSystem.CurrentAmountFuelInLiters.ToString());
            i_DataDict.Add("Fuel type", fuelType);
        }

        public override Type GetSystemType()
        {
            return typeof(FuelVehicleSystem);
        }

        public override void FillEnergy(List<string> i_FillEnergyDict)
        {
            float litersToAdd;
            bool parseSucceeded = float.TryParse(i_FillEnergyDict[0], out litersToAdd);
            if (parseSucceeded == false)
            {
                throw new FormatException();
            }
            eFuelType fuelType = eFuelType.Octan95;
            switch (i_FillEnergyDict[1])
            {
                case "Octan95":
                    fuelType = eFuelType.Octan95;
                    break;
                case "Octan96":
                    fuelType = eFuelType.Octan96;
                    break;
                case "Octan98":
                    fuelType = eFuelType.Octan98;
                    break;
                case "Soler":
                    fuelType = eFuelType.Soler;
                    break;
            }
            this.m_FuelSystem.Refueling(litersToAdd, fuelType);
        }
    }
}
