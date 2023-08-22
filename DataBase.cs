using AitukCore.Interfaces;
using AitukCore.Models;
using MySql.Data.MySqlClient;
using Rocket.Unturned.Player;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore
{
    public static class DataBase
    {
        public static IEnumerable<IAitukPlayer> GetPlayers()
        {
            List<IAitukPlayer> list = new List<IAitukPlayer>();
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Players";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CSteamID cSteamID = new CSteamID(reader.GetUInt64("CSteamID"));
                            int fractionId = reader.GetInt32("FractionId");
                            int fracrionLVL = reader.GetInt32("FractionLVL");
                            string lang = reader.GetString("Language");

                            AitukPlayer aitukPlayer = new AitukPlayer(cSteamID, fractionId, fracrionLVL, lang);

                            list.Add(aitukPlayer);
                        }
                    }
                }

                connection.Close();
            }
            return list;
        }

        public static IEnumerable<IAitukFraction> GetFractions()
        {
            List<IAitukFraction> list = new List<IAitukFraction>();
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Fractions";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AitukFraction aitukFraction = new AitukFraction()
                            {
                                Id = reader.GetInt32("Id"),
                                Balance = reader.GetUInt32("Balance")
                            };

                            list.Add(aitukFraction);
                        }
                    }
                }

                connection.Close();
            }
            return list;
        }

        public static int GetBalance(this IAitukFraction aitukFraction)
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.ConnectionString))
            {
                connection.Open();

                string sql = $"SELECT * FROM Fractions WHERE Id = {aitukFraction.Id}";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetInt32("Balance");
                        }
                    }
                }

                connection.Close();
            }
            return 0;
        }

        public static IAitukPlayer APlayer(this UnturnedPlayer uplayer)
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.ConnectionString))
            {
                connection.Open();

                string sql = $"SELECT * FROM Players WHERE CSteamID = {uplayer.CSteamID}";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CSteamID steamID = new CSteamID(reader.GetUInt64("CSteamID"));
                            int fractionId = reader.GetInt32("FractionId");
                            int fractionLVL = reader.GetInt32("FractionLVL");
                            string language = reader.GetString("Language");

                            return new AitukPlayer(steamID, fractionId, fractionLVL, language);
                        }
                    }
                }

                connection.Close();
            }

            //Если игрок не найден, создаем его
            uplayer.CreateAPlayer();
            uplayer.APlayer();

            return null;
        }

        public static void Save(this IAitukPlayer iplayer)
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.ConnectionString))
            {
                connection.Open();

                string sql = $"UPDATE Players SET FractionId = {iplayer.FractionId}, FractionLVL = {iplayer.FractionLVL}, Language = '{iplayer.Language}' WHERE CSteamID = {iplayer.CSteamID}";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteReader();
                }

                connection.Close();
            }
        }

        public static void CreateAPlayer(this UnturnedPlayer uplayer)
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.ConnectionString))
            {
                connection.Open();

                string sql = $"INSERT INTO Players (CSteamID, FractionId, FractionLVL, Language) VALUES ({uplayer.CSteamID}, 0, 0, '{Properties.Settings.Default.DefaultLanguage}')";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteReader();
                }

                connection.Close();
            }
        }
    }
}
