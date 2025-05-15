
namespace BogglerUltimate.Boggle;

public class BoggleDice
{
    internal List<BoggleDie> Dice { get; set; } = new List<BoggleDie>();

    public BoggleDice() { }

    internal BoggleDice CreateDice()
    {
        BoggleDice dice = new BoggleDice();

        foreach (string[] face in DiceFaces.BoggleDiceFaces)
        {
            BoggleDie die = BoggleDie.CreateDie(face);
            Dice.Add(die);
        }
        
        return dice;
    }

    internal void RollDice()
    {
        foreach (BoggleDie die in Dice)
        {
            Random rng = new Random();
            die.Roll(rng);
        }
    }
}

internal struct DiceFaces
{
    internal static readonly List<string[]> BoggleDiceFaces = new()
    {
        new[] { "A", "A", "E", "E", "G", "N" },
        new[] { "E", "L", "R", "T", "T", "Y" },
        new[] { "A", "O", "O", "T", "T", "W" },
        new[] { "A", "B", "B", "J", "O", "O" },
        new[] { "E", "H", "R", "T", "V", "W" },
        new[] { "C", "I", "M", "O", "T", "U" },
        new[] { "D", "I", "S", "T", "T", "Y" },
        new[] { "E", "I", "O", "S", "S", "T" },
        new[] { "D", "E", "L", "R", "V", "Y" },
        new[] { "A", "C", "H", "O", "P", "S" },
        new[] { "H", "I", "M", "N", "QU", "U" },
        new[] { "E", "E", "I", "N", "S", "U" },
        new[] { "E", "E", "G", "H", "N", "W" },
        new[] { "A", "F", "F", "K", "P", "S" },
        new[] { "H", "L", "N", "N", "R", "Z" },
        new[] { "D", "E", "I", "L", "R", "X" }
    };
}