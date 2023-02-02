namespace chessAPI.models.game;

public sealed class clsNewGame
{
    public clsNewGame(DateTime started, int whites, int blacks)
    {
        this.started = started;
        this.whites = whites;
        this.blacks = blacks;
    }

    public DateTime started { get; set; }
    public int whites { get; set; }
    public int blacks { get; set; }
}