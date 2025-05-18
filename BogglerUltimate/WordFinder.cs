using BogglerUltimate.Boggle;
using BogglerUltimate.Iterators;

namespace BogglerUltimate;

public class WordFinder
{
    private BoardIterator BoardIterator { get; init; }
    private TrieIterator TrieIterator { get; init; }
    private WordState Word { get; init; } = new WordState();

    public WordFinder(BoardIterator boardIterator, TrieIterator trieIterator)
    {
        BoardIterator = boardIterator;
        TrieIterator = trieIterator;
    }

    public HashSet<string> FindWords(Position pos)
    {
        VisitHistory visitHistory = new();
        Word.Clear();
        return FindWords(pos, visitHistory);
    }

    HashSet<string> FindWords(Position pos, VisitHistory visitHistory)
    {
        HashSet<string> foundWords = new HashSet<string>();
        
        // Marks current position as Visited
        visitHistory.Visit(pos);
        
        //Adds the current (first) letter to my _wordBuffer
        char letter = BoardIterator._board.Board[pos.X, pos.Y].SelectedFace[0];
        
        // increments depth while adding the letter to WordState._wordBuffer
        BoardIterator.Depth = Word.AddLetter(letter, BoardIterator.Depth);
        TrieIterator.Traverse(letter);

        if (TrieIterator.IsWord())
        {
            string newWord = Word.AddWord();
            foundWords.Add(newWord);
        }
            
            
        
        var directions = Directions.EnumerateDirections(
            pos, visitHistory);

        foreach (var direction in directions)
        {
            // Check to see if the position in the direction is viable
            Position possiblePosition = pos.PossibleMove(direction);

            if (!visitHistory.IsVisited(possiblePosition) && BoardIterator.CheckSpot(TrieIterator, possiblePosition))
            {
                BoardIterator.MoveSpot(direction);
                FindWords(BoardIterator._current, visitHistory);
            }
        }

        return foundWords;
    }

    class WordState
    {
        private char[] _wordBuffer = new char[17];

        internal int AddLetter(char letter, int depth)
        {
            bool isQ = letter == 'Q';
            if (letter != 'Q')
            {
                _wordBuffer[depth++] = letter;
            }
            else
            {
                _wordBuffer[depth++] = letter;
                _wordBuffer[depth++] = 'U';
            }

            return depth;
        }

        internal void Clear()
        {
            for (int i = 0; i < _wordBuffer.Length; i++)
                _wordBuffer[i] = '*';
        }

        internal string AddWord()
        {
            return _wordBuffer.ToString();
        }
    }
}