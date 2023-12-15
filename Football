using System;
using System.Data.SqlClient;
using System.Data;

class Program
{
    static void Main()
    {
        string connectionString = "YourConnectionStringHere"; // Потрібно замінити на власний рядок підключення до бази даних

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Створення таблиці для команд
            string createTeamsTableQuery = @"
                CREATE TABLE Teams (
                    TeamId INT PRIMARY KEY,
                    TeamName NVARCHAR(100),
                    Country NVARCHAR(50)
                )";
            ExecuteQuery(connection, createTeamsTableQuery);

            // Створення таблиці для гравців
            string createPlayersTableQuery = @"
                CREATE TABLE Players (
                    PlayerId INT PRIMARY KEY,
                    TeamId INT,
                    FullName NVARCHAR(100),
                    Country NVARCHAR(50),
                    JerseyNumber INT,
                    Position NVARCHAR(50),
                    FOREIGN KEY (TeamId) REFERENCES Teams(TeamId)
                )";
            ExecuteQuery(connection, createPlayersTableQuery);

            // Створення таблиці для матчів
            string createMatchesTableQuery = @"
                CREATE TABLE Matches (
                    MatchId INT PRIMARY KEY,
                    Team1Id INT,
                    Team2Id INT,
                    GoalsTeam1 INT,
                    GoalsTeam2 INT,
                    Scorer NVARCHAR(100),
                    MatchDate DATETIME,
                    FOREIGN KEY (Team1Id) REFERENCES Teams(TeamId),
                    FOREIGN KEY (Team2Id) REFERENCES Teams(TeamId)
                )";
            ExecuteQuery(connection, createMatchesTableQuery);
        }
    }

    static void ExecuteQuery(SqlConnection connection, string query)
    {
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }
}
