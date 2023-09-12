using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
            List<int> columnWidths = new List<int>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    //retrieve the SQL Server instance version

                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        PrintDataTable(ds.Tables[0]);
                    }

                    
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        static void PrintDataTable(DataTable dataTable)
        {
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();

            // Calculate the minimum width for each column
            int[] columnWidths = new int[dataTable.Columns.Count];
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                int maxColumnWidth = columnNames[i].Length;
                foreach (DataRow row in dataTable.Rows)
                {
                    string value = row[i].ToString();
                    if (value.Length > maxColumnWidth)
                    {
                        maxColumnWidth = value.Length;
                    }
                }
                columnWidths[i] = maxColumnWidth + 2; // Add margin
            }

            // Print the table
            Console.WriteLine(new string('=', columnWidths.Sum() + columnWidths.Length - 1));
            foreach (var columnName in columnNames)
            {
                int itemIndex = columnNames.ToList().IndexOf(columnName);
                string formattedItem = columnName.PadRight(columnWidths[itemIndex]);
                Console.Write($"| {formattedItem} ");
                
            }

            Console.WriteLine("|");
            Console.WriteLine(new string('=', columnWidths.Sum() + columnWidths.Length - 1));

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    int itemIndex = row.ItemArray.ToList().IndexOf(item);
                    string formattedItem = item.ToString().PadRight(columnWidths[itemIndex]);
                    Console.Write($"| {formattedItem} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine(new string('=', columnWidths.Sum() + columnWidths.Length - 1));
        }
    }
}
