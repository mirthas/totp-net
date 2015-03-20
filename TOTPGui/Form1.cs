using System;
using System.Reflection;
using System.Windows.Forms;
using ProgressBar;
using TOTP;

namespace TOTPGui
{
    public partial class Form1 : Form
    {
        private int digit = 6;
        private String secret;
        private int t1 = 30;
        private Totp totp;

        public Form1()
        {
            InitializeComponent();
            typeof (CircularProgressBar).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, circularProgressBar, new object[] {true});
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(qrCodeImgControl, qrCodeImgControl.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Totp.CheckBase32(textBoxSecret.Text))
            {
                secret = textBoxSecret.Text;
                t1 = (int) numericUpDown2.Value;
                digit = (int) numericUpDown1.Value;
                totp = new Totp(secret, t1, digit);
                qrCodeImgControl.Text = "otpauth://totp/" + textBoxLable.Text + ":" + textBoxUser.Text + "?secret=" +
                                        textBoxSecret.Text + "&issuer=" + textBoxLable.Text;
                toolTip1.SetToolTip(qrCodeImgControl, qrCodeImgControl.Text);
            }
            else
            {
                MessageBox.Show("The secret is not Base32 encoded", "TOTP GUI", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var unixTimestamp = (UInt64) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var tmp = ((unixTimestamp%(UInt64) t1) + 1)/(double) t1*360;
            circularProgressBar.ProgressAngle = (int) tmp;
            if (totp != null)
            {
                var code = totp.getCodeString();
                if (!textBoxCodeOutput.Text.Equals(code))
                    textBoxCodeOutput.Text = code;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
        }
    }
}