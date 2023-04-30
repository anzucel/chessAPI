using chessAPI.dataAccess.models;
using chessAPI.models.game;

namespace chessAPI.dataAccess.repositores;

public interface IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    Task<TI> getInfoGame(int idGame);
    Task<TI> addGame(clsNewGame newGame);
    Task<TI> updateWinner(clsWinner player);
    Task<clsGameEntityModel<TI, TC>> updateGame(clsGame<TI> gameToUpdate);
}