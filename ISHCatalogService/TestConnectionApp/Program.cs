using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        var connectionString = "Server=localhost;Database=ISHCatalog;Trusted_Connection=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }
        }
    }
}