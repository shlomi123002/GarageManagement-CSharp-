using System;
using System.Collections.Generic;

namespace Vehicles
{
    public abstract class Truck : Vehicle
    {
        private bool m_TransportsDangerMaterials;
        private float m_CargoVolume;

        public Truck() : base(12, 28f) { }

        public float CargoVolume
        {
            get { return this.m_CargoVolume; }
            set { this.m_CargoVolume = value; }
        }

        public bool TransportsDangerMaterials
        {
            get { return this.m_TransportsDangerMaterials; }
            set { this.m_TransportsDangerMaterials = value; }
        }

        public override void GetParameters(List<string> i_ParametersArray)
        {
            base.GetParameters(i_ParametersArray);
            i_ParametersArray.Add("transports danger materials(yes or no)");
            i_ParametersArray.Add("cargo volume");
        }

        public override void SetParameters(Dictionary<string, string> i_SetParametersDict, List<string> i_SetWheelsAirPressure)
        {
            base.SetParameters(i_SetParametersDict, i_SetWheelsAirPressure);
            bool parseSucceeded = false;
            float cargoVolume = 0;

            foreach (string param in i_SetParametersDict.Keys)
            {
                switch (param)
                {
                    case "transports danger materials(yes or no)":
                        SetTransportsDangerMaterials(i_SetParametersDict[param]);
                        break;
                    case "cargo volume":
                        parseSucceeded = float.TryParse(i_SetParametersDict[param], out cargoVolume);
                        if(parseSucceeded == false)
                        {
                            throw new FormatException();
                        }
                        this.m_CargoVolume = cargoVolume;
                        break;
                }
            }
        }

        public void SetTransportsDangerMaterials(string i_TransportsDangerMaterials)
        {
            switch (i_TransportsDangerMaterials)
            {
                case "yes":
                    this.m_TransportsDangerMaterials = true;
                    break;
                case "no":
                    this.m_TransportsDangerMaterials = false;
                    break;
                default:
                    throw new ArgumentException("Error: Invalid answer of transport danger materials.");
            }
        }

        public override void GetData(Dictionary<string, string> i_DataDict)
        {
            base.GetData(i_DataDict);
            string Transport = "";
            if(this.m_TransportsDangerMaterials == true)
            {
                Transport = "Yes";
            }
            else
            {
                Transport = "No";
            }
            i_DataDict.Add("transports danger materials(yes or no)", Transport);
            i_DataDict.Add("cargo volume", this.m_CargoVolume.ToString());
        }
    }
}
