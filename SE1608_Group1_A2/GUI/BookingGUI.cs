using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
//using SE1608_Group1_A2.DAL;
//using SE1608_Group1_A2.DTL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SE1608_Group1_A2.Models;
using SE1608_Group1_A2.DAL;

namespace SE1608_Group1_A2.GUI
{

    public partial class BookingGUI : Form
    {
        CinemaContext context;
        int showId;
        public BookingGUI(CinemaContext db, int showId)
        {
            this.context = db;
            this.showId = showId;
            InitializeComponent();
            bindGrid(false, db);
            generateCheckbox();
        }

        public BookingGUI()
        {
        }

        void bindGrid(bool filter, CinemaContext context)
        {

            //DataTable dt = ShowDAO.GetInstance().GetDataTable();
            //int count = dt.Columns.Count;

            //dataGridView1.DataSource = dt;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = context.Bookings.Where(x => x.ShowId == showId).ToList();
            int count = dataGridView1.Columns.Count;
            int total = dataGridView1.Rows.Count;
            textotal.Text = total.ToString();

            DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn
            {
                Name = "Detail",
                Text = "Detail",
                UseColumnTextForButtonValue = true
            };

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };


            dataGridView1.Columns.Insert(count, btnDetail);
            dataGridView1.Columns.Insert(count + 1, btnDelete);

            dataGridView1.Columns["ShowId"].Visible = false;
            dataGridView1.Columns["BookingId"].Visible = false;
            dataGridView1.Columns["Show"].Visible = false;

        }
        public void generateCheckbox()
        {
            string[] bookedMatrix = ShowDAO.GetInstance().BookedMatrix(showId);
            Dictionary<Point, CheckBox> checkboxes = new Dictionary<Point, CheckBox>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    /*         int x1 = 100 + x;
                             int y2 = 100 + y;*/

                    Point location = new Point((11 + x) * (15 + 10), (y + 2) * (15 + 10));

                    CheckBox checkBox = new CheckBox();

                    checkBox.Location = location;
                    checkBox.Text = string.Empty;
                    checkBox.Size = new Size(18, 17);


                    int index = y * 10 + x;

                    if (bookedMatrix[index] == "1")
                        checkBox.Checked = true;
                    checkBox.Enabled = false;

                    this.Controls.Add(checkBox);
                    checkboxes.Add(new Point(x, y), checkBox);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Detail"].Index)
            {
                int bid, showId;
                bid = (int)dataGridView1.Rows[e.RowIndex].Cells["BookingID"].Value;
                showId = (int)dataGridView1.Rows[e.RowIndex].Cells["ShowID"].Value;

                Booking b = context.Bookings.Where(x => (x.ShowId == showId && x.BookingId == bid)).ToList().SingleOrDefault();

                BookingDetailGUI f = new BookingDetailGUI(b);
                DialogResult dr = f.ShowDialog();
            }
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                int bid, showId;
                bid = (int)dataGridView1.Rows[e.RowIndex].Cells["BookingID"].Value;
                showId = (int)dataGridView1.Rows[e.RowIndex].Cells["ShowID"].Value;
                //Show show = ShowDAO.GetInstance().GetById(showId);
                // Show show = context.Shows.Find(showId);

                DialogResult dr = MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    Booking b = context.Bookings.Where(x => (x.ShowId == showId && x.BookingId == bid)).ToList().SingleOrDefault();
                    context.Bookings.Remove(b);
                    context.SaveChanges();
                    ResetForm();
                }
                else if (dr == DialogResult.No)
                {

                }
                /*if (dr == DialogResult.OK)
                    bindGrid(true);*/
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookingAddGUI f = new BookingAddGUI(showId, context);
            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("New booking is added");
                ResetForm();
            }



        }
        private void ResetForm()
        {
            this.Controls.Clear();
            InitializeComponent();
            bindGrid(false, context);
            generateCheckbox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
