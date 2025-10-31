using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ADO_NET_DisconnectedDemo
{
     class ConnectionLess
    {
        static void Main(string[] args)
        {
            string connStr = "Server=localhost;Database=minipayrolldb;User ID=root;Password=nisharg2004;";

        
            using MySqlConnection conn = new MySqlConnection(connStr);
            string selectQuery = "SELECT * FROM prlm01";
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);

 
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);

       
            DataSet ds = new DataSet();

            try
            {
            
                adapter.Fill(ds, "Departments");
                DataTable table = ds.Tables["Departments"];

       
            
                Console.WriteLine("Current Departments:");
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"{row["t01f01"],-5} | {row["t01f02"]}");
                }

          
         
                DataRow newRow = table.NewRow();
                newRow["t01f02"] = "Management Department";
                table.Rows.Add(newRow);


                if (table.Rows.Count > 0)
                {
       
                    table.Rows[0]["t01f02"] = table.Rows[0]["t01f02"] + " (Updated)";
                }

                if (table.Rows.Count > 1)
                {
                    table.Rows[^1].Delete();  // ^1 = last row
                }


                int rowsAffected = adapter.Update(ds, "Departments");
                Console.WriteLine($"Database updated ({rowsAffected} rows affected).");


                ds.Clear();
                adapter.Fill(ds, "Departments");

                foreach (DataRow row in ds.Tables["Departments"].Rows)
                {
                    Console.WriteLine($"{row["t01f01"],-5} | {row["t01f02"]}");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }
    }
}
