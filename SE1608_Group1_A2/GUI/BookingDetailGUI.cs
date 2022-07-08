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
    public partial class BookingDetailGUI : Form
    {
        public BookingDetailGUI(Booking b)
        {
            InitializeComponent();
            generateCheckbox(b.SeatStatus);
            name.Text = b.Name;
            amount.Text = b.Amount.ToString();
            name.Enabled = false;
            amount.Enabled = false;
        }

        public void generateCheckbox(string seatStatus)
        {
            Dictionary<Point, CheckBox> checkboxes = new Dictionary<Point, CheckBox>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    /*         int x1 = 100 + x;
                             int y2 = 100 + y;*/

                    Point location = new Point((10 + x) * (15 + 10), (y + 1) * (15 + 10));

                    CheckBox checkBox = new CheckBox();

                    checkBox.Location = location;
                    checkBox.Text = string.Empty;
                    checkBox.Size = new Size(18, 17);
                    checkBox.Enabled = false;

                    int index = y * 10 + x;

                    if (seatStatus[index] == '1')
                        checkBox.Checked = true;

                    this.Controls.Add(checkBox);
                    checkboxes.Add(new Point(x, y), checkBox);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
