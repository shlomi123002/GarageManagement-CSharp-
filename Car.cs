using System;
using System.Collections.Generic;

namespace Vehicles
{
    public abstract class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors;

        public Car() : base(5, 30f) { }

        public eCarColor CarColor
        {
            get { return this.m_CarColor; }
            set { this.m_CarColor = value; }
        }

        public eNumOfDoors NumOfDoors
        {
            get { return this.m_NumOfDoors; }
            set { this.m_NumOfDoors = value; }
        }

        public override void GetParameters(List<string> i_ParametersArray)
        {
            base.GetParameters(i_ParametersArray);
            i_ParametersArray.Add("car color");
            i_ParametersArray.Add("number of doors");
        }

        public override void SetParameters(Dictionary<string, string> i_SetParametersDict, List<string> i_SetWheelsAirPressure)
        {
            base.SetParameters(i_SetParametersDict, i_SetWheelsAirPressure);
            foreach (string param in i_SetParametersDict.Keys)
            {
                switch (param)
                {
                    case "car color":
                        SetCarColor(i_SetParametersDict[param]);
                        break;
                    case "number of doors":
                        SetNumberOfDoors(i_SetParametersDict[param]);
                        break;
                }
            }
        }

        public void SetCarColor(string i_Color)
        {
            switch (i_Color)
            {
                case "white":
                    this.m_CarColor = eCarColor.White;
                    break;
                case "blue":
                    this.m_CarColor = eCarColor.Blue;
                    break;
                case "red":
                    this.m_CarColor = eCarColor.Red;
                    break;
                case "yellow":
                    this.m_CarColor = eCarColor.Yellow;
                    break;
                default:
                    throw new ArgumentException("Error: Invalid color.");
            }
        }
        public void SetNumberOfDoors(string i_NumberOfDoors)
        {
            switch (i_NumberOfDoors)
            {
                case "2":
                    this.m_NumOfDoors = eNumOfDoors.TwoDoors;
                    break;
                case "3":
                    this.m_NumOfDoors = eNumOfDoors.ThreeDoors;
                    break;
                case "4":
                    this.m_NumOfDoors = eNumOfDoors.FourDoors;
                    break;
                case "5":
                    this.m_NumOfDoors = eNumOfDoors.FiveDoors;
                    break;
                default:
                    throw new ArgumentException("Error: Invalid doors number.");
            }
        }

        public override void GetData(Dictionary<string, string> i_DataDict)
        {
            base.GetData(i_DataDict);
            string carColor = "";
            string numOfDoors = "";

           switch(this.m_CarColor)
            {
                case eCarColor.White:
                    carColor = "White";
                    break;
                case eCarColor.Blue:
                    carColor = "Blue";
                    break;
                case eCarColor.Red:
                    carColor = "Red";
                    break;
                case eCarColor.Yellow:
                    carColor = "Yellow";
                    break;
            }

            switch(this.m_NumOfDoors)
            {
                case eNumOfDoors.TwoDoors:
                    numOfDoors = "2";
                    break;
                case eNumOfDoors.ThreeDoors:
                    numOfDoors = "3";
                    break;
                case eNumOfDoors.FourDoors:
                    numOfDoors = "4";
                    break;
                case eNumOfDoors.FiveDoors:
                    numOfDoors = "5";
                    break;
            }

            i_DataDict.Add("car color", carColor);
            i_DataDict.Add("number of doors", numOfDoors);
        }
    }
}
