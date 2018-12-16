using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HackHarvardAPI.Infrastructure
{
    public class API
    {
        private static string connectionString = "Server=tcp:transnetserver.database.windows.net,1433;Initial Catalog=API_transnet;Persist Security Info=False;User ID=admin_server;Password=romir123123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool InsertRate(Int64 Rate)
        {
            string queryString =
                "UPDATE elec_log  SET power = " + Rate + " WHERE seq = 1";

            if (LOG_TO_DB(queryString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertTotal(Int64 Total)
        {
            Total = Total + ReadTotal();
            string queryString =
                "UPDATE elec_log  SET total = " + Total + " WHERE seq = 1";

            if (LOG_TO_DB(queryString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ResetTotal()
        {
            string queryString =
                "UPDATE elec_log  SET total = 0 WHERE seq = 1";

            if (LOG_TO_DB(queryString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Int64 ReadRate()
        {
            string queryString =
                "SELECT [power] FROM[dbo].[elec_log] WHERE seq = 1";

            return Read_(queryString);
        }

        public Int64 ReadTotal()
        {
            string queryString =
                "SELECT [total] FROM[dbo].[elec_log] WHERE seq = 1";

            return Read_(queryString);
        }

        static Int64 Read_(string queryString)
        {
            Int64 reading = 0;
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        reading = Convert.ToInt64(reader[0].ToString());
                    }
                    reader.Close();

                    //if (email == "" || email == null || password == "" || password == null)
                    //  return false;

                    return reading;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }

        }

        bool LOG_TO_DB(string queryString)
        {
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

        }
    }
}
