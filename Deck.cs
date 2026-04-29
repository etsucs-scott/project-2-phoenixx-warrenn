namespace WarGame.Core;

// represents a shuffled deck of 52 cards
public class Deck
{
    private readonly Stack<Card> _cards;
    private static readonly string[] Suits =
        { "Hearts", "Diamonds", "Clubs", "Spades" };

    public Deck()
    {
        var list = new List<Card>();

        // build standard 52-card deck
        foreach (var suit in Suits)
        {
            for (int rank = 2; rank <= 14; rank++)
            {
                list.Add(new Card(suit, rank));
            }
        }

        Shuffle(list);
        _cards = new Stack<Card>(list);
    }

    public bool HasCards => _cards.Count > 0;

    public Card Draw() => _cards.Pop();

    private static void Shuffle(List<Card> cards)
    {
        var rng = new Random();
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }
}