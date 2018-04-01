using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SensorData obj_sensordata = new SensorData();
            int data_len = 37;
            double[] data = new double[data_len];
            for(int i = 0; i < data_len; i++)
            {
                data[i] = i + 30;
            }
            data[data_len - 1] = data[0];

            double[] dX = obj_sensordata.GetX(data);
            double[] dY = obj_sensordata.GetY(data);
            double[] dZ = obj_sensordata.GetZ(2);

            MLApp.MLApp matlab = new MLApp.MLApp();
            matlab.Visible = 0;
            matlab.PutWorkspaceData("X", "base", dX);
            matlab.PutWorkspaceData("Y", "base", dY);
            matlab.PutWorkspaceData("Z", "base", dZ);

            String sret = matlab.Execute("plot3(X, Y, Z, 'LineWidth', 5);");
            matlab.Execute("grid on;");

            Console.WriteLine(sret);
            /* double[] dCosine = obj_sensordata.GetCosine();
            double[] dSine = obj_sensordata.GetSine();
            
            for(int i = 0; i < dCosine.Length; i++)
            {
                Console.WriteLine("{0}:{1}  {2}", i*10, dCosine[i], dSine[i]);
            } */
        }
    }
}
