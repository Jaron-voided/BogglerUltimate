// See https://aka.ms/new-console-template for more information

using BogglerUltimate;
using BogglerUltimate.Boggle;

BoggleBoard board = BoggleBoard.MakeBoard();

foreach (Position pos in board.GetBoardPositions())
{
    
    var finder = new WordFinder();
}
