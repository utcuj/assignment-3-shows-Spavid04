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
    public partial class AdministratorForm : Form
    {
        private User user;

        public AdministratorForm(User user)
        {
            this.user = user;

            InitializeComponent();
        }
    }
}
