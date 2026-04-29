namespace WarGame.Core;

// represents a single playing card

public class Card : IComparable<Card>
{
    public string Suit { get; }
    public int Rank { get; } // 2–14 (Ace = 14)

    public Card(string suit, int rank)
    {
        Suit = suit;
        Rank = rank;
    }


    /// compares cards by rank (ace high)

    public int CompareTo(Card? other)
    {
        if (other == null) return 1;
        return Rank.CompareTo(other.Rank);
    }

    public override string ToString()
    {
        string rankStr = Rank switch
        {
            11 => "J",
            12 => "Q",
            13 => "K",
            14 => "A",
            _ => Rank.ToString()
        };

        return $"{rankStr} of {Suit}";
    }
}