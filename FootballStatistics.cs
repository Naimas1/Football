using System;
using System.Data;
using System.Data.SqlClient;

class FootballStatistics
{
    private string connectionString;

    public FootballStatistics(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void ShowGoalDifference()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = @"
                SELECT Teams.TeamName,
                       SUM(Matches.GoalsTeam1) - SUM(Matches.GoalsTeam2) AS GoalDifference
                FROM Teams
                JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName";

            DisplayQueryResults(connection, query, "Goal Difference");
        }
    }

    public void ShowFullMatchInfo()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Matches";

            DisplayQueryResults(connection, query, "Full Match Information");
        }
    }

    public void ShowMatchesOnDate(DateTime date)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string formattedDate = date.ToString("yyyy-MM-dd");
            string query = $"SELECT * FROM Matches WHERE MatchDate = '{formattedDate}'";

            DisplayQueryResults(connection, query, $"Matches on {formattedDate}");
        }
    }

    public void ShowTeamMatches(int teamId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT * FROM Matches WHERE Team1Id = {teamId} OR Team2Id = {teamId}";

            DisplayQueryResults(connection, query, $"Matches for Team {teamId}");
        }
    }

    public void ShowGoalScorersOnDate(DateTime date)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string formattedDate = date.ToString("yyyy-MM-dd");
            string query = $@"
                SELECT Players.FullName, Matches.Scorer, Matches.MatchDate
                FROM Players
                JOIN Matches ON Players.PlayerId = Matches.Scorer
                WHERE Matches.MatchDate = '{formattedDate}'";

            DisplayQueryResults(connection, query, $"Goal Scorers on {formattedDate}");
        }
    }

    public void ShowAllPlayersWhoScoredOnDate(DateTime date)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string formattedDate = date.ToString("yyyy-MM-dd");
            string query = $@"
                SELECT Players.FullName, Matches.Scorer, Matches.MatchDate
                FROM Players
                JOIN Matches ON Players.PlayerId = Matches.Scorer
                WHERE Matches.MatchDate = '{formattedDate}'";

            DisplayQueryResults(connection, query, $"Players who scored on {formattedDate}");
        }
    }

    private void DisplayQueryResults(SqlConnection connection, string query, string resultTitle)
    {
        using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
        {
            DataTable dataTable = new DataTable(resultTitle);
            adapter.Fill(dataTable);

            Console.WriteLine($"--- {resultTitle} ---");
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    Console.Write($"{col.ColumnName}: {row[col]} | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
