namespace chessAPI.models.game;

public sealed class clsTeamPlayer<TI>
    where TI : struct, IEquatable<TI>
{
    public clsTeamPlayer(TI id, TI team_id, TI player_id)
    {
        this.id = id;
        this.team_id = team_id;
        this.player_id = player_id;
    }

    public TI id { get; set; }
    public TI team_id { get; set; }
    public TI player_id { get; set; }
}