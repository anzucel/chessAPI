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

    private const string _start = @"
    UPDATE public.game
	SET blacks=@BLACKS
    WHERE id=1 AND NOT EXISTS (
        SELECT a.player_id
        FROM team_player a
        WHERE team_id = @WHITES
        AND EXISTS (
            SELECT 1
            FROM team_player b
            WHERE team_id = @BLACKS AND a.player_id = b.player_id
        )
    )
	RETURNING id";

    public string SQLGetAll => _selectAll;

    public string SQLDataEntity => _selectOne;

    public string NewDataEntity => _add;

    public string DeleteDataEntity => _delete;

    // public string UpdateWholeEntity => _update;
    public string UpdateWholeEntity => _start;
}