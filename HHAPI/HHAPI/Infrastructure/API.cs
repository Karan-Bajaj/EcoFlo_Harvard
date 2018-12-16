using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HHAPI.Infrastructure
{
    public class API
    {
        private static string connectionString = "Server=tcp:hackharvard.database.windows.net,1433;Initial Catalog=HHDB;Persist Security Info=False;User ID=database_admin;Password=Casio123@@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static bool InsertRate(Int64 Rate)
        {
            InsertTotal(Rate);

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

        public static bool InsertTotal(Int64 Total)
        {
            Total = (Total/4) + ReadTotal();
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

        public static bool ResetTotal()
        {
            string queryString =
                "UPDATE elec_log  SET total = 0 WHERE seq = 1";

            if (LOG_TO_DB(queryString))
            {
                queryString =
                "UPDATE elec_log  SET power = 0 WHERE seq = 1";
                if (LOG_TO_DB(queryString))
                { return true; }
                return false;
            }
            else
            {
                return false;
            }
        }

        public static Int64 ReadRate()
        {
            string queryString =
                "SELECT [power] FROM [dbo].[elec_log] WHERE seq = 1";

            return Read_(queryString);
        }

        public static Int64 ReadTotal()
        {
            string queryString =
                "SELECT [total] FROM [dbo].[elec_log] WHERE seq = 1";

            return Read_(queryString);
        }

        public static Int64 Read_(string queryString)
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
                        reading = Convert.ToInt64(reader[0]);
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

        public static bool LOG_TO_DB(string queryString)
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
