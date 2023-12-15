using System;
using System.Data;
using System.Data.SqlClient;

class FootballStatistics3
{
    private string connectionString;

    public FootballStatistics3(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void ShowTopScorersForTeam(int teamId, int topCount)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP {topCount} Players.FullName, COUNT(Matches.Scorer) AS Goals
                FROM Players
                LEFT JOIN Matches ON Players.PlayerId = Matches.Scorer
                WHERE Players.TeamId = {teamId}
                GROUP BY Players.FullName
                ORDER BY Goals DESC";

            DisplayQueryResults(connection, query, $"Top {topCount} Scorers for Team {teamId}");
        }
    }

    public void ShowTopScorersOverall(int topCount)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP {topCount} Players.FullName, COUNT(Matches.Scorer) AS Goals
                FROM Players
                LEFT JOIN Matches ON Players.PlayerId = Matches.Scorer
                GROUP BY Players.FullName
                ORDER BY Goals DESC";

            DisplayQueryResults(connection, query, $"Top {topCount} Scorers Overall");
        }
    }

    public void ShowBestScorerForTeam(int teamId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT Players.FullName, COUNT(Matches.Scorer) AS Goals
                FROM Players
                LEFT JOIN Matches ON Players.PlayerId = Matches.Scorer
                WHERE Players.TeamId = {teamId}
                GROUP BY Players.FullName
                ORDER BY Goals DESC
                OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";

            DisplayQueryResults(connection, query, $"Best Scorer for Team {teamId}");
        }
    }

    public void ShowBestScorerOverall()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT Players.FullName, COUNT(Matches.Scorer) AS Goals
                FROM Players
                LEFT JOIN Matches ON Players.PlayerId = Matches.Scorer
                GROUP BY Players.FullName
                ORDER BY Goals DESC
                OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";

            DisplayQueryResults(connection, query, "Best Scorer Overall");
        }
    }
  
  private void DisplayQueryResults(SqlConnection connection, string query, string resultTitle)
    {

    }
}
