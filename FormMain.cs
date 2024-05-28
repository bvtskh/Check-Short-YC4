using CheckShort_YC4.Business;
using CheckShort_YC4.DAL;
using CheckShort_YC4.Entities;
using CheckShort_YC4.Generic;
using CheckShort_YC4.Sigleton;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CheckShort_YC4
{
    
    public partial class FormMain : Form
    {
        private int qty = 0;
        private int state = 1; // Ready
        private bool IsFirtTimeReceive = true;
        private System.Windows.Forms.Timer Timer;
        BackgroundWorker BackgroundWorker;
        #region Method
        private void BinDataToControls()
        {
            lblVersion.Text = Ultils.GetRunningVersion();
        }
        public FormMain()
        {
            InitializeComponent();
            BinDataToControls();
            Settings.Read();
            CommPort com = CommPort.Instance;
            com.StatusChanged += OnStatusChanged;
            com.DataReceived += OnDataReceived;
            cboCOMPort.Items.AddRange(SerialPort.GetPortNames());
            com.Open();           
            lblVersion.Text = Ultils.GetRunningVersion();
            Timer = new System.Windows.Forms.Timer();
            Timer.Tick += Timer_Tick;

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            toolState.Text = "State: " + GetApplicationState(state);
        }

        private string GetApplicationState(int state)
        {
            switch(state)
            {
                case 0:
                    return "Waiting";
                case 1:
                    return "Ready";
                case 2:
                    return "Stop";
                case 3:
                    return "Finish";
                default:
                    return @"N\A";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(!BackgroundWorker.IsBusy)
            {
                BackgroundWorker.RunWorkerAsync();
            }
        }

        // delegate used for Invoke
        internal delegate void StringDelegate(string data);
        public void OnStatusChanged(string status)
        {
            //Handle multi-threading
            if (InvokeRequired)
            {
                Invoke(new StringDelegate(OnStatusChanged), new object[] { status });
                return;
            }
            lblPortName.Text = status;
        }

        // shutdown the worker thread when the form closes
        protected override void OnClosed(EventArgs e)
        {
            CommPort com = CommPort.Instance;
            com.Close();

            base.OnClosed(e);
        }

        private Label tempLabel = new Label();
        public void OnDataReceived(string dataIn)
        {          
            if (IsFirtTimeReceive)
            {
                return;
            }
            //Handle multi-threading
            if (InvokeRequired)
            {
                Invoke(new StringDelegate(OnDataReceived), new object[] { dataIn });
                return;
            }
            else
            {
                tempLabel.Text += dataIn;
                if (tempLabel.Text == Settings.Option.SignalOK)
                {                    
                    IsFirtTimeReceive = true;
                    panelBarcode.Enabled = true;
                    ShowMessage("PASS", "OK", $"Barcode {txtBarcode.Text} check OK ");
                    lblRecei.Text = Settings.Option.SignalOK;
                    tempLabel.Text = "";
                    state = Settings.State.Finish;
                }
                else if (tempLabel.Text == Settings.Option.SignalNG)
                {
                    IsFirtTimeReceive = true;
                    panelBarcode.Enabled = true;
                    ShowMessage("FAIL", "NG", $"Barcode {txtBarcode.Text} check NG");
                    lblRecei.Text = Settings.Option.SignalNG;
                    tempLabel.Text = "";
                    state = Settings.State.Finish;
                }
                else if (tempLabel.Text == Settings.Option.SignalRST)
                {
                    tempLabel.Text = "";
                }
                else if (tempLabel.Text == Settings.Option.SignalReady)
                {
                    tempLabel.Text = "";
                }
                else if (tempLabel.Text == Settings.Option.SignalERROR)
                {
                    IsFirtTimeReceive = true;
                    panelBarcode.Enabled = true;
                    ShowMessage("FAIL", "NG", $"Kiểm tra khí, sensor xilanh!");
                    lblRecei.Text = Settings.Option.SignalERROR;
                    tempLabel.Text = "";
                    state = Settings.State.Finish;
                }
                if(state == Settings.State.Finish)
                {
                    txtBarcode.ResetText();
                    txtBarcode.Focus();
                    state= Settings.State.Ready;
                }
                else if (tempLabel.Text == Settings.Option.SignalCHECK)
                {
                    tempLabel.Text = "";
                }
                if (tempLabel.Text.Length >= 10)
                {
                    tempLabel.Text = "";
                }

            }
        }

        #endregion


        // bool @start = false;
        private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (IsFirtTimeReceive)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    state = GetCOMportState();
                    // kiểm tra trạng thái
                    if (state == Settings.State.Stop)
                    {
                        ShowMessage("FAIL", "NG", "Chưa có kết nối đến cổng COM!");
                        return;
                    }
                    if (state == Settings.State.Ready)
                    {
                        ShowMessage("WAIT", "WAIT", $"Đang kiểm tra: {txtBarcode.Text}");
                        // cho bắn barcode
                        CommPort com = CommPort.Instance;
                        // check logic

                        // gửi tín hiệu
                        com.Send(Settings.Option.SignalBAR);
                        toolSendSignal.Text = "Send: " + Settings.Option.SignalBAR;
                        panelBarcode.Enabled = false;
                        IsFirtTimeReceive = false;
                        qty += 1;
                        lblQty.Text = qty.ToString();
                    }
                }
            }
        }

        private int GetCOMportState()
        {
            CommPort commPort = CommPort.Instance;
            if (commPort.IsOpen) return Settings.State.Ready;
            return Settings.State.Stop;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var boardNo = e.Argument.ToString();
            bool flag = false;
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(Settings.Option.SleepTime);
                flag = SingletonHelper.PVSInstance.GetWorkOrderItemByBoardNo(boardNo) != null;
                if (flag) break;
            }
            e.Result = flag;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
 
        }
        private void lblConfigs_Click(object sender, EventArgs e)
        {
            new FormConfig().ShowDialog();
            CommPort commPort = CommPort.Instance;
            if (commPort.IsOpen)
            {
                ShowMessage("WAIT", "WAIT", "Chương trình đã sẵn sàng đọc barcode!");
                state = 1;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Timer.Start();
            CommPort com = CommPort.Instance;
            if (com.IsOpen)
            {
                ShowMessage("WAIT", "WAIT", "Chương trình đã sẵn sàng đọc barcode!");
            }
            else
            {
                ShowMessage("FAIL", "NG", "Chưa có kết nối đến cổng COM!");
            }
            lblRecei.Text = "..........";
        }
        
        private void SoftInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {

            }
            else if (e.Error != null)
            {

            }
            else
            {
                var softInfo = e.Result as PVSWebServiceReference.Base_Soft_InfoEntity;
                SingletonHelper.PVSInstance.RegisterSoftInfo(softInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str_status"></param>
        /// <param name="str_message"></param>
        private void ShowMessage(string key, string str_status, string str_message)
        {
            switch (key)
            {
                case "PASS":
                    this.lblStatus.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblStatus.Text = str_status;
                        this.lblStatus.BackColor = Color.FromArgb(0, 128, 0);
                        this.lblStatus.Update();
                    });
                    this.lblMessage.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblMessage.Text = str_message;
                        this.lblMessage.BackColor = Color.FromArgb(0, 128, 0);
                        this.lblMessage.Update();
                    });
                    break;
                case "FAIL":
                    this.lblStatus.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblStatus.Text = str_status;
                        this.lblStatus.BackColor = Color.DarkRed;
                        this.lblStatus.Update();
                    });
                    this.lblMessage.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblMessage.Text = str_message;
                        this.lblMessage.BackColor = Color.DarkRed;
                        this.lblMessage.Update();
                    });
                    break;
                case "WAIT":
                    this.lblStatus.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblStatus.Text = str_status;
                        this.lblStatus.BackColor = Color.FromArgb(255, 128, 0);
                        this.lblStatus.Update();
                    });
                    this.lblMessage.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblMessage.Text = str_message;
                        this.lblMessage.BackColor = Color.FromArgb(255, 128, 0);
                        this.lblMessage.Update();
                    });

                    break;
                case "Empty":
                    this.lblStatus.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblStatus.Text = @"[N\A]";
                        this.lblStatus.BackColor = Color.FromArgb(255, 128, 0);
                        this.lblStatus.Update();
                    });
                    this.lblMessage.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblMessage.Text = "no results";
                        this.lblMessage.BackColor = Color.FromArgb(255, 128, 0);
                        this.lblMessage.Update();
                    });
                    break;
                case "Default":
                    this.lblStatus.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblStatus.Text = str_status;
                        this.lblStatus.BackColor = Color.FromArgb(255, 128, 0);
                        this.lblStatus.Update();
                    });
                    this.lblMessage.Invoke((MethodInvoker)delegate ()
                    {
                        this.lblMessage.Text = str_message;
                        this.lblMessage.BackColor = Color.FromArgb(255, 128, 0);
                        this.lblMessage.Update();
                    });
                    break;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBarcode.ResetText();
            lblRule.Text = @"N\A";
            lblRecei.Text ="..........";
            lblModel.Text = @"N\A";
            lblQty.Text = "0";
            state = GetCOMportState();

            CommPort commPort = CommPort.Instance;
            commPort.Send(Settings.Option.SignalRST);
            toolSendSignal.Text = "Send: " + Settings.Option.SignalRST;
            IsFirtTimeReceive = true;
            state = Settings.State.Ready;
            panelBarcode.Enabled = true;
            txtBarcode.Focus();
            txtBarcode.Clear();

            if (commPort.IsOpen)
            {
                ShowMessage("WAIT", "WAIT", "Chương trình đã sẵn sàng đọc barcode!");
                
            }
            else
            {
                ShowMessage("FAIL", "NG", "Chưa có kết nối đến cổng COM!");
            }
        }

        private void comReceiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblRecei.Text = Settings.Option.SignalReady;
        }

        SerialPort serialPort1 = new SerialPort();

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {             
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = cboCOMPort.SelectedItem.ToString();
                serialPort1.BaudRate = 9600;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Handshake = Handshake.None;
                serialPort1.Open();
            }
            if (rdoOK.Checked)
            {
                serialPort1.Write(Settings.Option.SignalOK);
            }
            if (rdoNG.Checked)
            {
                serialPort1.Write(Settings.Option.SignalNG);
            }
            if (rdoRST.Checked)
            {
                serialPort1.Write(Settings.Option.SignalRST);
            }
            if (rdoREADY.Checked)
            {
                serialPort1.Write(Settings.Option.SignalReady);
            }
            if (rdoERROR.Checked)
            {
                serialPort1.Write(Settings.Option.SignalERROR);
            }
            if (rdoCHECKING.Checked)
            {
                serialPort1.Write(Settings.Option.SignalCHECK);
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
