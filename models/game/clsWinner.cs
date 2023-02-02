namespace chessAPI.models.game;

public sealed class clsWinner
{
    public clsWinner(int id, int winner)
    {
        this.id = id;
        this.winner = winner;
    }

    public int id { get; set; }
    public int winner { get; set; }
}