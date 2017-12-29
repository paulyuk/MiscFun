using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{


    public partial class Form1 : Form
    {

        bool _mouseEnabled = true;
        bool firstrun = true;
        const int MOUSE_MOVE_INTERVAL = 10000; //10 seconds by default

        [DllImportAttribute("User32.dll")]

        private static extern int FindWindow(String ClassName, String
WindowName);

        [DllImportAttribute("User32.dll")]
        private static extern int SetForegroundWindow(int hWnd);



        public Form1()
        {
            InitializeComponent();

            this.timer1.Interval = MOUSE_MOVE_INTERVAL;
            this.timer1.Enabled = true; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.toggleMouseEnabled();
        }

        private void toggleMouseEnabled()
        {
            if (this._mouseEnabled) {
                this._mouseEnabled = false;
                this.label1.Text = "Mouse bot DISABLED";
                this.button2.Text = "Enable";

            } else {
                this._mouseEnabled = true;
                this.label1.Text = "Mouse bot ENABLED";
                this.button2.Text = "Disable";
            }

        }

        private void moveMouse()
        {
            // Set the Current cursor, move the cursor's Position,

            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
            Cursor.Position = new Point(Cursor.Position.X + 50, Cursor.Position.Y + 50);
        }

        private void doSendKeys()
        {
            //to activate an application
            int hWnd = FindWindow(null, "Minecraft");
            if (hWnd > 0)
            {
                SetForegroundWindow(hWnd);
                System.Threading.Thread.Sleep(1000);

                if (this.firstrun == true)
                {
                    SendKeys.Send("{Esc}");       //good to pause or unpause
                    this.firstrun = false;
                }
                
                SendKeys.Send("w");             //move forward
                System.Threading.Thread.Sleep(1000);
                SendKeys.Send("d");             //move right
                System.Threading.Thread.Sleep(1000);
               // SendKeys.Send("s");             //move back
                System.Threading.Thread.Sleep(1000);
               // SendKeys.Send("a");             //move left
                System.Threading.Thread.Sleep(1000);


            }
        }

        private void doheartbeateffect()
        {
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Refresh();

            System.Threading.Thread.Sleep(2000);

            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this._mouseEnabled)
            {
                //this.moveMouse();
                this.doSendKeys();

                this.doheartbeateffect();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
