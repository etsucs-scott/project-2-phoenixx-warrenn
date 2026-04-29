namespace WarGame.Core;


// represents player's hand (queue behavior)

public class Hand
{
    private readonly Queue<Card> _cards = new();

    public int Count => _cards.Count;

    public void AddToBottom(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
            _cards.Enqueue(card);
    }

    public Card PlayTopCard()
    {
        return _cards.Dequeue();
    }
}