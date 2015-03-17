using ProgressBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOTP;

namespace TOTPGui
{
    public partial class Form1 : Form
    {


        private int digit = 6;
        private int t1 = 30;
        private String secret;
        private Totp totp = null;

        public Form1()
        {
            InitializeComponent();
            typeof(CircularProgressBar).InvokeMember("DoubleBuffered",
    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
    null, this.circularProgressBar, new object[] { true });
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.qrCodeImgControl, this.qrCodeImgControl.Text); 
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Totp.CheckBase32(this.textBoxSecret.Text))
            {
                secret = this.textBoxSecret.Text;
                t1 = (int)this.numericUpDown2.Value;
                digit = (int)this.numericUpDown1.Value;
                totp = new Totp(secret, t1, digit);
                this.qrCodeImgControl.Text = "otpauth://totp/" + this.textBoxLable.Text + ":" + this.textBoxUser.Text + "?secret=" + this.textBoxSecret.Text + "&issuer=" + this.textBoxLable.Text;
                toolTip1.SetToolTip(this.qrCodeImgControl, this.qrCodeImgControl.Text);
            }
            else
            {
                MessageBox.Show("The secret is not Base32 encoded", "TOTP GUI",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UInt64 unixTimestamp = (UInt64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            double tmp = ((unixTimestamp % (UInt64)t1) + 1) / (double)t1 * 360;
            this.circularProgressBar.ProgressAngle = (int)tmp;
            if (totp != null)
            {
                String code = totp.getCodeString();
                if (!this.textBoxCodeOutput.Text.Equals(code))
                    this.textBoxCodeOutput.Text = code;
            } 
                
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

     


        
        

        
    }
}
