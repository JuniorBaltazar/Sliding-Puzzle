using System;
using System.Collections.Generic;

namespace BaltaRed.SlidingPuzzle.SaveGame
{
    [Serializable]
    public class PuzzleGameStatus
    {
        public bool initialLoadGame = false;
        public int puzzleSize = 0;
        public List<int> indices;
    }
}