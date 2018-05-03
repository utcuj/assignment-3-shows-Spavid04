using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shows.Core.Models;

namespace Shows.Client.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = new ApiConnector<Tuple<Guid, UserLevel>>().ForController("login")
                .AddParameter("username", textBox1.Text).AddParameter("password", textBox2.Text).Get();

            if (data.Item1 == Guid.Empty)
            {
                MessageBox.Show("Invalid credentials!");
                return;
            }

            var user = new ApiConnector<User>().ForController("user")
                .AddParameter("guid", data.Item1).Get();
            Form f = null;
            switch (data.Item2)
            {
                case UserLevel.Regular:
                case UserLevel.Premium:
                    f = new UserForm(user);
                    break;
                case UserLevel.Administrator:
                    f = new AdministratorForm(user);
                    break;
            }
            this.Hide();
            f.ShowDialog();

            Environment.Exit(0);
        }
    }
}
