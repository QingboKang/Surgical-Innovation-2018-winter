using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Model3D
    {
        // data length
        public int iDataLen = 37;

        // fixed, angle
        private double[] dAngle;
        private double[] dCosine;
        private double[] dSine;
         
        // X Y Z 
        private double[] dX;
        private double[] dY;
        private double[] dZ;
        
        public Model3D()
        {
            dAngle = new double[iDataLen];
            dCosine = new double[iDataLen];
            dSine = new double[iDataLen];

            dX = new double[iDataLen];
            dY = new double[iDataLen];
            dZ = new double[iDataLen];

            for (int i = 0; i < iDataLen; i++)
            {
                dAngle[i] = i * 10;
                dCosine[i] = Math.Cos(dAngle[i] * Math.PI / 180);
                dSine[i] = Math.Sin(dAngle[i] * Math.PI / 180);
            }
        }

        public double[] GetX(double[] dSensorData)
        {
            for (int i = 0; i < iDataLen; i++)
            {
                dX[i] = dSensorData[i] * dCosine[i];
            }
            return dX;
        }

        public double[] GetY(double[] dSensorData)
        {
            for (int i = 0; i < iDataLen; i++)
            {
                dY[i] = dSensorData[i] * dSine[i];
            }
            return dY;
        }

        public double[] GetZ(int ii)
        {
            for (int i = 0; i < iDataLen; i++)
            {
                dZ[i] = ii;
            }
            return dZ;
        }

        public double[] GetCosine()
        {
            return dCosine;
        }

        public double[] GetSine()
        {
            return dSine;
        }

    }
}
