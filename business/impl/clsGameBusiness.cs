using chessAPI.business.interfaces;
using chessAPI.dataAccess.repositores;
using chessAPI.models.game;

namespace chessAPI.business.impl;

public sealed class clsGameBusiness<TI, TC> : IGameBusiness<TI> 
    where TI : struct, IEquatable<TI>
    where TC : struct
{
    internal readonly IGameRepository<TI, TC> gameRepository;

    public clsGameBusiness(IGameRepository<TI, TC> gameRepository)
    {
        this.gameRepository = gameRepository;
    }

    public async Task<clsInfoGame<TI>> newGame(clsNewGame newGame)
    {
        var x = await gameRepository.addGame(newGame).ConfigureAwait(false);
        return new clsInfoGame<TI>(x, newGame.blacks, newGame.whites);
    }
    public async Task<clsSearchGame<TI>> setWinner(clsWinner winner)
    {
        var x = await gameRepository.updateWinner(winner).ConfigureAwait(false);
        return new clsSearchGame<TI>(winner.id, x, x);
    }

    public async Task<clsGame<TI>> startGame(clsGame<TI> newGame){
        var res = await gameRepository.updateGame(newGame).ConfigureAwait(false);
        return res == null ? null : 
        new clsGame<TI>(res.id, res.started, res.whites, res.blacks, res.turn, res.winner);
    }
    public async Task<clsSearchGame<TI>> getInfoGame(int idGame)
    {
        var x = await gameRepository.getInfoGame(idGame).ConfigureAwait(false);
        return new clsSearchGame<TI>(idGame, x, x);
    }
}