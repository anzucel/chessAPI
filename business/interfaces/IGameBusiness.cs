using chessAPI.models.game;

namespace chessAPI.business.interfaces;

public interface IGameBusiness<TI> 
    where TI : struct, IEquatable<TI>
{
    Task<clsSearchGame<TI>> getInfoGame(int idGame);
    Task<clsInfoGame<TI>> newGame(clsNewGame newGame);
    Task<clsSearchGame<TI>> setWinner(clsWinner winner);
}