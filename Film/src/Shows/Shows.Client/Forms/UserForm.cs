﻿using System;
using System.Collections.Concurrent;
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
    public partial class UserForm : Form
    {
        private User user;
        private static ConcurrentBag<Notification> pendingNotifications = new ConcurrentBag<Notification>();

        public static void TryShowNotification(User currentUser, Notification notification)
        {
            if (currentUser.Id == notification.ForUser.Id)
            {
                pendingNotifications.Add(notification);
                //new ApiConnector().ForController("notification").AddParameter("notificationId", notification.Id)
                //    .Delete();
            }
        }

        public UserForm(User user)
        {
            this.user = user;
            NotificationReceiever.InitilizeHub(user);

            InitializeComponent();

            InitTab1();

            Timer notificationTimer = new Timer();
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Interval = 5000;
            notificationTimer.Start();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            lock (pendingNotifications)
            {
                if (pendingNotifications.Count > 0)
                {
                    int notificationCount = 1;
                    foreach (var pendingNotification in pendingNotifications)
                    {
                        NotificationForm f = new NotificationForm(pendingNotification.ToString(), notificationCount);
                        f.Show();
                        new ApiConnector().ForController("notification").AddParameter("notificationId", pendingNotification.Id)
                            .Delete();
                        notificationCount++;
                    }
                    Notification n;
                    while (!pendingNotifications.IsEmpty)
                    {
                        pendingNotifications.TryTake(out n);
                    }
                }
            }
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var show = ((Show)listBox1.SelectedItem);

                if (show.Available)
                {
                    MessageBox.Show("Unavailable!");
                    return;
                }

                Tuple<int, int> data = new Tuple<int, int>(this.user.Id, show.Id);
                new ApiConnector().ForController("userHistory").Post(data);

                MessageBox.Show("Ta-da!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var show = ((Show)listBox1.SelectedItem);

                MessageBox.Show(show.ToLongString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var show = ((Show)listBox1.SelectedItem);

                var reviews = new ApiConnector<List<UserReview>>().ForController("userReview")
                    .AddParameter("showId", show.Id)
                    .Get();

                ReviewForm f = new ReviewForm(reviews);
                f.ShowDialog();
            }
        }

        private void searchFields_Changed(object sender, EventArgs e)
        {
            RefreshTab1List();
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
                .AddParameter("imdbRating", (int) numericUpDown1.Value)
                .AddParameter("available",
                    checkBox1.CheckState == CheckState.Checked
                        ? (bool?) true
                        : (checkBox1.CheckState == CheckState.Unchecked ? (bool?) false : null));

            List<Show> shows = new List<Show>();

            if (checkBox2.Checked)
            {
                shows.AddRange(apiConnector.AddParameter("showType", ShowType.Movie).Get());
            }
            if (checkBox3.Checked)
            {
                shows.AddRange(apiConnector.AddParameter("showType", ShowType.Theatre).Get());
            }
            if (checkBox4.Checked)
            {
                shows.AddRange(apiConnector.AddParameter("showType", ShowType.Sport).Get());
            }
            foreach (var show in shows)
            {
                listBox1.Items.Add(show);
            }
        }

        #endregion

        #region Tab2

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                var showHist = ((UserShowHistory)listBox2.SelectedItem);

                Tuple<int, int> data = new Tuple<int, int>(this.user.Id, showHist.Id);
                var review = new ApiConnector<UserReview>().ForController("userReview")
                    .AddParameter("userId", this.user.Id)
                    .AddParameter("historyId", showHist.Id)
                    .Get();

                if (review == null) review = new UserReview();

                numericUpDown2.Value = review.Rating;
                textBox6.Text = review.Review;
            }
            else
            {
                numericUpDown2.Value = 0;
                textBox6.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                var showHist = ((UserShowHistory)listBox2.SelectedItem);

                Tuple<int, string> data = new Tuple<int, string>((int) numericUpDown2.Value, textBox6.Text);
                new ApiConnector().ForController("userReview")
                    .AddParameter("userId", this.user.Id)
                    .AddParameter("showId", showHist.Show.Id)
                    .Post(data);

                MessageBox.Show("Ta-da!");
            }
        }

        private void RefreshTab2List()
        {
            listBox2.Items.Clear();

            var history = new ApiConnector<List<UserShowHistory>>().ForController("userHistory")
                .AddParameter("userId", this.user.Id).Get();

            foreach (var hist in history.OrderByDescending(x => x.DateTime))
            {
                listBox2.Items.Add(hist);
            }
        }

        #endregion

        #region Tab3

        private void button4_Click(object sender, EventArgs e)
        {
            var show = ((Show) listBox1.SelectedItem);

            var data = new Tuple<int, int>(this.user.Id, show.Id);
            new ApiConnector().ForController("interest").Post(data);

            MessageBox.Show("Ta-da!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex == -1)
                return;

            var group = (UserGroup) listBox3.SelectedItem;

            new ApiConnector().ForController("recommendation")
                .AddParameter("userId", this.user.Id)
                .AddParameter("showId", ((Show) listBox1.SelectedItem).Id)
                .AddParameter("toGroup", true)
                .Post(((UserGroup) listBox3.SelectedItem).Id);

            MessageBox.Show("Ta-da!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox7.Text))
                return;

            new ApiConnector().ForController("group")
                .AddParameter("userId", this.user.Id)
                .Post(textBox7.Text);

            textBox7.Clear();
            RefreshTab3List();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox8.Text))
                return;

            new ApiConnector().ForController("recommendation")
                .AddParameter("userId", this.user.Id)
                .AddParameter("showId", ((Show)listBox1.SelectedItem).Id)
                .AddParameter("toGroup", false)
                .Post(textBox8.Text);

            textBox8.Clear();
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                var group = (UserGroup)listBox3.SelectedItem;

                new ApiConnector().ForController("group")
                    .AddParameter("userId", this.user.Id)
                    .AddParameter("groupId", group.Id)
                    .Delete();

                listBox3.Items.Remove(group);

                RefreshTab3List();
            }
        }

        private void RefreshTab3List()
        {
            listBox3.Items.Clear();

            var groups = new ApiConnector<List<UserGroup>>().ForController("group")
                .AddParameter("userId", this.user.Id)
                .Get();

            foreach (var userGroup in groups)
            {
                listBox3.Items.Add(userGroup);
            }
        }

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                RefreshTab2List();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                if (this.user.UserLevel == UserLevel.Regular)
                {
                    MessageBox.Show("You are not a premium user!");
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    button4.Enabled = button5.Enabled = button7.Enabled = listBox1.SelectedIndex != -1;
                    RefreshTab3List();
                }
            }
        }
    }
}
