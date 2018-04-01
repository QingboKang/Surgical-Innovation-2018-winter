using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class SensorData
    {
        // the data obtained from the sensor
        public double[] dSensorData;
        // data length
        public int iDataLen = 37;

        // fixed, angle
        public double[] dAngle;

        // X Y Z 
        private double[] dX;
        private double[] dY;
        private double[] dZ;


        public SensorData()
        {
            dSensorData = new double[iDataLen];
            dAngle = new double[iDataLen];

            for (int i = 0; i < iDataLen; i++ )
            {
                dSensorData[i] = 20 + i * 2;
                dAngle[i] = i * 10;
            }

            
        }



        


    }
}
