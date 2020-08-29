public class UserScore
{
    public string userName { get; set; }
    public int heightRecord { get; set; }
    public int gameScore { get; set; }

    public UserScore()
    {
        userName = "";
        heightRecord = 0;
        gameScore = 0;
    }
}
