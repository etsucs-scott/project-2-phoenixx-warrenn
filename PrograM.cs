using WarGame.Core;

Console.WriteLine("WAR GAME");

int playerCount = GetPlayerCount(args);

var names = Enumerable.Range(1, playerCount)
    .Select(i => $"Player {i}")
    .ToList();

var engine = new WarGameEngine();
engine.InitializePlayers(names);

string? winner = engine.PlayGame(Console.WriteLine);

Console.WriteLine();
Console.WriteLine($"GAME OVER — Winner: {winner}");


// =============================
// helpers
// =============================
int GetPlayerCount(string[] args)
{
    if (args.Length > 0 &&
        int.TryParse(args[0], out int fromArg) &&
        fromArg is >= 2 and <= 4)
        return fromArg;

    while (true)
    {
        Console.Write("Enter number of players (2–4): ");
        if (int.TryParse(Console.ReadLine(), out int input) &&
            input is >= 2 and <= 4)
            return input;
    }
}