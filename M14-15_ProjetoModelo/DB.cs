﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace M14_15_ProjetoModelo
{
    class DB
    {
        private static DB instance;

        public static DB Instance
        {
            get
            {
                if (instance == null) instance = new DB();

                return instance;
            }
        }

        string strConnect;
        SqlConnection dbConnection;

        public DB()
        {
            strConnect = ConfigurationManager.ConnectionStrings["sql"].ToString();
            dbConnection = new SqlConnection(strConnect);
            dbConnection.Open();
        }

        ~DB()
        {
            try
            {
                dbConnection.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message); 
            }
        }
        
        public void ExecSQL(string sql)
        {
            SqlCommand command = new SqlCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            command.Dispose();
            command = null;
        }

        public void ExecSQL(string sql, List<SqlParameter> parameters)
        {
            SqlCommand command = new SqlCommand(sql, dbConnection);
            command.Parameters.AddRange(parameters.ToArray());
            command.ExecuteNonQuery();
            command.Dispose();
            command = null;
        }

        public DataTable ExecQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, dbConnection);
            DataTable registry = new DataTable();
            SqlDataReader data =  command.ExecuteReader();
            registry.Load(data);
            data = null;
            command.Dispose();
            return registry;

        }
    }
}
