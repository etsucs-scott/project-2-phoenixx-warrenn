namespace WarGame.Core;

/// holds all player hands
public class PlayerHands
{
    public Dictionary<string, Hand> Hands { get; } = new();

    public IEnumerable<string> ActivePlayers =>
        Hands.Where(h => h.Value.Count > 0)
             .Select(h => h.Key);

    public int ActivePlayerCount =>
        ActivePlayers.Count();
}