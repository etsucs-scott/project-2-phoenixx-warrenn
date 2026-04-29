namespace WarGame.Core;


// controls the War game flow and rules
public class WarGameEngine
{
    private const int RoundLimit = 10_000;

    public PlayerHands Players { get; } = new();
    public PlayedCards Played { get; } = new();
    public List<Card> Pot { get; } = new();

    public int RoundNumber { get; private set; }

    // =============================
    // SETUP
    // =============================
    public void InitializePlayers(IEnumerable<string> names)
    {
        foreach (var name in names)
            Players.Hands[name] = new Hand();

        DealCards();
    }

    private void DealCards()
    {
        var deck = new Deck();
        var playerList = Players.Hands.Keys.ToList();

        int index = 0;
        while (deck.HasCards)
        {
            var card = deck.Draw();
            Players.Hands[playerList[index]]
                .AddToBottom(new[] { card });

            index = (index + 1) % playerList.Count;
        }
    }

    // =============================
    // GAME LOOP
    // =============================
    public string? PlayGame(Action<string>? logger = null)
    {
        while (RoundNumber < RoundLimit &&
               Players.ActivePlayerCount > 1)
        {
            RoundNumber++;
            PlayRound(logger);
        }

        return DetermineWinner();
    }

    // =============================
    // ROUND LOGIC
    // =============================
    private void PlayRound(Action<string>? logger)
    {
        Played.Clear();

        // eliminate empty players
        var active = Players.ActivePlayers.ToList();

        foreach (var name in active)
        {
            var hand = Players.Hands[name];
            var card = hand.PlayTopCard();

            Played.Cards[name] = card;
            Pot.Add(card);
        }

        ResolveBattle(active, logger);
    }

    private void ResolveBattle(List<string> contenders,
        Action<string>? logger)
    {
        while (true)
        {
            int maxRank = contenders
                .Max(p => Played.Cards[p].Rank);

            var winners = contenders
                .Where(p => Played.Cards[p].Rank == maxRank)
                .ToList();

            if (winners.Count == 1)
            {
                AwardPot(winners[0], logger);
                return;
            }

            logger?.Invoke("Tie detected — tiebreaker!");

            // tiebreaker round
            contenders = winners;

            foreach (var player in contenders.ToList())
            {
                var hand = Players.Hands[player];

                if (hand.Count == 0)
                {
                    contenders.Remove(player);
                    continue;
                }

                var card = hand.PlayTopCard();
                Played.Cards[player] = card;
                Pot.Add(card);
            }

            if (contenders.Count == 1)
            {
                AwardPot(contenders[0], logger);
                return;
            }
        }
    }

    private void AwardPot(string winner, Action<string>? logger)
    {
        Players.Hands[winner].AddToBottom(Pot);
        logger?.Invoke($"{winner} wins the round and takes {Pot.Count} cards.");
        Pot.Clear();
    }

    private string? DetermineWinner()
    {
        var standings = Players.Hands
            .Select(p => (Name: p.Key, Count: p.Value.Count))
            .OrderByDescending(p => p.Count)
            .ToList();

        if (standings.Count == 0) return null;

        if (standings[0].Count ==
            standings.Skip(1).FirstOrDefault().Count)
            return "Draw";

        return standings[0].Name;
    }
}