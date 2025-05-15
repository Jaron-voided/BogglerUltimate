using BogglerUltimate.Boggle;
using BogglerUltimate.Trie;

namespace BogglerUltimate.Iterators;

public class BoardIterator
{
    private readonly BoggleBoard _board;
    private readonly VisitHistory _visitHistory;
    private Position _current;
    
    internal int Depth { get; set; }
    internal char[] WordBuffer = new char[16];
    
    // Should this be a factory
    public BoardIterator(BoggleBoard board, Position start)
    {
        _board = board;
        _visitHistory = new VisitHistory();
        _current = start;
        Depth = 0;
    }

    internal void AddLetter(BoggleDie die)
    {
        bool isQu = die.SelectedFace[0] == 'Q';

        if (!isQu)
        {
            WordBuffer[Depth] = die.SelectedFace[0];
        }
        else
        {
            WordBuffer[Depth] = 'Q';
            Depth++;
            WordBuffer[Depth] = 'U';
        }
    }

    internal void MoveSpot(Direction direction)
    {
        _current.Move(direction);
    }

    // Pass the position of adjacent spots to see if its a valid path
    internal bool CheckSpot(TrieIterator trieIterator, Position position)
    {
        string faceString = _board.Board[position.X, position.Y].SelectedFace;
        char face = faceString[0];

        return trieIterator._current.HasChild(face);
    }

    internal HashSet<string> ExplorePath(BoggleTrie trie)
    {
        TrieIterator trieIterator = new TrieIterator(trie);
        HashSet<string> foundWords = new HashSet<string>();
        _visitHistory.Visit(_current);
        
        AddLetter(_board.Board[_current.X, _current.Y]);
        Depth++;
        
        var directions = Directions.EnumerateDirections(
            _current.X, _current.Y, _visitHistory.ToBoolArray());

        foreach (var direction in directions)
        {
            Position newPosition = _current.CheckMove(direction);
            if (CheckSpot(trieIterator, newPosition))
            {
                _current.Move(direction);
                trieIterator.Traverse(_board.Board[_current.X, _current.Y].SelectedFace[0]);
                // More logic here later
            }
            ExplorePathInternal(trieIterator, foundWords, _current, Depth);
        }
        
        return foundWords;
    }

    internal HashSet<string> ExplorePathInternal(TrieIterator trieIterator, HashSet<string> foundWords,
        Position current, int depth)
    {
        throw new NotImplementedException();
    }
    
}