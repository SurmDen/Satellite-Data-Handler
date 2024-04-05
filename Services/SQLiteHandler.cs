using Microsoft.Data.Sqlite;
using SQLite_Parser.Interfaces;
using SQLite_Parser.Models;
using System.Text;

namespace SQLite_Parser.Services
{
    public class SQLiteHandler : ISQLiteHandler
    {
        public List<SignalModel> GetData(string dbPath)
        {
            string sqlExpression = "SELECT * from Signals";

            List<SignalModel> signalModels = new List<SignalModel>();

            using (SqliteConnection connection = new SqliteConnection($"Data Source={dbPath};Mode=ReadWrite"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = sqlExpression;

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SignalModel signalModel = new SignalModel();

                            signalModel.Id = reader.GetInt64(0);

                            signalModel.FrequencyMHz = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);

                            signalModel.BandWidthMHZ = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);

                            signalModel.Level = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);

                            signalModel.SNR = reader.IsDBNull(4) ? 0 : reader.GetDouble(4);

                            signalModel.StandartType = reader.IsDBNull(5) ? "" : reader.GetString(5);

                            signalModel.ModeType = reader.IsDBNull(6) ? "" : reader.GetString(6);

                            signalModel.SpeedMBod = reader.IsDBNull(7) ? 0 : reader.GetDouble(7);

                            signalModel.TimeFirst = reader.GetDateTime(8);

                            signalModel.TimeLast = reader.GetDateTime(9);

                            signalModel.Recorder = reader.IsDBNull(10) ? false : reader.GetBoolean(10);

                            signalModel.FileName = reader.IsDBNull(11) ? "" : reader.GetString(11);

                            signalModel.FileSize = reader.IsDBNull(12) ? 0 : reader.GetDouble(12);

                            signalModel.RecognitionType = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);

                            signalModel.StateTA = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);

                            signalModel.TechnicalAnalisysResult = reader.IsDBNull(15) ? 0 : reader.GetInt64(15);

                            signalModel.SignalStrenght = reader.IsDBNull(16) ? 0 : reader.GetDouble(16);

                            signalModels.Add(signalModel);
                        }
                    }
                }

                connection.Close();
            }

            return signalModels;

        }
    }
}
