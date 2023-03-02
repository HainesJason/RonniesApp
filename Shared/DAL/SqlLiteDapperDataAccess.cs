using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace SMS.Shared.DAL
{

    public class SqlLiteDapperDataAccess : IDataAccess
    {
        private readonly string _dbFile = string.Empty;
        private readonly string _connectionString = string.Empty;

        public SqlLiteDapperDataAccess()
        {

            //_dbFile = Environment.CurrentDirectory + "\\sms.sqlite";
            // _dbFile =  ".\\data\\sms.db";
            // _dbFile = "./wwwroot/data/sms.db";
            _dbFile = "ronnies.db";
            // _connectionString = "Data Source= " + _dbFile;

            _connectionString = "Data Source=ronnies.db;Cache=Shared";
            if (!File.Exists(_dbFile))
            {
                CreateDatabase();
            }


        }

        private void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Execute(
                    @"CREATE TABLE Player (
	                Id	INTEGER NOT NULL UNIQUE,
	                Firstname	TEXT NOT NULL,
	                Lastname	TEXT NOT NULL,
	                Email	TEXT NOT NULL,
	                PhoneNumber	TEXT,
	                IsActivePlayer	NUMERIC NOT NULL,
	                PRIMARY KEY(Id AUTOINCREMENT))");

                cnn.Execute(
                    @"CREATE TABLE Fixture (
	                Id	INTEGER NOT NULL UNIQUE,
	                Opponent	TEXT NOT NULL,
	                DateOfFixture	INTEGER NOT NULL,
	                Venue	INTEGER NOT NULL,
	                StartTime	TEXT NOT NULL,
	                Postcode	TEXT,
	                NumberOfPlayersRequired	INTEGER NOT NULL,
	                PRIMARY KEY(Id AUTOINCREMENT))");

                cnn.Execute(
                    @"
                CREATE TABLE Availability (
	                Id	INTEGER NOT NULL UNIQUE,
	                FixtureId	INTEGER NOT NULL,
	                PlayerId	INTEGER NOT NULL,
	                PRIMARY KEY(Id AUTOINCREMENT),
	                FOREIGN KEY(FixtureId) REFERENCES Fixture,
	                FOREIGN KEY(PlayerId) REFERENCES Fixture)");
            }
        }
        private SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection(_connectionString);
        }


        public async Task ExecuteACommand<T>(string sqlStatement, T parameters, string connectionString, CommandType commandType = CommandType.Text)
        {
            connectionString = _connectionString;
            //using IDbConnection connection = new SQLiteConnection(_connectionString);
            //await connection.ExecuteAsync(sqlStatement, parameters, commandType: commandType);

            using (IDbConnection connection = new SQLiteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlStatement, parameters, commandType: commandType);
            }
        
        }

        public async Task<IEnumerable<T>> RunAQuery<T, U>(string sqlStatement, U parameters, string connectionString, CommandType commandType = CommandType.Text)
        {
            try
            {
                connectionString = _connectionString;
                //using IDbConnection connection = new SqliteConnection(connectionString);
                //return await connection.QueryAsync<T>(sqlStatement, parameters, commandType: commandType);

                using (IDbConnection connection = new SqliteConnection(connectionString))
                {
                    return await connection.QueryAsync<T>(sqlStatement, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}