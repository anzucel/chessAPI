using chessAPI.dataAccess.common;
using chessAPI.dataAccess.interfaces;
using chessAPI.dataAccess.models;
using chessAPI.models.game;
using Dapper;

namespace chessAPI.dataAccess.repositores;

public sealed class clsGameRepository<TI, TC> : clsDataAccess<clsGameEntityModel<TI, TC>, TI, TC>, IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    public clsGameRepository(IRelationalContext<TC> rkm,
                               ISQLData queries,
                               ILogger<clsGameRepository<TI, TC>> logger) : base(rkm, queries, logger)
    {
    }

    public async Task<TI> addGame(clsNewGame newGame)
    {
        var p = new DynamicParameters();
        p.Add("STARTED", newGame.started);
        p.Add("BLACKS", newGame.blacks);
        p.Add("WHITES", newGame.whites);
        return await add<TI>(p).ConfigureAwait(false);
    }

    public async Task<TI> getInfoGame(int idGame)
    {
        var p = new DynamicParameters();
        p.Add("ID", idGame);
        return await get<TI>(p).ConfigureAwait(false);
    }

    public async Task<TI> updateWinner(clsWinner player)
    {
        var p = new DynamicParameters();
        p.Add("ID", player.id);
        p.Add("WINNER", player.winner);
        return await update<TI>(p).ConfigureAwait(false);
    }

    public async Task<clsGameEntityModel<TI, TC>> updateGame(clsGame<TI> game){
        var p = new DynamicParameters();
        p.Add("WHITES", game.blacks);
        p.Add("BLACKS", game.blacks);
        p.Add("ID", game.id);
        var res = await set<clsGameEntityModel<TI, TC>>(p, null, queries.UpdateWholeEntity, null)
        .ConfigureAwait(false);
        return res;
    }

    protected override DynamicParameters fieldsAsParams(clsGameEntityModel<TI, TC> entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        var p = new DynamicParameters();
        p.Add("ID", entity.id);
        return p;
    }

    protected override DynamicParameters keyAsParams(TI key)
    {
        var p = new DynamicParameters();
        p.Add("ID", key);
        return p;
    }
}