using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shows.Core;
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

            InitTab1();
        }

        #region Tab1

        private void InitTab1()
        {
            this.dateTimePicker1.MinDate = new DateTime(1800, 1, 1);
            this.dateTimePicker1.MaxDate = new DateTime(2100, 1, 1);
            this.dateTimePicker1.Value = this.dateTimePicker1.MinDate + TimeSpan.FromSeconds(1);
            this.dateTimePicker2.MinDate = new DateTime(1800, 1, 1);
            this.dateTimePicker2.MaxDate = new DateTime(2100, 1, 1);
            this.dateTimePicker2.Value = DateTime.Now;

            RefreshTab1List();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshTab1List();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker1.Value = this.dateTimePicker1.MinDate + TimeSpan.FromSeconds(1);
            dateTimePicker2.Value = DateTime.Now;
            numericUpDown1.Value = 0;
            checkBox1.CheckState = CheckState.Indeterminate;

            listBox1.SelectedIndex = -1;
            RefreshTab1List();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var show = listBox1.SelectedIndex != -1 ? (Show)listBox1.SelectedItem : new Show();
            
            show.Title = textBox1.Text;
            show.Actors = textBox2.Text;
            show.Description = textBox3.Text;
            show.Genre = textBox4.Text;
            show.ImdbId = textBox5.Text;
            show.ReleaseDate = dateTimePicker1.Value;
            show.ImdbRating = (int) numericUpDown1.Value;
            show.Available = checkBox1.Checked;

            new ApiConnector().ForController("show").Post(show);

            linkLabel2_LinkClicked(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var show = (Show) listBox1.SelectedItem;

                new ApiConnector().ForController("show").AddParameter("showId", show.Id).Delete();

                linkLabel2_LinkClicked(null, null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
            linkLabel2_LinkClicked(null, null);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var show = (Show) listBox1.SelectedItem;

                textBox1.Text = show.Title;
                textBox2.Text = show.Actors;
                textBox3.Text = show.Description;
                textBox4.Text = show.Genre;
                textBox5.Text = show.ImdbId;
                dateTimePicker1.Value = show.ReleaseDate;
                dateTimePicker2.Value = DateTime.Now;
                numericUpDown1.Value = show.ImdbRating;
                checkBox1.CheckState = show.Available ? CheckState.Checked : CheckState.Unchecked;
            }
        }

        private void RefreshTab1List()
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
                return;

            listBox1.Items.Clear();

            var apiConnector = new ApiConnector<List<Show>>().ForController("show")
                .AddParameter("title", textBox1.Text.FlattenString())
                .AddParameter("actors", textBox2.Text.FlattenString())
                .AddParameter("description", textBox3.Text.FlattenString())
                .AddParameter("genre", textBox4.Text.FlattenString())
                .AddParameter("imdbId", textBox5.Text.FlattenString())
                .AddParameter("date1", dateTimePicker1.Value)
                .AddParameter("date2", dateTimePicker2.Value)
                .AddParameter("imdbRating", (int)numericUpDown1.Value)
                .AddParameter("available",
                    checkBox1.CheckState == CheckState.Checked
                        ? (bool?)true
                        : (checkBox1.CheckState == CheckState.Unchecked ? (bool?)false : null));

            var shows = apiConnector.Get();
            foreach (var show in shows)
            {
                listBox1.Items.Add(show);
            }
        }

        #endregion

        #region Tab2
        
        private void button4_Click(object sender, EventArgs e)
        {
            var user = listBox2.SelectedIndex != -1 ? (User) listBox2.SelectedItem : new User();

            user.Username = textBox7.Text;
            user.Password = textBox8.Text;
            user.UserLevel = radioButton1.Checked ? UserLevel.Regular : UserLevel.Premium;

            new ApiConnector().ForController("user").Post(user);

            listBox2.SelectedIndex = -1;
            RefreshTab2List();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                var user = (User) listBox2.SelectedItem;

                new ApiConnector().ForController("user").AddParameter("userId", user.Id).Delete();

                listBox2.SelectedIndex = -1;
                RefreshTab2List();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = -1;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            RefreshTab2List();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                var user = (User) listBox2.SelectedItem;

                textBox7.Text = user.Username;
                textBox8.Text = user.Password;
                radioButton1.Checked = user.UserLevel == UserLevel.Regular;
                radioButton2.Checked = user.UserLevel == UserLevel.Premium;
            }
            else
            {
                textBox7.Clear();
                textBox8.Clear();
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
        }

        private void RefreshTab2List()
        {
            listBox2.Items.Clear();

            var regulars = new ApiConnector<List<User>>().ForController("user")
                .AddParameter("username", textBox6.Text.FlattenString())
                .AddParameter("level", UserLevel.Regular)
                .Get();
            var premiums = new ApiConnector<List<User>>().ForController("user")
                .AddParameter("username", textBox6.Text.FlattenString())
                .AddParameter("level", UserLevel.Premium)
                .Get();

            foreach (var user in regulars.Concat(premiums))
            {
                listBox2.Items.Add(user);
            }
        }

        #endregion
    }
}
