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
        private List<int> listData;

        public Form1()
        {
            InitializeComponent();
            listData = new List<int>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboBox1.Items.AddRange(ports);
            comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;
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
                    btnEight.Enabled = false;
                    button1.Enabled = false;

                }
                else if (serialPort1.IsOpen == false)
                {
                    comboBox1.Enabled = false;

                    btnOpenPort.Text = "Close Port";

                    serialPort1.BaudRate = 9600;
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Parity = 0;
                    serialPort1.DataBits = 8;
                //    serialPort1.ReceivedBytesThreshold = 10;
                    serialPort1.Encoding = Encoding.ASCII;
                    serialPort1.DataReceived += port_DataReceived;
                    serialPort1.Open();

                    btnSend.Enabled = true;
                    btnEight.Enabled = true;
                    button1.Enabled = true;
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
                    value = serialPort1.ReadExisting();

                    if (!value.EndsWith("\0"))
                    {
                        return null;
                    }
                    value = value.Trim('\0');
                    // add to list
                    string[] tokens = value.Split('\n');
                    foreach (string token in tokens)
                    {
                        if (token != "")
                        {
                            int number;
                            Int32.TryParse(token, out number);
                            listData.Add(number);
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

        private void RefreshInfoTextBox()
        {
            string value = this.ReadSerialData();
            Action<string> setValueAction = text => this.txtInfo.Text += text;

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
            for(int i = 1; i <= listData.Count; i++)
            {
                listIndex.Add(i);
            } 

            this.chart1.Series[0].Points.DataBindXY( listIndex, listData );
          //  this.chart1.Series[1].Points.DataBindXY(listIndex, listData);

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.listData.Clear();
            serialPort1.Write("a");
            Thread.Sleep(100);
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
    }
}
