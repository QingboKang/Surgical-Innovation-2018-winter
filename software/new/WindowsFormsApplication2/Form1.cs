using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        MLApp.MLApp matlab = new MLApp.MLApp();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            matlab.Visible = 0;
            String cmd_str2 = "theta=linspace(0,360,36);" + "XX = 1 * cos(theta * pi / 180);" + "YY = 1 * sin(theta * pi / 180);" + "ZZ = linspace(0, 10, 36);" + "X =[]; Y =[]; Z =[];" + "hold on;" + " grid on;" + "for i = 1:10" + "Z(i,:) = i * ones(length(ZZ), 1);" + "Y(i,:) = i * YY';" + "X(i,:) = XX'/i;" + "plot3(X(i,:), Y(i,:), Z(i,:));" + " end;" + " view(45, 30);";
            matlab.Execute(cmd_str2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String cmd_str = "theta=linspace(0,360,36);" + "XX = 10 * cos(theta * pi / 180);" + "YY = 10 * sin(theta * pi / 180);" + "ZZ = linspace(0, 10, 36);" + "X =[]; Y =[]; Z =[];" + "hold on;" + " grid on;" + "for i = 1:10" + "Z(i,:) = i * ones(length(ZZ), 1);" + "Y(i,:) = i * YY';" + "X(i,:) = XX'/i;" + "plot3(X(i,:), Y(i,:), Z(i,:));" + " end;" + " view(45, 30);";
            matlab.Execute(cmd_str);

        }
    }
}
