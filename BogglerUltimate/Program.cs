// See https://aka.ms/new-console-template for more information

using BogglerUltimate;
using BogglerUltimate.Boggle;
using BogglerUltimate.Iterators;
using BogglerUltimate.Trie;

BoggleBoard board = BoggleBoard.MakeBoard();

foreach (Position pos in board.GetBoardPositions())
{
    BoardIterator boardIterator = new BoardIterator(board, pos);

    BoggleDictionary boggleDictionary = BoggleDictionary.GetBoggleDictionary();
    BoggleTrie boggleTrie = BoggleTrie.CreateBoggleTrie(boggleDictionary.Words);

    TrieIterator trieIterator = new TrieIterator(boggleTrie);
    
    var finder = new WordFinder(boardIterator, trieIterator);
    HashSet<string> foundWords = finder.FindWords(pos);

    foreach (string word in foundWords)
    {
        Console.WriteLine(word);
    }
}
