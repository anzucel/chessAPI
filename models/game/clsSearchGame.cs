namespace chessAPI.models.game;

public sealed class clsSearchGame<TI>
{
    public clsSearchGame(int id, TI whites, TI blacks)
    {
        this.id = id;
        this.whites = whites;
        this.blacks = blacks;
    }

    public int id { get; set; }
    
    public TI whites { get; set; }
    public TI blacks { get; set; }
}