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
    public partial class ReviewForm : Form
    {
        public ReviewForm(List<UserReview> reviews)
        {
            InitializeComponent();

            foreach (var review in reviews)
            {
                listBox1.Items.Add(review);
            }

            label1.Text = $"Average: {reviews.Select(x=>x.Rating).Average()}";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var review = (UserReview) listBox1.SelectedItem;

                textBox1.Text = review.Review;
            }
            else
            {
                textBox1.Clear();
            }
        }
    }
}
