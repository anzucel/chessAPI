namespace chessAPI.dataAccess.queries.postgreSQL;

public sealed class qGame : IQGame
{
    private const string _selectAll = @"";
    private const string _selectOne = @"
    SELECT started, winner 
    FROM public.game
    WHERE id=@ID";
    private const string _add = @"
    INSERT INTO public.game(started, whites, blacks)
	VALUES (@EMAIL, @WHITES, @BLACKS) RETURNING id";
    private const string _delete = @"";
    private const string _update = @"
    UPDATE public.game
	SET winner=@WINNER
	WHERE id=@ID";

    public string SQLGetAll => _selectAll;

    public string SQLDataEntity => _selectOne;

    public string NewDataEntity => _add;

    public string DeleteDataEntity => _delete;

    public string UpdateWholeEntity => _update;
}