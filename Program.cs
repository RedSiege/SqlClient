using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = string.Empty;
            

            ArgOptions options = CommandLineArguments.ArgParse(args);
            bool error = false;
            
            if (string.IsNullOrEmpty(options.DatabaseName))
            {
                error = true;
            }
            if (string.IsNullOrEmpty(options.Server))
            {
                error = true;
            }
            if (string.IsNullOrEmpty(options.Query))
            {
                error = true;
            }

            if (error)
            {
                Console.WriteLine("[*]Ex: SqlClient.exe --username <username> --password <password> --server <IP Address|host> --database <databasename> <SQL Query> ");
                Console.WriteLine("[*]Ex: SqlClient.exe --server <IP Address|host> --database <databasename> <SQL Query> ");
                return;
            }

            if (string.IsNullOrEmpty(options.UserName) && string.IsNullOrEmpty(options.Password))
            {
                connString = $"Server={options.Server};Database={options.DatabaseName}; Integrated Security=True";
            }
            else
            {
                connString = $"Server={options.Server};Database={options.DatabaseName}; User ID={options.UserName};Password={options.Password}";
            }

            string query = options.Query;


            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    

                    SqlCommand cmd = new SqlCommand(query, conn);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //display retrieved record (first column only/string value)
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                Console.WriteLine(dr.GetName(i));
                            }
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                Console.WriteLine(dr.GetValue(i));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
