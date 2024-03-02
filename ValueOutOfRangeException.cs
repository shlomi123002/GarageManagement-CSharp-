using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get { return this.m_MaxValue; }
            set { this.m_MaxValue = value; }
        }

        public float MinValue
        {
            get { return this.m_MinValue; }
            set { this.m_MinValue = value; }
        }

        public ValueOutOfRangeException(string filed, float i_MaxValue, float i_MinValue) : base("Error: " + filed + ": Out of range, need to be between " +i_MinValue +" - " + i_MaxValue)
        {
            this.MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
        }
    }
}
