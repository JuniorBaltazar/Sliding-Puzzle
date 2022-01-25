using System;

[Serializable]
public class PuzzleGameStatus
{
    public bool initialLoadGame = false;
    public int puzzleSize = 0;
    public int[] indices;
}