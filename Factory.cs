using System;
using System.Collections.Generic;


namespace Vehicles
{
    public class Factory // מחלקה שיוצרת אובייקטים
    {
        private readonly string[] r_VehicleList = { "FuelCar", "ElectricCar", "FuelMotorcycle", "ElectricMotorcycle", "FuelTruck" };

        public string[] VehicleList
        {
            get { return this.r_VehicleList; }
        }

        public Vehicle CreateSpecificVehicle(string i_VehicleType)
        {
            Vehicle someVehicle = null;
            switch(i_VehicleType)
            {
                case "FuelCar":
                    someVehicle = new FuelCar();
                    break;
                case "ElectricCar":
                    someVehicle = new ElectricCar();
                    break;
                case "FuelMotorcycle":
                    someVehicle = new FuelMotorcycle();
                    break;
                case "ElectricMotorcycle":
                    someVehicle = new ElectricMotorcycle();
                    break;
                case "FuelTruck":
                    someVehicle = new FuelTruck();
                    break;
            }

            return someVehicle;
        }
    }
}
