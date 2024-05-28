using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckShort_YC4
{
    public partial class fMasterCheck : Form
    {
        public string BoardNo { get; private set; }
        public string ProductID { get; private set; }
        public string Password { get; private set; }
        public fMasterCheck()
        {
            InitializeComponent();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.BoardNo = txtBoardNo.Text.Trim();
            this.ProductID = txtProduct.Text.Trim();
            this.Password = txtPass.Text;
            this.Close();
        }
    }
}
