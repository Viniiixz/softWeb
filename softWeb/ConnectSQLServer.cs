﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace softWeb
{
    public class ConnectSQLServer
    {
        SqlConnection con = new SqlConnection();

        //Constructor
        public ConnectSQLServer()
        {
            con.ConnectionString = "Data Source=VINIIIXZ\\SQLSERVER;Initial Catalog=AdventureWorksDW2019;Integrated Security=True";
        }

        //Checking connection in SQL
        public SqlConnection Connect()
        {
            //Validate to connection
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            return con;
        }

        //Method disconnect
        public void Disconnect()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
}
