using System;
using System.Data;
using System.Data.SqlClient;

class FootballStatistics4
{
    private string connectionString;

    public FootballStatistics4(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void ShowTopTeamsByGoalsScored(int topCount)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP {topCount} Teams.TeamName, SUM(Matches.GoalsTeam1 + Matches.GoalsTeam2) AS TotalGoals
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalGoals DESC";

            DisplayQueryResults(connection, query, $"Top {topCount} Teams by Goals Scored");
        }
    }

    public void ShowTopScoringTeam()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP 1 Teams.TeamName, SUM(Matches.GoalsTeam1 + Matches.GoalsTeam2) AS TotalGoals
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalGoals DESC";

            DisplayQueryResults(connection, query, "Top Scoring Team");
        }
    }

    public void ShowTopTeamsByGoalsConceded(int topCount)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP {topCount} Teams.TeamName, SUM(Matches.GoalsTeam1 + Matches.GoalsTeam2) AS TotalGoalsConceded
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalGoalsConceded ASC";

            DisplayQueryResults(connection, query, $"Top {topCount} Teams by Goals Conceded");
        }
    }

    public void ShowBestDefensiveTeam()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP 1 Teams.TeamName, SUM(Matches.GoalsTeam1 + Matches.GoalsTeam2) AS TotalGoalsConceded
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalGoalsConceded ASC";

            DisplayQueryResults(connection, query, "Best Defensive Team");
        }
    }

    private void DisplayQueryResults(SqlConnection connection, string query, string resultTitle)
    {
        // Так само як і раніше
    }
}
