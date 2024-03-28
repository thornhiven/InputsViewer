using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputsViewer
{
    public partial class Form1 : Form
    {
        List<ButtonInterface> buttons;       
        globalKeyboardHook gkh = new globalKeyboardHook();
        public Form1()
        {
            TopMost = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttons = new List<ButtonInterface>();
            buttons.Add(new ButtonInterface("fuf", "up1", "down1", Keys.B, pictureBox1));
            gkh.HookedKeys.Add(Keys.B);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            gkh.KeyDown += new KeyEventHandler(gkh_Keydown);
            //this.TransparencyKey = BackColor;
            //this.FormBorderStyle = FormBorderStyle.None;
        }

        private void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (ButtonInterface b in buttons)
            {
                if (e.KeyCode == b.key)
                {
                    this.pictureBox1.Image = b.upStateImage;
                    break;
                }
            }
        }

        private void gkh_Keydown(object sender, KeyEventArgs e)
        {
            foreach (ButtonInterface b in buttons)
            {
                if (e.KeyCode == b.key)
                {
                    this.pictureBox1.Image = b.downStateImage;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
                this.FormBorderStyle = FormBorderStyle.Sizable;
            else
                this.FormBorderStyle = FormBorderStyle.None;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.TransparencyKey = BackColor;
                this.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TransparencyKey = Color.Empty;
            }
        }
    }
}
