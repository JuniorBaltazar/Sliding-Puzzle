using System.Collections.Generic;

public sealed class Board
{
    public static int Empty = -1;

    private int size;
    private int[] spaces;

    public Board(int size)
    {
        this.size = size;
        spaces = new int[size * size];
        for (int i = 0; i < spaces.Length; i++)
        {
            spaces[i] = Empty;
        }
    }

    public Board(Board board)
    {
        size = board.size;
        spaces = new int[size * size];

        for (int i = 0; i < spaces.Length; i++)
        {
            spaces[i] = board.spaces[i];
        }
    }

    public Board Clone() { return new Board(this); }

    public void Set(int row, int col, int value)
    {
        spaces[row * size + col] = value;
    }

    public int Get(int row, int col)
    {
        return spaces[row * size + col];
    }

    public int Size { get { return size; } }

    public bool IsSolved
    {
        get
        {
            int expected = 0;
            for (int i = 0; i < spaces.Length; i++)
            {
                if (spaces[i] == expected)
                {
                    // advance expected token to next, or Empty since in winning state the final space is empty
                    expected = i == spaces.Length - 2 ? Board.Empty : i + 1;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// Get the values of the tiles which can be played in the current board state
    /// </summary>
    /// <value>Array of tile values which can be legally played</value>
    public int[] AvailableMoves
    {
        get
        {
            // find the empty space, and sum up the space left/right/up/down from it which can be played
            int eRow = -1, eCol = -1;
            bool found = false;
            for (int row = 0; row < size && !found; row++)
            {
                for (int col = 0; col < size && !found; col++)
                {
                    if (spaces[row * size + col] == Empty)
                    {
                        eRow = row;
                        eCol = col;
                        found = true;
                    }
                }
            }

            List<int> moves = new List<int>();
            if (eRow > 0)
            {
                moves.Add(Get(eRow - 1, eCol));
            }
            if (eRow < size - 1)
            {
                moves.Add(Get(eRow + 1, eCol));
            }
            if (eCol > 0)
            {
                moves.Add(Get(eRow, eCol - 1));
            }
            if (eCol < size - 1)
            {
                moves.Add(Get(eRow, eCol + 1));
            }

            return moves.ToArray();
        }
    }

    internal void Swap(int srcRow, int srcCol, int dstRow, int dstCol)
    {
        int srcOffset = srcRow * size + srcCol;
        int dstOffset = dstRow * size + dstCol;
        int t = spaces[dstOffset];
        spaces[dstOffset] = spaces[srcOffset];
        spaces[srcOffset] = t;
    }

    public override string ToString()
    {
        string[] rows = new string[size];
        for (int row = 0; row < size; row++)
        {
            string rowStr = "[";
            for (int col = 0; col < size; col++)
            {
                int v = Get(row, col);
                rowStr += v != Board.Empty ? v.ToString() : " ";
                if (col < size - 1) { rowStr += ", "; }
            }
            rowStr += "]";
            rows[row] = rowStr;
        }

        return string.Join(", ", rows) + " (Solved: " + IsSolved + ") (Plays: " + string.Join(", ", AvailableMoves) + ")";
    }

    /// <summary>
    /// Play the tile with a specific value (tiles have value from
    /// </summary>
    /// <param name="value">The value of the tile to play</param>
    /// <returns>A new board representing the next board state, or null if value was not on the board, or the play was not legal</returns>
    public Board Play(int value)
    {
        int fRow = -1, fCol = -1;
        bool found = false;
        for (int row = 0; row < size && !found; row++)
        {
            for (int col = 0; col < size && !found; col++)
            {
                if (spaces[row * size + col] == value)
                {
                    fRow = row;
                    fCol = col;
                    found = true;
                }
            }
        }

        return found ? Play(fRow, fCol) : null;
    }


    /// <summary>
    /// Play the move on the tile at (row,col). 
    /// </summary>
    /// <param name="row">Row of the tile to play</param>
    /// <param name="col">Col of the tile to play</param>
    /// <returns>If a legal move, returns a new Board representing the new play state, else, null</returns>
    public Board Play(int row, int col)
    {
        Board result = null;

        // check to left
        if (row > 0 && Get(row - 1, col) == Board.Empty)
        {
            result = Clone();
            result.Swap(row, col, row - 1, col);
        }

        // check to right
        if (row < size - 1 && Get(row + 1, col) == Board.Empty)
        {
            result = Clone();
            result.Swap(row, col, row + 1, col);
        }

        // check to up
        if (col > 0 && Get(row, col - 1) == Board.Empty)
        {
            result = Clone();
            result.Swap(row, col, row, col - 1);
        }

        // check to down
        if (col < size - 1 && Get(row, col + 1) == Board.Empty)
        {
            result = Clone();
            result.Swap(row, col, row, col + 1);
        }

        return result;
    }
}