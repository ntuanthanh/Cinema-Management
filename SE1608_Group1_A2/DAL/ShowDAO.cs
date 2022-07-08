using Microsoft.Data.SqlClient;
using SE1608_Group1_A2.DTL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE1608_Group1_A2.DAL
{
    class ShowDAO:DAO
    {
        static ShowDAO Instance;
        ShowDAO() { }
        static ShowDAO() => Instance = new ShowDAO();

        public static ShowDAO GetInstance() => Instance;

        public DataTable GetDataTable() => GetDataTable("select * from shows");

        public Show GetById(int id)
        {
            DataTable dt = GetDataTable("select * from shows where showId = " + id);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            Show show = new Show
            {
                ShowId = (int)row["showId"],
                RoomId = (int)row["roomId"],
                FilmId = (int)row["filmId"],
                ShowDate = (DateTime)row["showDate"],
                Status = (bool)row["status"],
                Slot = (int)row["slot"],
                Price = (decimal)row["price"]
            };
            return show;
        }
        public void Update(Show show)
        {
            SqlCommand cmd = new SqlCommand("Update shows set filmId = @filmId, "+
                "slot = @slot, price = @price Where showId = @showId");
            cmd.Parameters.AddWithValue("@filmId", show.FilmId);
            cmd.Parameters.AddWithValue("@slot", show.Slot);
            cmd.Parameters.AddWithValue("@price", show.Price);
            cmd.Parameters.AddWithValue("@showId", show.ShowId);

            Update(cmd);
        }


        public string[] BookedMatrix(int ShowId)
        {
            string sql = "select *from Bookings where ShowID=" + ShowId;
            DataTable dt = GetDataTable(sql);
            List<string> ListSeats = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string seat = dr["SeatStatus"].ToString();
                ListSeats.Add(seat);
            }
            //return ListSeats[0].Length.ToString();

            string[] BookedMatrix = new string[100];
            for (int i = 0; i < 100; i++)
            {
                BookedMatrix[i] = "0";
            }

            foreach (string seat in ListSeats)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (seat[i] == '1') BookedMatrix[i] = "1";
                }
            }
            return BookedMatrix;
        }

        public string IndividualBookedMatrix(int ShowId, int userId)
        {
            string sql = "select *from Bookings where ShowID=" + ShowId;
            DataTable dt = GetDataTable(sql);
            DataRow dataRow = dt.Rows[0];
            string seat = dataRow["SeatStatus"].ToString();
            return seat;
        }

    }
}
