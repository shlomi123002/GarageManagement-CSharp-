using System;

namespace Vehicles
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaximumAirPressureByManufacturer;

        public Wheel(float i_MaximumAirPressure)
        {
            this.r_MaximumAirPressureByManufacturer = i_MaximumAirPressure;
        }

        public string ManufacturerName
        {
            get { return this.m_ManufacturerName; }
            set { this.m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return this.m_CurrentAirPressure; }
            set { this.m_CurrentAirPressure = value; }
        }

        public float MaximumAirPressureByManufacturer
        {
            get { return this.r_MaximumAirPressureByManufacturer; }
        }

        public void TireInflation(float i_AirPressureToAdd) 
        {
            if (i_AirPressureToAdd + this.m_CurrentAirPressure > this.r_MaximumAirPressureByManufacturer || i_AirPressureToAdd + this.m_CurrentAirPressure < 0)
            {
                throw new ValueOutOfRangeException("Air pressure to add", this.r_MaximumAirPressureByManufacturer - this.m_CurrentAirPressure, 0f);
            }
            else
            {
                this.m_CurrentAirPressure += i_AirPressureToAdd;
            }
        }
    }
}
