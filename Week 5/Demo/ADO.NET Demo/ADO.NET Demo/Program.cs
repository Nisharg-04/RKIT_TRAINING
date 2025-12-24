using System;
using MySql.Data.MySqlClient;

namespace ADO.NET_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=minipayrolldb;User ID=root;Password=****;";


            using MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();
                Console.WriteLine("Connection established successfully!\n");
                string selectQuery = "SELECT t01f01, t01f02 FROM prlm01";
                using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("t01f01");
                        string name = reader.GetString("t01f02");
                        Console.WriteLine($"ID: {id}, Name: {name}");
                    }
                    // while (reader.Read())
                    // {
                    //     int id = reader.GetInt32("t01f01");
                    //     string name = reader.GetString("t01f02");
                    //     Console.WriteLine($"ID: {id}, Name: {name}");
                    // }
                }

                //Console.WriteLine("\nInserting new record");
                //string insertQuery = "INSERT INTO prlm01 (t01f02) VALUES (@name)";
                //using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                //{
                //    insertCmd.Parameters.AddWithValue("@name", "Public Welfare Department");
                //    int rows = insertCmd.ExecuteNonQuery();
                //    Console.WriteLine($"Rows inserted: {rows}");
                //}


                //Console.WriteLine("\nUpdating a record");
                //string updateQuery = "UPDATE prlm01 SET t01f02 = @newName WHERE t01f02 = @oldName";
                //using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                //{
                //    updateCmd.Parameters.AddWithValue("@newName", "Updated Welfare");
                //    updateCmd.Parameters.AddWithValue("@oldName", "Public Welfare Department");
                //    int updated = updateCmd.ExecuteNonQuery();
                //    Console.WriteLine($"Rows updated: {updated}");
                //}

                //Console.WriteLine("\nDeleting a record");
                //string deleteQuery = "DELETE FROM prlm01 WHERE t01f02 = @name";
                //using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                //{
                //    deleteCmd.Parameters.AddWithValue("@name", "Updated Welfare");
                //    int deleted = deleteCmd.ExecuteNonQuery();
                //    Console.WriteLine($"Rows deleted: {deleted}");
                //}

                //Console.WriteLine("\nTransaction demo");

                //using (MySqlTransaction transaction = conn.BeginTransaction())
                //{
                //    try
                //    {
                //        string q1 = "INSERT INTO prlm01 (t01f02) VALUES (@name1)";
                //        string q2 = "INSERT INTO prlm01 (t01f02) VALUES (@name2)";

                //        using (MySqlCommand cmd1 = new MySqlCommand(q1, conn, transaction))
                //        using (MySqlCommand cmd2 = new MySqlCommand(q2, conn, transaction))
                //        {
                //            cmd1.Parameters.AddWithValue("@name1", "HR Department");
                //            cmd2.Parameters.AddWithValue("@name2", "Finance Department");

                //            cmd1.ExecuteNonQuery();
                //            cmd2.ExecuteNonQuery();
                //        }

                //        transaction.Commit();
                //        Console.WriteLine("Transaction committed successfully!");
                //    }
                //    catch (Exception tranEx)
                //    {
                //        transaction.Rollback();
                //        Console.WriteLine($"Transaction rolled back due to error: {tranEx.Message}");
                //    }
                //}
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                Console.WriteLine("\nConnection closed.");
            }
        }
    }
}
