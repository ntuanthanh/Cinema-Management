using SE1608_Group1_A2.DAL;
using SE1608_Group1_A2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE1608_Group1_A2.GUI
{
    public partial class BookingAddGUI : Form
    {
        char[] click = new char[100];
        decimal? price = 0;
        Show s;
        Dictionary<String, Point> checkboxes = new Dictionary<String, Point>();
        CinemaContext context;
        int showId;
        public BookingAddGUI(int showId, CinemaContext context)
        {
            for (int i = 0; i < click.Length; i++)
            {
                click[i] = '0';
            }
            this.context = context;
            this.showId = showId;
            InitializeComponent();
            generateCheckbox();
            s = context.Shows.Find(showId);
            amount.Enabled = false;
           
        }

        public void generateCheckbox()
        {
            string[] bookedMatrix = ShowDAO.GetInstance().BookedMatrix(showId);
            int key = 0;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {

                    Point location1 = new Point((11 + x) * (15 + 10), (y + 2) * (15 + 10));

                    Point location2 = new Point(x * (15 + 10), y * (15 + 10));

                    CheckBox checkBox = new CheckBox();

                    checkBox.Location = location1;
                    checkBox.Text = string.Empty;
                    checkBox.Size = new Size(18, 17);


                    int index = y * 10 + x;

                    if (bookedMatrix[index] == "1")
                    {
                        checkBox.Checked = true;
                        checkBox.Enabled = false;
                    }
                    else
                    {
                        checkBox.CheckedChanged += checkBox1_CheckedChanged;
                    }
                    checkBox.Name = "cbox" + key++;

                    this.Controls.Add(checkBox);
                    checkboxes.Add(checkBox.Name, new Point(x, y));
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string name = ((CheckBox)sender).Name;
            Point index = checkboxes.GetValueOrDefault(name);
            int arrIndex = index.Y * 10 + index.X;
            if (click[arrIndex] == '0')
            {
                price = price + s.Price; 
                click[arrIndex] = '1';
                amount.Text = price.ToString();
            }
            else if (click[arrIndex] == '1')
            {
                price = price - s.Price;
                click[arrIndex] = '0';
                amount.Text = price.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (price == 0)
            {
                MessageBox.Show("No seat is booked");
                return;
            }
            string seat = new string(click);
            Booking b = new Booking();
            b.ShowId = showId;
            b.SeatStatus = seat;
            b.Name = name.Text;
            b.Amount = decimal.Parse(amount.Text);
            context.Bookings.Add(b);
            context.SaveChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
