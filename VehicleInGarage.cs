using System;
using System.Collections.Generic;

namespace Vehicles
{
    public class VehicleInGarage
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhone;
        private eVehicleCondition m_VehicleCondition;
        private readonly Vehicle r_OwnerVehicle;

        public VehicleInGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_OwnerVehicle, Dictionary<string, string> i_ParametersDict )
        {
            this.r_OwnerName = i_OwnerName;
            this.r_OwnerPhone = i_OwnerPhone;
            this.r_OwnerVehicle = i_OwnerVehicle;
            this.m_VehicleCondition = eVehicleCondition.InRepair;
        }

        public string OwnerName
        {
            get { return this.r_OwnerName; }
        }

        public string OwnerPhone
        {
            get { return this.r_OwnerPhone; }
        }

        public eVehicleCondition VehicleCondition
        {
            get { return this.m_VehicleCondition; }
            set { this.m_VehicleCondition = value; }
        }

        public Vehicle OwnerVehicle
        {
            get { return this.r_OwnerVehicle; }
        }

        public static List<string> VehicleLicenseNumberListFilterByCondition(List<VehicleInGarage> i_VehicleList, eVehicleCondition i_VehicleCondition)
        {
            List<string> vehicleList = new List<string>();
            foreach (VehicleInGarage vehicle in i_VehicleList)
            {
                if(vehicle.VehicleCondition == i_VehicleCondition)
                {
                    vehicleList.Add(vehicle.OwnerVehicle.LicenseNumber);
                }
            }

            return vehicleList;
        }
        public static void ChangeVehicleCondition(List<VehicleInGarage> i_VehicleList, string i_LicenseNumber, eVehicleCondition i_VehicleCondition)
        {
            bool findLicenseNumber = false;
            foreach(VehicleInGarage vehicle in i_VehicleList)
            {
                if(vehicle.OwnerVehicle.LicenseNumber == i_LicenseNumber)
                {
                    findLicenseNumber = true;
                    vehicle.VehicleCondition = i_VehicleCondition;
                    break;
                }
            }
            if (findLicenseNumber == false)
            {
                throw new ArgumentException("Error: License number not found.");
            }
        }

        public static void TireInflationToTheMaximum(List<VehicleInGarage> i_VehicleList, string i_LicenseNumber)
        {
            Wheel[] wheelsList;
            bool findLicenseNumber = false;

            foreach (VehicleInGarage vehicle in i_VehicleList)
            {
                if (vehicle.OwnerVehicle.LicenseNumber == i_LicenseNumber)
                {
                    findLicenseNumber = true;
                    wheelsList = vehicle.r_OwnerVehicle.WheelsArray;
                    foreach(Wheel wheel  in wheelsList)
                    {
                        wheel.TireInflation(wheel.MaximumAirPressureByManufacturer - wheel.CurrentAirPressure);
                    }

                    break;
                }
            }

            if (findLicenseNumber == false)
            {
                throw new ArgumentException("Error: License number not found.");
            }
        }
        
        public static void AddFuel(List<VehicleInGarage> i_VehicleList, string i_LicenseNumber, string i_FuelType, string i_FuelToAdd)
        {
            List<string> parametersList = new List<string>();
            parametersList.Add(i_FuelToAdd);
            parametersList.Add(i_FuelType);
            Type checkType;
            bool findLicenseNumber = false;

            foreach (VehicleInGarage vehicle in i_VehicleList)
            {
                if (vehicle.OwnerVehicle.LicenseNumber == i_LicenseNumber)
                {
                    findLicenseNumber = true;
                    checkType = vehicle.OwnerVehicle.GetSystemType();
                    if (checkType != typeof(FuelVehicleSystem))
                    {
                        throw new ArgumentException("Error: It is not possible to refuel a vehicle Because it doesn't have a fuel system.");
                    }
                    vehicle.OwnerVehicle.FillEnergy(parametersList);
                    break;
                }
            }

            if (findLicenseNumber == false)
            {
                throw new ArgumentException("Error: License number not found.");
            }
        }
        
        
        public static void Charging(List<VehicleInGarage> i_VehicleList, string i_LicenseNumber, string i_ChargeToAddInMinutes)
        {
            List<string> parametersList = new List<string>();
            parametersList.Add(i_ChargeToAddInMinutes);
            Type checkType;
            bool findLicenseNumber = false;

            foreach (VehicleInGarage vehicle in i_VehicleList)
            {
                if (vehicle.OwnerVehicle.LicenseNumber == i_LicenseNumber)
                {
                    findLicenseNumber = true;
                    checkType = vehicle.OwnerVehicle.GetSystemType();
                    if (checkType != typeof(ElectricVehicleSystem))
                    {
                        throw new ArgumentException("Error: It is not possible to Charge a vehicle Because it doesn't have a electric system.");
                    }
                    vehicle.OwnerVehicle.FillEnergy(parametersList);
                    break;
                }
            }

            if(findLicenseNumber == false)
            {
                throw new ArgumentException("Error: License number not found.");
            }
        }
        public static string VehicleConditionToString(eVehicleCondition i_Condition)
        {
            string condition = "";
            switch(i_Condition)
            {
                case eVehicleCondition.InRepair:
                    condition = "InRepair";
                    break;
                case eVehicleCondition.Paid:
                    condition = "Paid";
                    break;
                case eVehicleCondition.Repaired:
                    condition = "Repaired";
                    break;
            }

            return condition;
        }

        public static Dictionary<string, string> ShowVehicleData(List<VehicleInGarage> i_VehicleList, string i_LicenseNumber)
        {
            Dictionary<string, string> dataDict = new Dictionary<string, string>();
            bool findLicenseNumber = false;

            foreach (VehicleInGarage vehicle in i_VehicleList)
            {
                if (vehicle.OwnerVehicle.LicenseNumber == i_LicenseNumber)
                {
                    findLicenseNumber = true;
                    dataDict.Add("Owner name", vehicle.OwnerName);
                    dataDict.Add("Owner phone", vehicle.OwnerPhone);
                    dataDict.Add("Vehicle condition", VehicleConditionToString(vehicle.m_VehicleCondition));
                    vehicle.OwnerVehicle.GetData(dataDict);
                    break;
                }
            }

            if (findLicenseNumber == false)
            {
                throw new ArgumentException("Error: License number not found.");
            }

            return dataDict;
        }
    }
}
