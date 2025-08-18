using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame
{
    public class DatabaseConnection
    {
        public DatabaseConnection() 
        {
            // All information I need to connect to database
            ServerName = Properties.Resources.serverName;
            DatabaseName = Properties.Resources.databaseName;
            DatabaseUsername = Properties.Resources.databaseUsername;
            DatabasePassword = Properties.Resources.databasePassword;

            string connString = string.Format("Server={0}; database={1}; UID={2}; password ={3}", ServerName, DatabaseName, DatabaseUsername, DatabasePassword);
            Connection = new MySqlConnection(connString);
        }

        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseUsername { get; set; }
        public MySqlConnection Connection { get; set; }
        public MySqlDataReader Reader { get; set; }
        public MySqlCommand Command { get; set; }

        // Gets user info from the database
        public bool selectQuery(string tableName, string [] args)
        {
           
           try
            {
                Connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Database Connection Error!", "Database Error", MessageBoxButtons.OK);
            }

            Command = Connection.CreateCommand();

            Command.CommandText = "SELECT * FROM " + tableName + " WHERE username = @username and password = @password";
            Command.Parameters.AddWithValue("@username", args[0]);
            Command.Parameters.AddWithValue("@password", args[1]);

            Reader = Command.ExecuteReader();

            if(Reader.HasRows)
            {
                Connection.Close();
                return true;
            }
            else
            {
                Connection.Close();
                return false;
            }
        }

        // Inserts data into database
        public int insertQuery(string username, int score, int time, DateTime date)
        {
            string query = "INSERT INTO scores (username, score, time, date) VALUES (@username,@score,@time,@date)";
            
            MySqlCommand cmd = new MySqlCommand(query, Connection);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@date", date.ToString("MM-dd-yyyy HH:mm:ss"));

            Connection.Open();

            int result = 0;

            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Connection.Close();
            }

            Connection.Close();

            return result;
        }

        // Select top 5 best scores
        public void selectScores(List<Scores> scores)
        {
            try
            {
                Connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Database Connection Error!", "Database Error", MessageBoxButtons.OK);
            }

            Command = Connection.CreateCommand();

            Command.CommandText = "SELECT * FROM scores ORDER BY score DESC LIMIT 5";

            Reader = Command.ExecuteReader();

            if(Reader.HasRows)
            {
                while(Reader.Read())
                {
                    scores.Add(new Scores
                    {
                        Username = Reader["username"].ToString(),
                        Score = Reader["score"].ToString(),
                        Time = (Convert.ToInt64(Reader["time"].ToString()) / 40).ToString(),
                        Date = Reader["date"].ToString()
                    });
                }
            }
            else
            {

            }

            Connection.Close();
        }
    }
}
