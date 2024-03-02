using System;
using System.Collections.Generic;

namespace Vehicles
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_EnergyPercentage;
        protected Wheel[] m_WheelsArray;

        public Vehicle(int i_NumberOfWheels , float i_MaxAirPressure)
        {
            this.m_WheelsArray = new Wheel[i_NumberOfWheels];
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel(i_MaxAirPressure);
                this.m_WheelsArray[i] = wheel;
            }
        }

        public string ModelName
        {
            get { return this.m_ModelName; }
            set { this.m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return this.m_LicenseNumber; }
            set { this.m_LicenseNumber = value; }
        }

        public float EnergyPercentage
        {
            get { return this.m_EnergyPercentage; }
            set { this.m_EnergyPercentage = value; }
        }

        public Wheel[] WheelsArray
        {
            get { return this.m_WheelsArray; }
            set { this.m_WheelsArray = value; }
        }

        public virtual void GetParameters(List<string> i_ParametersArray)
        {
            i_ParametersArray.Add("license number"); 
            i_ParametersArray.Add("model name"); 
            i_ParametersArray.Add("wheels manufacturer name");
            i_ParametersArray.Add("current air pressure");
        }

        public virtual void SetParameters(Dictionary<string, string> i_SetParametersDict, List<string> i_SetWheelsAirPressure)
        {
            foreach (string param in i_SetParametersDict.Keys)
            {
                switch(param)
                {
                    case "model name":
                        this.m_ModelName = i_SetParametersDict[param];
                        break;
                    case "license number":
                        this.m_LicenseNumber = i_SetParametersDict[param];
                        break;
                }
            }

            InitilazeWheels(i_SetParametersDict["wheels manufacturer name"], i_SetWheelsAirPressure);
        }

        public virtual void GetData(Dictionary<string, string> i_DataDict)
        {
            string stringFormat;
            i_DataDict.Add("model name", this.m_ModelName);
            i_DataDict.Add("wheels manufacturer name", this.m_WheelsArray[0].ManufacturerName);
            i_DataDict.Add("maximum air pressure", this.m_WheelsArray[0].MaximumAirPressureByManufacturer.ToString());
            for (int i = 0; i < this.m_WheelsArray.Length; i++)
            {
                stringFormat = string.Format("current air pressure" + i);
                i_DataDict.Add(stringFormat, this.m_WheelsArray[i].CurrentAirPressure.ToString());
            }
            i_DataDict.Add("Energy percentage", this.m_EnergyPercentage.ToString());
            i_DataDict.Add("number of wheels", this.m_WheelsArray.Length.ToString());
        }

        public void InitilazeWheels( string i_ManufacturerName, List<string> i_SetWheelsAirPressure)
        {
            bool parseSucceeded = false;
            float currentAirPressure = 0;
            int i = 0;

            foreach (Wheel wheel in this.m_WheelsArray)
            {
                parseSucceeded = float.TryParse(i_SetWheelsAirPressure[i], out currentAirPressure);
                i++;
                if (parseSucceeded == false)
                {
                    throw new FormatException();
                }
                else if (currentAirPressure > wheel.MaximumAirPressureByManufacturer || currentAirPressure < 0)
                {
                    throw new ValueOutOfRangeException("Current air pressure", wheel.MaximumAirPressureByManufacturer, 0);
                }
                else
                {
                    wheel.CurrentAirPressure = currentAirPressure;
                    wheel.ManufacturerName = i_ManufacturerName;
                }
            }
        }

        public abstract Type GetSystemType();

        public abstract void FillEnergy(List<string> i_FillEnergyList);
    }
}
