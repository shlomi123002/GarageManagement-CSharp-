using System;
using System.Collections.Generic;
using Vehicles;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        private Factory m_Factory;
        private List<VehicleInGarage> m_VehiclesInGarageArray;
        public GarageUI()
        {
            this.m_Factory = new Factory();
            this.m_VehiclesInGarageArray = new List<VehicleInGarage>();
        }
        public Factory Factory
        {
            get { return this.m_Factory; }
            set { this.m_Factory = value; }
        }

        public List<VehicleInGarage> VehiclesInGarageArray
        {
            get { return this.m_VehiclesInGarageArray; }
            set { this.m_VehiclesInGarageArray = value; }
        }

        public void GarageSoftware()
        {
            Console.WriteLine("Welcome To The Garage Management System\n");
            string userInput = "";
            string licenseNumber = "";
            while (true)
            {
                userInput = ChooseOption();
                Console.Clear();
                if (userInput == "3" || userInput == "4" || userInput == "5" || userInput == "6" || userInput == "7")
                {
                    licenseNumber = ChooseLicenseNumber();
                }

                switch (userInput)
                {
                    case "1":
                        NewCarIntoTheGarage();
                        break;
                    case "2":
                        GetListVehiclesByCondition();
                        break;
                    case "3":
                        ChangeVehicleConditionByLicenseNumber(licenseNumber);
                        break;
                    case "4":
                        TireInflation(licenseNumber);
                        break;
                    case "5":
                        RefuelVehicle(licenseNumber);
                        break;
                    case "6":
                        ChargeVehicle(licenseNumber);
                        break;
                    case "7":
                        VehicleDataByLicenseNumber(licenseNumber);
                        break;
                    case "8":
                        goto end;
                    default:
                        Console.WriteLine("Try again , choose option between 1 - 8 "); 
                        break;
                }

                Console.WriteLine("\nPress any key for return to home page...");
                Console.ReadKey();
                Console.Clear();
            }
            end:;
        }

        public string ChooseOption()
        {
            string userInput = "";
            Console.WriteLine("========== Garage Home Page ==========\n");
            Console.WriteLine("Choose one of the following options :");
            Console.WriteLine("[1] - Add new vehicle");
            Console.WriteLine("[2] - Get list of vehicles that filter by condition");
            Console.WriteLine("[3] - Change the status of a vehicle");
            Console.WriteLine("[4] - To inflate the tires of a vehicle to the maximum");
            Console.WriteLine("[5] - Refuel a vehicle");
            Console.WriteLine("[6] - Charge a vehicle");
            Console.WriteLine("[7] - View full vehicle data");
            Console.WriteLine("[8] - Exit");
            userInput = Console.ReadLine();
            return userInput;
        }

        public void NewCarIntoTheGarage()
        {
            string ownerName, ownerPhoneNumber , licenseNumber;
            Vehicle someVehicle;
            string stringFormat;
            List<string> getParametersArray = new List<string>();
            List<string> setWheelsAirPressure = new List<string>();
            Dictionary<string, string> setParametersDict = new Dictionary<string, string>();

            Console.Write("Enter your name :");
            ownerName = Console.ReadLine();
            Console.Write("Enter your phone number :");
            ownerPhoneNumber = Console.ReadLine();
            someVehicle = m_Factory.CreateSpecificVehicle(GetVehicleType());
            someVehicle.GetParameters(getParametersArray);
            Console.Write("Enter license number : ");
            licenseNumber = Console.ReadLine();
            try
            {
                if (!LicenseNumberIsExist(licenseNumber, this.m_VehiclesInGarageArray))
                {
                    setParametersDict.Add("license number", licenseNumber);
                    foreach (string param in getParametersArray)
                    {
                        if(param == "current air pressure")
                        {

                            SetWheelsAirPressure(someVehicle , setWheelsAirPressure);
                        }
                        else if(param == "car color")
                        {
                            setParametersDict.Add(param, ChooseCarColor());
                        }
                        else if(param == "number of doors") 
                        {
                            setParametersDict.Add(param, ChooseNumberOfDoors());
                        }
                        else if (param == "licence type")
                        {
                            setParametersDict.Add(param, ChooseLicenseType());
                        }
                        else if (param == "transports danger materials(yes or no)")
                        {
                            setParametersDict.Add(param, ChooseTransportsDangerMaterials());
                        }
                        else if (param != "license number")
                        {
                            stringFormat = String.Format("Enter {0} : ", param);
                            Console.Write(stringFormat);
                            setParametersDict.Add(param, Console.ReadLine());
                        }
                    }
                    someVehicle.SetParameters(setParametersDict, setWheelsAirPressure);
                    VehicleInGarage newCarIntoGarage = new VehicleInGarage(ownerName, ownerPhoneNumber, someVehicle, setParametersDict);
                    this.m_VehiclesInGarageArray.Add(newCarIntoGarage);
                }
                else
                {
                    throw new ArgumentException("Error: The vehicle with this license number is already in the garage.");
                }
            }
            catch(ArgumentException ae)
            {
                stringFormat = String.Format("Eror: " + ae.Message);
                Console.WriteLine(stringFormat);
            }
            catch (FormatException fe)
            {
                stringFormat = String.Format("Error: " + fe.Message);
                Console.WriteLine(stringFormat);
            }
            catch (ValueOutOfRangeException re)
            {
                stringFormat = String.Format("Error: " + re.Message);
                Console.WriteLine(stringFormat);
            }
        }
        public string ChooseCarColor()
        {
            string color;
            string userInput;
            Console.WriteLine("car color :");
            Console.WriteLine("[1] - White");
            Console.WriteLine("[2] - Blue");
            Console.WriteLine("[3] - Red");
            Console.WriteLine("[4] - Yellow");
            userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    color = "white";
                    break;
                case "2":
                    color = "blue";
                    break;
                case "3":
                    color = "red";
                    break;
                case "4":
                    color = "yellow";
                    break;
                default:
                    throw new ArgumentException("Error: Invalid color.");
            }

            return color;
        }
        public string ChooseNumberOfDoors()
        {
            string numberOfDoors;
            string userInput;
            Console.WriteLine("number of doors :");
            Console.WriteLine("[1] - two doors");
            Console.WriteLine("[2] - three doors");
            Console.WriteLine("[3] - four doors");
            Console.WriteLine("[4] - five doors");
            userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    numberOfDoors = "2";
                    break;
                case "2":
                    numberOfDoors = "3";
                    break;
                case "3":
                    numberOfDoors = "4";
                    break;
                case "4":
                    numberOfDoors = "5";
                    break;
                default:
                    throw new ArgumentException("Error: Invalid number of doors.");
            }

            return numberOfDoors;
        }
        public string ChooseLicenseType()
        {
            string LicenseType;
            string userInput;
            Console.WriteLine("license Type :");
            Console.WriteLine("[1] - A1");
            Console.WriteLine("[2] - A2");
            Console.WriteLine("[3] - AB");
            Console.WriteLine("[4] - B2");
            userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    LicenseType = "A1";
                    break;
                case "2":
                    LicenseType = "A2";
                    break;
                case "3":
                    LicenseType = "AB";
                    break;
                case "4":
                    LicenseType = "B2";
                    break;
                default:
                    throw new ArgumentException("Error: Invalid license type.");
            }

            return LicenseType;
        }
        public string ChooseTransportsDangerMaterials()
        {
            string transport;
            string userInput;
            Console.WriteLine("transports danger materials(yes or no) :");
            Console.WriteLine("[1] - Yes");
            Console.WriteLine("[2] - No");
            
            userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    transport = "Yes";
                    break;
                case "2":
                    transport = "No";
                    break;
                default:
                    throw new ArgumentException("Error: Invalid choise.");
            }

            return transport;
        }

        public void SetWheelsAirPressure(Vehicle i_Vehicle , List<string> i_SetWheelsAirPressure)
        {
            string stringFormat;
            string userInput;
            string airPressure;
            Console.WriteLine("set air pressure");
            Console.WriteLine("[1] - to update all wheels to the same value");
            Console.WriteLine("[2] - To update each wheel individually");
            userInput = Console.ReadLine();

            if (userInput == "1")
            {
                stringFormat = string.Format("wheels air pressure :");
                Console.Write(stringFormat);
                airPressure = Console.ReadLine();
                for (int i = 0; i < i_Vehicle.WheelsArray.Length; i++)
                {
                    i_SetWheelsAirPressure.Add(airPressure);
                }
            }
            else if(userInput == "2")
            {
                for (int i = 0; i < i_Vehicle.WheelsArray.Length; i++)
                {
                    stringFormat = string.Format("wheel number {0} :", i + 1);
                    Console.Write(stringFormat);
                    airPressure = Console.ReadLine();
                    i_SetWheelsAirPressure.Add(airPressure);
                }
            }
            else
            {
                throw new ValueOutOfRangeException("Set air pressure", 2, 1);
            }
        }

        public bool LicenseNumberIsExist(string i_LicenseNumber, List<VehicleInGarage> i_VehicleList)
        {
            bool licenseNumberExist = false;
            foreach (VehicleInGarage vehicle in i_VehicleList)
            {
                if (vehicle.OwnerVehicle.LicenseNumber == i_LicenseNumber)
                {
                    licenseNumberExist = true;
                    break;
                }
            }
            return licenseNumberExist;
        }

        public string GetVehicleType()
        {
            int i = 1;
            string vehicleType = "";
            string userInput ;
            string stringFormat = String.Format("Enter your vehicle type that you want to put in the garage (press 1 - {0}) :", m_Factory.VehicleList.Length);
            int userChoise;
            bool parseSucceeded = false;

            Console.WriteLine(stringFormat);
            foreach (string type in m_Factory.VehicleList)
            {
                stringFormat = String.Format("[{0}] - {1}", i++, type);
                Console.WriteLine(stringFormat);
            }

            userInput = Console.ReadLine();
            try
            {
                parseSucceeded = int.TryParse(userInput, out userChoise);
                if (parseSucceeded)
                {
                        
                    if (userChoise >= 1 && userChoise <= this.m_Factory.VehicleList.Length)
                    {
                        vehicleType = m_Factory.VehicleList[userChoise - 1];
                    }
                    else
                    {
                        throw new ValueOutOfRangeException("Vehicle type", m_Factory.VehicleList.Length, 1);
                    }
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException fe)
            {
                stringFormat = String.Format("Error: " + fe.Message);
                Console.WriteLine(stringFormat);
            }
            catch(ValueOutOfRangeException re)
            {
                stringFormat = String.Format("Error: " + re.Message);
                Console.WriteLine(stringFormat);
            }
            
            return vehicleType;
        }
        public void GetListVehiclesByCondition()
        {
            List<string> vehiclesList;
            string stringFormat;

            try
            {
                vehiclesList = VehicleInGarage.VehicleLicenseNumberListFilterByCondition(m_VehiclesInGarageArray, ChooseCondition());
                Console.WriteLine("Vehicles list : ");
                foreach (string licenseNumber in vehiclesList)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
            catch(ValueOutOfRangeException re)
            {
                stringFormat = String.Format("Error: "+re.Message);
                Console.WriteLine(stringFormat);
            }
        }

        public eVehicleCondition ChooseCondition()
        {
            string userInput;
            eVehicleCondition condition = eVehicleCondition.InRepair;

            Console.WriteLine("Choose vehicle condition :");
            Console.WriteLine("[1] - In repair");
            Console.WriteLine("[2] - Repaired");
            Console.WriteLine("[3] - Paid");
            userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    condition = eVehicleCondition.InRepair;
                    break;
                case "2":
                    condition = eVehicleCondition.Repaired;
                    break;
                case "3":
                    condition = eVehicleCondition.Paid;
                    break;
                default:
                    throw new ValueOutOfRangeException("Vehicle condition", 3, 1);
            }

            return condition;
        }

        public string ChooseLicenseNumber()
        {
            string licenseNumber;

            Console.Write("Enter license number of Vehicle :");
            licenseNumber = Console.ReadLine();
            return licenseNumber;
        }

        public void ChangeVehicleConditionByLicenseNumber(string i_LicenseNumber)
        {
            string stringFormat;
            try
            {
                VehicleInGarage.ChangeVehicleCondition(m_VehiclesInGarageArray, i_LicenseNumber, ChooseCondition());
            }
            catch (ValueOutOfRangeException re)
            {
                stringFormat = string.Format("Error: " + re.Message);
                Console.WriteLine(stringFormat);
            }
            catch(ArgumentException ae)
            {
                stringFormat = string.Format("Error: " + ae.Message);
                Console.WriteLine(stringFormat);
            }
        }

        public void TireInflation(string i_LicenseNumber)
        {
            string stringFormat;
            try
            {
                VehicleInGarage.TireInflationToTheMaximum(m_VehiclesInGarageArray, i_LicenseNumber);
            }
            catch(ValueOutOfRangeException re)
            {
                stringFormat = string.Format("Error: " + re.Message);
                Console.WriteLine(stringFormat);
            }
            catch (ArgumentException ae)
            {
                stringFormat = string.Format("Error: " + ae.Message);
                Console.WriteLine(stringFormat);
            }
        }

        public string ChooseFuelType()
        {
            string userInput = "";
            string fuelType = "";
            Console.WriteLine("[1] - Octan95");
            Console.WriteLine("[2] - Octan96");
            Console.WriteLine("[3] - Octan98");
            Console.WriteLine("[4] - Soler");
            userInput = Console.ReadLine();

            switch(userInput)
            {
                case "1":
                    fuelType = "Octan95";
                    break;
                case "2":
                    fuelType = "Octan96";
                    break;
                case "3":
                    fuelType = "Octan98";
                    break;
                case "4":
                    fuelType = "Soler";
                    break;
                default:
                    throw new ValueOutOfRangeException("Fuel type", 4, 1);
            }

            return fuelType;
        }

        public string FuelToAdd()
        {
            string fuelToAdd;
            Console.Write("Enter how mach fuel you want to add in liters : ");
            fuelToAdd = Console.ReadLine();                                    
            return fuelToAdd;
        }

        public void RefuelVehicle(string i_LicenseNumber)
        {
            string stringFormat;
            try
            {
                VehicleInGarage.AddFuel(this.m_VehiclesInGarageArray, i_LicenseNumber, ChooseFuelType(), FuelToAdd());
            }
            catch (ValueOutOfRangeException re)
            {
                stringFormat = string.Format("Error: " + re.Message);
                Console.WriteLine(stringFormat);
            }
            catch (ArgumentException ae)
            {
                stringFormat = string.Format("Error: " + ae.Message);
                Console.WriteLine(stringFormat);
            }
            catch(FormatException fe)
            {
                stringFormat = string.Format("Error: " + fe.Message);
                Console.WriteLine(stringFormat);
            }
        }

        public string ChargeToAdd()
        {
            string chargeToAdd;
            Console.Write("Enter how mach charge you want to add in minutes : ");
            chargeToAdd = Console.ReadLine();                                
            return chargeToAdd;
        }

        public void ChargeVehicle(string i_LicenseNumber)
        {
            string stringFormat;
            try
            {
                VehicleInGarage.Charging(this.m_VehiclesInGarageArray, i_LicenseNumber, ChargeToAdd());
            }
            catch (ValueOutOfRangeException re)
            {
                stringFormat = string.Format("Error: " + re.Message);
                Console.WriteLine(stringFormat);
            }
            catch (ArgumentException ae)
            {
                stringFormat = string.Format("Error: " + ae.Message);
                Console.WriteLine(stringFormat);
            }
            catch (FormatException fe)
            {
                stringFormat = string.Format("Error: " + fe.Message);
                Console.WriteLine(stringFormat);
            }
        }

        public void VehicleDataByLicenseNumber(string i_LicenseNumber)
        {
            Dictionary<string, string> dataDict;
            string stringFormat;
            int i = 1;
            try
            {
                dataDict = VehicleInGarage.ShowVehicleData(this.m_VehiclesInGarageArray, i_LicenseNumber);

                foreach (var pair in dataDict)
                {
                    if(pair.Key.Contains("current air pressure"))
                    {
                        stringFormat = String.Format("wheel {0} air pressure : {1}", i++, pair.Value);
                        Console.WriteLine(stringFormat);
                    }
                    else if (pair.Key != "license number")
                    {
                        stringFormat = String.Format("{0} : {1}", pair.Key, pair.Value);
                        Console.WriteLine(stringFormat);
                    }
                }
            }
            catch (ArgumentException fe)
            {
                stringFormat = string.Format("Error: " + fe.Message);
                Console.WriteLine(stringFormat);
            }
        }
    }
}

