﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE1608_Group1_A2.DAL
{
    public class RoomDAO:DAO
    {
        
        static RoomDAO Instance;
        RoomDAO() { }
        static RoomDAO() => Instance = new RoomDAO();

        public static RoomDAO GetInstance() => Instance;

        public DataTable GetDataTable() => GetDataTable("select * from rooms");
    }
}
