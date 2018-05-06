using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shows.Core.Models;

namespace Shows.Client.Forms
{
    public partial class NotificationForm : Form
    {
        public NotificationForm(string text, int notificationCount)
        {
            InitializeComponent();

            label1.Text = text;

            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width - 10,
                workingArea.Bottom - (Size.Height - 10) * notificationCount);

            Timer t = new Timer();
            t.Tick += (sender, args) =>
            {
                this.Opacity -= 0.01;

                if (this.Opacity < 0.01)
                    this.Close();
            };
            t.Interval = 5000/100; //100 opacity levels over 5000 ms
            t.Start();
        }
    }
}
