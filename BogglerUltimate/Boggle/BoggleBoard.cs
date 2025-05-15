using System.Text;

namespace BogglerUltimate.Boggle;

public class BoggleBoard
{
    private const int BoardSize = 4;
    internal BoggleDie[,] Board = new BoggleDie[BoardSize, BoardSize];
    
    private readonly BoggleDice _boggleDice;

    private BoggleBoard(BoggleDice boggleDice)
    {
        _boggleDice = boggleDice;
    }
    
    internal static BoggleBoard MakeBoard()
    {
        BoggleDice boggleDice = new BoggleDice();
        BoggleBoard newBoard =  new BoggleBoard(boggleDice);
        boggleDice.CreateDice();
        var random = new Random();
        
        Random rng = new Random();
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                var index = random.Next(boggleDice.Dice.Count);
                BoggleDie die = boggleDice.Dice[index];
                die.Roll(rng);
                newBoard.Board[i, j] = die;
                boggleDice.Dice.RemoveAt(index);
            }
        }
        return newBoard;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                sb.Append(Board[i, j].ToString().PadRight(3));
            }

            sb.AppendLine();
        }
        return sb.ToString();
    }
}

internal class Directions
{
    private const int BoardSize = 4;

    static readonly Direction[] All =
    [
        new Direction { DX = -1, DY = 1 },   // Up Left
        new Direction { DX = 0, DY = 1 },    // Up
        new Direction { DX = 1, DY = 1 },    // Up Right
        new Direction { DX = -1, DY = 0 },   // Left
        new Direction { DX = 1, DY = 0 },    // Right
        new Direction { DX = -1, DY = -1 },  // Down Left
        new Direction { DX = 0, DY = -1 },   // Down 
        new Direction { DX = 1, DY = -1 },   // Down Right
    ];

    internal bool[] visited = new bool[16];

    internal static IEnumerable<Direction> EnumerateDirections(int x, int y, bool[] visited)
    {
        for (int i = 0; i < All.Length; i++)
        {
            // Iterates over each spot in Directions All[]
            var d= All[i];
            
            // Takes your current spot and adds the direction X and Y to it
            int newX = x + d.DX;
            int newY = y + d.DY;
            
            // Ensures the direction is not off the board in any way
            if (newX < 0 || newX >= visited.Length || newY < 0 || newY >= visited.Length)
                continue;
            
            // Ensures the spot has not been visited yet
            if (visited[newY * 4 + newX])
                continue;
            
            // Returns the direction if its not off the board nor has been visited yet
            yield return d;
        }
    }

    int PositionToIndex(int x, int y)
    {
        return y * BoardSize + x;
    }

}

internal struct Direction
{
    internal int DX;
    internal int DY;
}

public struct Position
{
    internal int X { get; set; }
    internal int Y { get; set; }

    internal Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    internal Position CheckMove(Direction direction)
    {
        Position pos = new Position();
        pos.X = X + direction.DX;
        pos.Y = Y + direction.DY;

        return pos;
    }
    internal void Move(Direction direction)
    {
        X += direction.DX;
        Y += direction.DY;
    }
}

public struct VisitHistory
{
    private const int BoardSize = 4;
    private int History;

    internal void Visit(Position position/*int x, int y*/)
    {
        int index = position.Y * BoardSize + position.X;
        History |= (1 << index);
    }

    internal bool IsVisited(Position position)
    {
        int index = position.Y * BoardSize + position.X;
        return (History & (1 << index)) != 0;
    }
    
    internal bool[] ToBoolArray()
    {
        bool[] visited = new bool[BoardSize * BoardSize];
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = (History & (1 << i)) != 0;
        }
        return visited;
    }
}