namespace chessAPI.models.game;

public sealed class clsInfoGame<TI>
{
    public clsInfoGame(TI id, int whites, int blacks)
    {
        this.id = id;
        this.whites = whites;
        this.blacks = blacks;
    }

    public TI id { get; set; }
    
    public int whites { get; set; }
    public int blacks { get; set; }
}