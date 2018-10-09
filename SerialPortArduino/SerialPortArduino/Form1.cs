using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SerialPortArduino
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();

            foreach (var item in ports)
            {
                Console.WriteLine(item);
            }
            listBox1.Items.AddRange(ports);
            comboBox1.Items.AddRange(ports);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                return;
            }
            else
            {
                serialPort1.BaudRate = Int32.Parse(comboBox2.Text);
                serialPort1.PortName = comboBox1.Text;

                serialPort1.Open();
                button2.Enabled = false;
                button3.Enabled = true;

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                return;
            else
            {
                serialPort1.Close();
                button2.Enabled = true;
                button3.Enabled = false;
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //byte[] buffer = new byte[sp.BytesToRead];
            //Console.WriteLine("data received {0} bytes", sp.BytesToRead);
            //sp.Read(buffer, 0, sp.BytesToRead);
            //string v = System.Text.Encoding.ASCII.GetString(buffer);
            //Console.WriteLine(v);
            //textBox1.Text += v;
            string s = sp.ReadExisting();
            Console.WriteLine(s);
            textBox1.Invoke(myDelegate, s);

        }

        public delegate void AddDataDelegate(String myString); // declare delegate
        
        public AddDataDelegate myDelegate; // define delegate

        private void Form1_Load(object sender, EventArgs e)
        {
            this.myDelegate = new AddDataDelegate(AddDataMethod);

        }

        private void AddDataMethod(string myString)
        {
            textBox1.AppendText(myString);
        }
    }
}
