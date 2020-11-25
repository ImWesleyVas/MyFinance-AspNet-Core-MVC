﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyFinance.Util
{
    public class DAL
    {

        private static string server = "localhost";
        private static string database = "FinancasPessoais";
        private static string user = "developer";
        private static string password = "study";
        private string connectionString = $"Server={server}; Database={database}; Uid={user}; Pwd={password} ";
        private MySqlConnection connection;

        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        // Executa SELECTs
        public DataTable RetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(command);

            da.Fill(dataTable);
            return dataTable;
        }

        // Executa INSERTs, UPDATEs, DELETEs

        public void ExecutarComandoSQL(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
