using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckShort_YC4
{
    public partial class FormTest : Form
    {
        SerialPort serialPort1;
        SerialPort serialPort2;
        public FormTest()
        {
            InitializeComponent();
            serialPort1 = new SerialPort();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler1);

            serialPort2 = new SerialPort();
            serialPort2.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler2);
        }

        private void DataReceivedHandler2(object sender, SerialDataReceivedEventArgs e)
        {
            string indata = serialPort2.ReadExisting();
            this.Invoke(new MethodInvoker(delegate {
                richTextBox1.AppendText("Port 1: " + indata);
            }));
        }

        private void DataReceivedHandler1(object sender, SerialDataReceivedEventArgs e)
        {
            string indata = serialPort1.ReadExisting();
            this.Invoke(new MethodInvoker(delegate {
                richTextBox1.AppendText("Port 2: " + indata);
            }));
        }

        private void btnCom1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (serialPort1.IsOpen)
                {
                    MessageBox.Show($"cổng {comboBox1.SelectedItem.ToString()} đang được sử dụng");
                    return;
                }
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.BaudRate = 19200;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Handshake = Handshake.None;
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    label1.Text = "Connected!";
                }
            }      
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            comboBox2.Items.AddRange(SerialPort.GetPortNames());
        }

        private void btnCom2_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex != -1)
            {
                if (serialPort2.IsOpen)
                {
                    MessageBox.Show($"cổng {comboBox1.SelectedItem.ToString()} đang được sử dụng");
                    return;
                }
                serialPort2.PortName = comboBox2.SelectedItem.ToString();
                serialPort2.BaudRate = 19200;
                serialPort2.Parity = Parity.None;
                serialPort2.DataBits = 8;
                serialPort2.StopBits = StopBits.One;
                serialPort2.Handshake = Handshake.None;
                if (!serialPort2.IsOpen)
                {
                    serialPort2.Open();
                    label2.Text = "Connected!";
                }
                
            }   
        }

        private void btnsend1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                }
                serialPort1.Write(textBox1.Text);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"IO Exception: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Access Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}");
            }
        }

        private void btnsend2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort2.IsOpen)
                {
                    serialPort2.Open();
                }
                serialPort2.Write(textBox2.Text);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"IO Exception: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Access Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}");
            }
        }
    }
}
