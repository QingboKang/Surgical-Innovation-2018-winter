using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // store all the data
        private List<double> listData;
        static int iMeasureTimes = 0;

        // Matlab 
        MLApp.MLApp matlab = new MLApp.MLApp();

        public Form1()
        {
            InitializeComponent();
            listData = new List<double>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboBox1.Items.AddRange(ports);
            comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;

            matlab.Visible = 1;
            matlab.Execute("hold on;");
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            try
            {
                if(serialPort1.IsOpen == true)
                {
                    comboBox1.Enabled = true;

                    listData.Clear();
                    txtInfo.Text = "";

                    timer1.Enabled = false;
                    serialPort1.Close();
                    btnOpenPort.Text = "Open Port";
                    btnSend.Enabled = false;
                   

                }
                else if (serialPort1.IsOpen == false)
                {
                    comboBox1.Enabled = false;

                    btnOpenPort.Text = "Close Port";

                    serialPort1.BaudRate = 250000;
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Parity = 0;
                    serialPort1.DataBits = 8;
                //    serialPort1.ReceivedBytesThreshold = 10;
                    serialPort1.Encoding = Encoding.ASCII;
                    serialPort1.DataReceived += port_DataReceived;
                    serialPort1.Open();

                    btnSend.Enabled = true;
   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.RefreshInfoTextBox();
        }

        private void DisposeSerialPort()
        {
            if(serialPort1 != null)
            {
                if( serialPort1.IsOpen )
                {
                    serialPort1.Close();
                }
                serialPort1.Dispose();
            }

        }

        private string ReadSerialData()
        {
            string value = "";
            try
            {
                if (serialPort1 != null && serialPort1.BytesToRead > 0)
                {
                    // read data from serial port
                    value = serialPort1.ReadLine();

                    //if (!value.EndsWith("\n"))
                    //{
                    //    return null;
                    //}
                    value = value.Trim('\n');
                    // add to list
                    string[] tokens = value.Split(',');
                    if (tokens.Count()!=37)
                    {
                        return null;
                    }
                    else
                    {
                        foreach (string token in tokens)
                        {
                            if (token != "")
                            {
                                double number;
                                Double.TryParse(token, out number);
                                listData.Add(Convert.ToDouble(textBox1.Text)*number+Convert.ToDouble(textBox2.Text)+5);
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          //  MessageBox.Show(value + " " + value.Length, "fd", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return value;
        }

        private void Show3DModel()
        {
            Model3D obj_3d = new Model3D();
            if (listData.Count + 1!= obj_3d.iDataLen)
            {
                return;
            }

            double[] data = new double[obj_3d.iDataLen];
            for (int i = 0; i < obj_3d.iDataLen - 1; i++)
            {
                data[i] = listData[i];
            }
            data[obj_3d.iDataLen - 1] = data[0];

            double[] dX = obj_3d.GetX(data);
            double[] dY = obj_3d.GetY(data);
            double[] dZ = obj_3d.GetZ(iMeasureTimes);

            matlab.PutWorkspaceData("X", "base", dX);
            matlab.PutWorkspaceData("Y", "base", dY);
            matlab.PutWorkspaceData("Z", "base", dZ);

            String sret = matlab.Execute("plot3(X, Y, Z, 'LineWidth', 5);");
            matlab.Execute("xlabel('X'), ylabel('Y'), zlabel('Z');");
            matlab.Execute("grid on;");
            matlab.Execute("view(45, 45);");
            matlab.Execute("hold on;");

            iMeasureTimes++;
         //   MessageBox.Show(sret);

        }

        private void RefreshInfoTextBox()
        {
            string value = this.ReadSerialData();
            Action<string> setValueAction = text => this.txtInfo.Text += text;
            value += "\n";
            if (this.txtInfo.InvokeRequired)
            {
                this.txtInfo.Invoke(setValueAction, value);
            }
            else
            {
                setValueAction(value);
            }

            //
            if (value == null || value == "")
                return;

            List<int> listIndex = new List<int>();
            for(int i = 0; i < listData.Count; i++)
            {
                listIndex.Add(i);
            }

            chart1.Invoke((MethodInvoker)delegate { chart1.Series[0].Points.DataBindXY(listIndex, listData); });
            Show3DModel();
          //  this.chart1.Series[1].Points.DataBindXY(listIndex, listData);

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.listData.Clear();
            serialPort1.Write("a");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            DisposeSerialPort();
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            this.listData.Clear();
            serialPort1.Write("b");
            Thread.Sleep(300);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnEight_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Escape):
                    serialPort1.Close();
                    Close();
                    break;
            }
        }
    }
}
