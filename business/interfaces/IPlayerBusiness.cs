using chessAPI.models.player;

namespace chessAPI.business.interfaces;

public interface IPlayerBusiness<TI> 
    where TI : struct, IEquatable<TI>
{
    Task<clsPlayer<TI>> addPlayer(clsNewPlayer newPlayer);
    Task<clsPlayer<TI>> getPlayer(int idPlayer);
    Task<clsPlayer<TI>> updatePlayer(clsPlayer<TI> newPlayer);
}