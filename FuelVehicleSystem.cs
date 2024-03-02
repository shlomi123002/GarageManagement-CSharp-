using System;
using System.Collections.Generic;

namespace Vehicles
{
    public class FuelVehicleSystem
    {
        private readonly eFuelType r_FuelType;
        private float m_CurrentAmountFuelInLiters;
        private readonly float r_MaximumAmountFuelInLiters;

        public FuelVehicleSystem(eFuelType fuelType, float MaximumAmountFuelInLiters)
        {
            this.r_FuelType = fuelType;
            this.r_MaximumAmountFuelInLiters = MaximumAmountFuelInLiters;
        }

        public eFuelType FuelType
        {
            get { return this.r_FuelType; }
        }

        public float CurrentAmountFuelInLiters
        {
            get { return this.m_CurrentAmountFuelInLiters; }
            set { this.m_CurrentAmountFuelInLiters = value; }
        }

        public float MaximumAmountFuelInLiters
        {
            get { return this.r_MaximumAmountFuelInLiters; }
        }
        
        public void Refueling(float i_AmoutFuelInLitersToAdd, eFuelType i_FuelType)
        {
            if (this.r_FuelType != i_FuelType)
            {
                throw new ArgumentException("The type of fuel entered does not match the type of vehicle");
            }
            else if (i_AmoutFuelInLitersToAdd + this.m_CurrentAmountFuelInLiters > this.r_MaximumAmountFuelInLiters || i_AmoutFuelInLitersToAdd + this.m_CurrentAmountFuelInLiters < 0)
            {
                throw new ValueOutOfRangeException("Amount fuel to add", this.r_MaximumAmountFuelInLiters - this.m_CurrentAmountFuelInLiters, 0f);
            }
            else
            {
                this.m_CurrentAmountFuelInLiters += i_AmoutFuelInLitersToAdd;
            }
        }

        public void SetFuelSystemParameters(Dictionary<string, string> i_SetParametersDict)
        {
            float fuelAmount = 0;
            bool setFuelSecceeded = false;

            foreach (string param in i_SetParametersDict.Keys)
            {
                switch (param)
                {
                    case "amount fuel in liters":
                        setFuelSecceeded = float.TryParse(i_SetParametersDict[param] , out fuelAmount); 
                        if(setFuelSecceeded == false)
                        {
                            throw new FormatException();
                        }
                        else if (fuelAmount < 0 || fuelAmount > this.r_MaximumAmountFuelInLiters)
                        {
                            throw new ValueOutOfRangeException("Fuel amount", this.r_MaximumAmountFuelInLiters , 0f);
                        }
                        else
                        {
                            this.m_CurrentAmountFuelInLiters = fuelAmount;
                        }
                        break;
                }
            }
        }
    }
}
