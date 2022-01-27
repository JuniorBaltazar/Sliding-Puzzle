using System;
using System.Collections.Generic;

[Serializable]
public class PuzzleGameStatus
{
    public bool initialLoadGame = false;
    public int puzzleSize = 0;
    public List<int> indices;
}