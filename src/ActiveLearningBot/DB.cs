using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ActiveLearningBot
{
    public class DB
    {
        public static void SaveMessage(string message)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("INSERT INTO dbo.Message (Message, Date) VALUES (@Message, @Date)", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Message", message));
                    cmd.Parameters.Add(new SqlParameter("@Date", DateTime.Now));
                        cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static string GetLatestMessage()
        {
            string message = string.Empty;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"DECLARE @Id INT
                                                  DECLARE @Message VARCHAR(MAX)  
                                                  SELECT TOP(1) @Id = Id, @Message = Message FROM dbo.Message WHERE Learned = 0 ORDER BY Date DESC
                                                  UPDATE dbo.Message SET Learned = 1 WHERE Id = @Id
                                                  SELECT @Message", conn))
                    message = cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            return message;
        }
    }
}