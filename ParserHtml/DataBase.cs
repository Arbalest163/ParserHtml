using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace ParserHtml
{
    /// <summary>
    /// класс DataBase выгружает словарь в базу данных
    /// </summary> 
    class DataBase
    { 
        private string _fileName;
        private string _dbConnect;
        private SQLiteConnection _sqlConnection;
        private SQLiteCommand _sqlCmd;

        public DataBase()
        {
            _fileName = "TestDB.db";
            _dbConnect = $"Data Source = {_fileName}; Version = 3; ";
            _sqlConnection = new SQLiteConnection(_dbConnect);
            _sqlCmd = new SQLiteCommand();
        }

        /// <summary>
        /// Метод выгружает принятый словарь в базу данных
        /// </summary>
        /// <param name="dictionary"></param>
        public void UploadToSql(Dictionary<string, int> dictionary)
        {
            try
            {
                createEmptyDataBase();

                _sqlConnection = new SQLiteConnection(_dbConnect);
                _sqlConnection.Open();

                _sqlCmd.Connection = _sqlConnection;

                foreach (var item in dictionary)
                {
                    _sqlCmd.CommandText = $"INSERT INTO [dbTableName] " +
                                            $"([word], [countWord]) " +
                                            $"VALUES('{@item.Key}', '{@item.Value}')";
                    _sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }

        }

        /// <summary>
        /// Методсоздаёт пустой файл базы данных
        /// </summary>
        private void createEmptyDataBase()
        {
            try
            {
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                    SQLiteConnection.CreateFile(_fileName);
                }

                _sqlConnection = new SQLiteConnection(_dbConnect);
                _sqlConnection.Open();

                _sqlCmd.Connection = _sqlConnection;
                _sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS [dbTableName] " +
                                      "( [id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                                      "[word] NVARCHAR(20), " +
                                      "[countWord] INTEGER)";
                _sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }

        }
    }

}

