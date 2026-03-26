namespace ContestSystem.API.DTOs;

public class LeaderboardDto
{
    public int Rank { get; set; }
    public string Name { get; set; } = "";
    public int Score { get; set; }
}