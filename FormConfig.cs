using CheckShort_YC4.Business;
using CheckShort_YC4.Entities;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace CheckShort_YC4
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            //SystemSetting.Read();
            BinDataToControls();
        }

        private string[] GetTaskWindows()
        {
            List<string> task = new List<string>();
            // Get the desktopwindow handle
            int nDeshWndHandle = NativeWin32.GetDesktopWindow();
            // Get the first child window
            int nChildHandle = NativeWin32.GetWindow(nDeshWndHandle, NativeWin32.GW_CHILD);
            while (nChildHandle != 0)
            {
                //If the child window is this (SendKeys) application then ignore it.
                if (nChildHandle == this.Handle.ToInt32())
                {
                    nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
                }

                // Get only visible windows
                if (NativeWin32.IsWindowVisible(nChildHandle) != 0)
                {
                    StringBuilder sbTitle = new StringBuilder(1024);
                    // Read the Title bar text on the windows to put in combobox
                    NativeWin32.GetWindowText(nChildHandle, sbTitle, sbTitle.Capacity);
                    string sWinTitle = sbTitle.ToString();
                    {
                        if (sWinTitle.Length > 0)
                        {
                            task.Add(sWinTitle);
                        }
                    }
                }
                // Look for the next child.
                nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
            }
            return task.ToArray();
        }

        private void BinDataToControls()
        {
            CommPort com = CommPort.Instance;
            int found = 0;
            string[] portList = com.GetAvailablePorts();
            for (int i = 0; i < portList.Length; ++i)
            {
                string name = portList[i];
                cboCOMPort.Items.Add(name);
                if (name == Settings.Port.PortName)
                    found = i;
            }
            if (portList.Length > 0) cboCOMPort.SelectedIndex = found;
            txtRun.Text = Settings.Option.SignalBAR;
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            if (cboCOMPort.SelectedIndex == -1)
            {
                MessageBox.Show("Cổng COM không hợp lệ !", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRun.Focus();
                return;
            }
            Settings.Port.PortName = cboCOMPort.Text;
            CommPort com = CommPort.Instance;
            com.Open();
            Settings.Write();
            MessageBox.Show("Save success!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }


        private void btnTestSend_Click(object sender, EventArgs e)
        {
            Settings.Port.PortName = cboCOMPort.Text;
            CommPort com = CommPort.Instance;
            com.Send(txtRun.Text);
            MessageBox.Show("Gửi dữ liệu đi thành công.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {

        }
    }
}
