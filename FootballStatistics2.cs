using System;
using System.Data;
using System.Data.SqlClient;

class FootballStatistics2
{
    private string connectionString;

    public FootballStatistics2(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public void AddMatch(int team1Id, int team2Id, int goalsTeam1, int goalsTeam2, string scorer, DateTime matchDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Перевірка наявності команд та їх інформації
            if (!CheckTeamExists(connection, team1Id) || !CheckTeamExists(connection, team2Id))
            {
                Console.WriteLine("One or both teams do not exist. Unable to add the match.");
                return;
            }

            string insertMatchQuery = $@"
                INSERT INTO Matches (Team1Id, Team2Id, GoalsTeam1, GoalsTeam2, Scorer, MatchDate)
                VALUES ({team1Id}, {team2Id}, {goalsTeam1}, {goalsTeam2}, '{scorer}', '{matchDate.ToString("yyyy-MM-dd")}')";

            ExecuteQuery(connection, insertMatchQuery);
        }
    }

    public void UpdateMatch(int matchId, int team1Id, int team2Id, int goalsTeam1, int goalsTeam2, string scorer, DateTime matchDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Перевірка наявності матчу
            if (!CheckMatchExists(connection, matchId))
            {
                Console.WriteLine("Match does not exist. Unable to update.");
                return;
            }

            // Перевірка наявності команд та їх інформації
            if (!CheckTeamExists(connection, team1Id) || !CheckTeamExists(connection, team2Id))
            {
                Console.WriteLine("One or both teams do not exist. Unable to update the match.");
                return;
            }

            string updateMatchQuery = $@"
                UPDATE Matches
                SET Team1Id = {team1Id}, Team2Id = {team2Id}, GoalsTeam1 = {goalsTeam1},
                    GoalsTeam2 = {goalsTeam2}, Scorer = '{scorer}', MatchDate = '{matchDate.ToString("yyyy-MM-dd")}'
                WHERE MatchId = {matchId}";

            ExecuteQuery(connection, updateMatchQuery);
        }
    }

    public void DeleteMatch(int team1Id, int team2Id, DateTime matchDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Пошук матчу за командами та датою
            string findMatchQuery = $@"
                SELECT MatchId
                FROM Matches
                WHERE (Team1Id = {team1Id} AND Team2Id = {team2Id} OR Team1Id = {team2Id} AND Team2Id = {team1Id})
                    AND MatchDate = '{matchDate.ToString("yyyy-MM-dd")}'";

            int matchId = GetSingleIntResult(connection, findMatchQuery);

            // Перевірка наявності матчу
            if (matchId == 0)
            {
                Console.WriteLine("Match not found. Unable to delete.");
                return;
            }

            // Запит користувача для підтвердження видалення
            Console.Write("Do you really want to delete the match? (yes/no): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "yes")
            {
                string deleteMatchQuery = $"DELETE FROM Matches WHERE MatchId = {matchId}";
                ExecuteQuery(connection, deleteMatchQuery);
                Console.WriteLine("Match deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }
    }

    private bool CheckTeamExists(SqlConnection connection, int teamId)
    {
        string query = $"SELECT COUNT(*) FROM Teams WHERE TeamId = {teamId}";
        int teamCount = GetSingleIntResult(connection, query);

        return teamCount > 0;
    }

    private bool CheckMatchExists(SqlConnection connection, int matchId)
    {
        string query = $"SELECT COUNT(*) FROM Matches WHERE MatchId = {matchId}";
        int matchCount = GetSingleIntResult(connection, query);

        return matchCount > 0;
    }

    private int GetSingleIntResult(SqlConnection connection, string query)
    {
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            object result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
    }

    private void ExecuteQuery(SqlConnection connection, string query)
    {
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }
}
