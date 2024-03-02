using System;
using System.Collections.Generic;

namespace Vehicles
{
    public abstract class Motorcycle : Vehicle
    {
        private eMotorcycleLicenseTypes m_LicenceType;
        private int m_EngineVolumeInCC;

        public Motorcycle() : base(2, 29f) { }

        public eMotorcycleLicenseTypes LicenceType
        {
            get { return this.m_LicenceType; }
            set { this.m_LicenceType = value; }
        }

        public int EngineVolumeInCC
        {
            get { return this.EngineVolumeInCC; }
            set { this.EngineVolumeInCC = value; }
        }

        public override void GetParameters(List<string> i_ParametersArray)
        {
            base.GetParameters(i_ParametersArray);
            i_ParametersArray.Add("licence type");
            i_ParametersArray.Add("engine volume in CC");
        }

        public override void SetParameters(Dictionary<string, string> i_SetParametersDict, List<string> i_SetWheelsAirPressure)
        {
            base.SetParameters(i_SetParametersDict, i_SetWheelsAirPressure);
            bool parseSucceeeded = true;
            int engineVolume = 0;
            
            foreach (string param in i_SetParametersDict.Keys)
            {
                switch (param)
                {
                    case "licence type":
                        SetLicenceType(i_SetParametersDict[param]);
                        break;
                    case "engine volume in CC":
                        parseSucceeeded = int.TryParse(i_SetParametersDict[param], out engineVolume);
                        if(parseSucceeeded == false)
                        {
                            throw new FormatException();
                        }
                        this.m_EngineVolumeInCC = engineVolume;
                        break;
                }
            }
        }

        public void SetLicenceType(string i_LicenceType)
        {
            switch (i_LicenceType)
            {
                case "A1":
                    this.m_LicenceType = eMotorcycleLicenseTypes.A1;
                    break;
                case "A2":
                    this.m_LicenceType = eMotorcycleLicenseTypes.A2;
                    break;
                case "AB":
                    this.m_LicenceType = eMotorcycleLicenseTypes.AB;
                    break;
                case "B2":
                    this.m_LicenceType = eMotorcycleLicenseTypes.B2;
                    break;
                default:
                    throw new ArgumentException("Error: Invalid license type.");
            }
        }

        public override void GetData(Dictionary<string, string> i_DataDict)
        {
            base.GetData(i_DataDict);
            string licenseType = "";
            switch (this.m_LicenceType)
            {
                case eMotorcycleLicenseTypes.A1:
                    licenseType = "A1";
                    break;
                case eMotorcycleLicenseTypes.A2:
                    licenseType = "A2";
                    break;
                case eMotorcycleLicenseTypes.AB:
                    licenseType = "AB";
                    break;
                case eMotorcycleLicenseTypes.B2:
                    licenseType = "B2";
                    break;
            }

            i_DataDict.Add("license type", licenseType);
            i_DataDict.Add("engine volume in CC", this.m_EngineVolumeInCC.ToString());
        }
    }
}
