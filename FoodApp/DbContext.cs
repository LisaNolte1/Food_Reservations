using System.Data.SqlClient;
using System.Data;
using System;

namespace FoodApp
{
    public class DbContext : IDisposable
    {
        private readonly string connectionString = "data source=REECES\\SQLEXPRESS; database=FoodApp; User ID=259;Password=;Integrated Security=SSPI";
        private readonly SqlConnection connection;

        public DbContext()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable result = new DataTable();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(result);
                }
            }

            return result;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteNonQuery();
            }
        }
    }
}
