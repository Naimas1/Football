using System;
using System.Data;
using System.Data.SqlClient;

class FootballStatistics5
{
    private string connectionString;

    public FootballStatistics5(string connectionString)
    {
        this.connectionString = connectionString;
    }


    public void ShowTopTeamsByPoints(int topCount)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP {topCount} Teams.TeamName,
                    SUM(CASE WHEN Matches.GoalsTeam1 > Matches.GoalsTeam2 THEN 3
                             WHEN Matches.GoalsTeam1 = Matches.GoalsTeam2 THEN 1
                             ELSE 0 END) AS TotalPoints
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalPoints DESC";

            DisplayQueryResults(connection, query, $"Top {topCount} Teams by Points");
        }
    }

    public void ShowTopTeamByPoints()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP 1 Teams.TeamName,
                    SUM(CASE WHEN Matches.GoalsTeam1 > Matches.GoalsTeam2 THEN 3
                             WHEN Matches.GoalsTeam1 = Matches.GoalsTeam2 THEN 1
                             ELSE 0 END) AS TotalPoints
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalPoints DESC";

            DisplayQueryResults(connection, query, "Top Team by Points");
        }
    }

    public void ShowBottomTeamsByPoints(int bottomCount)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP {bottomCount} Teams.TeamName,
                    SUM(CASE WHEN Matches.GoalsTeam1 > Matches.GoalsTeam2 THEN 3
                             WHEN Matches.GoalsTeam1 = Matches.GoalsTeam2 THEN 1
                             ELSE 0 END) AS TotalPoints
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalPoints ASC";

            DisplayQueryResults(connection, query, $"Bottom {bottomCount} Teams by Points");
        }
    }

    public void ShowBottomTeamByPoints()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $@"
                SELECT TOP 1 Teams.TeamName,
                    SUM(CASE WHEN Matches.GoalsTeam1 > Matches.GoalsTeam2 THEN 3
                             WHEN Matches.GoalsTeam1 = Matches.GoalsTeam2 THEN 1
                             ELSE 0 END) AS TotalPoints
                FROM Teams
                LEFT JOIN Matches ON Teams.TeamId = Matches.Team1Id OR Teams.TeamId = Matches.Team2Id
                GROUP BY Teams.TeamName
                ORDER BY TotalPoints ASC";

            DisplayQueryResults(connection, query, "Bottom Team by Points");
        }
    }

    private void DisplayQueryResults(SqlConnection connection, string query, string resultTitle)
    {

    }
}
