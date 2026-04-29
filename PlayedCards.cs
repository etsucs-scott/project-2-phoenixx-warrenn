namespace WarGame.Core;

// stores cards played in the current round
public class PlayedCards
{
    public Dictionary<string, Card> Cards { get; } = new();

    public void Clear() => Cards.Clear();
}