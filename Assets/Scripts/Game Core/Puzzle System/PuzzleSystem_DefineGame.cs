using UnityEngine;

public sealed partial class PuzzleSystem
{
    private void SetStatusGame()
    {
        _puzzleStatusData.LoadGame(out _puzzleStatus);

        if (_puzzleStatus.initialLoadGame == false)
        {
            NewGame();
        }
        else
        {
            LoadGame();
        }
    }

    private void SetGameSettings()
    {
        for (int row = 0; row < _puzzleData.PuzzleSize; row++)
        {
            for (int collumn = 0; collumn < _puzzleData.PuzzleSize; collumn++)
            {
                int i = IndexPiecePosition(row, collumn);

                bool newGame = i < AmountOfPieces;
                bool loadGame = _puzzleStatus.indices[i] != -1;

                bool isPiece = _puzzleStatus.initialLoadGame == false ? newGame : loadGame;

                if (isPiece)
                {
                    GameObject pieceObj = Instantiate(_puzzleData.PiecePrefab, _puzzleTableObject.transform);
                    Piece piece = pieceObj.GetComponent<Piece>();
                    PieceInteraction pieceInteraction = piece.PieceInteraction;

                    pieceInteraction.BoxCollider.enabled = false;

                    piece.Index = pieceValues[i];
                    piece.SetRowCollumn(row, collumn);

                    _rowsCollumns[row, collumn] = piece;

                    _pieces.Add(piece);
                    _dictPieceInteractions.Add(pieceInteraction, piece);

                    CreatePiece(piece);
                    SetCorrectPosition(piece);

                    continue;
                }

                CreateEmptyPiece(i, row, collumn);
            }
        }
    }

    private void LoadGame()
    {
        _rowsCollumns = new Piece[_puzzleStatus.puzzleSize, _puzzleStatus.puzzleSize];
        pieceValues = _puzzleStatus.indices;

        for (int i = 0; i < pieceValues.Count - 1; i++)
        {
            _piecesCorrectPosition.Add(false);
        }
    }

    private void NewGame()
    {
        _rowsCollumns = new Piece[_puzzleData.PuzzleSize, _puzzleData.PuzzleSize];
        _puzzleStatus.puzzleSize = _puzzleData.PuzzleSize;

        for (int i = 0; i < AmountOfPieces; i++)
        {
            _piecesCorrectPosition.Add(false);

            if (_randomize == false)
            {
                pieceValues.Add(i);
            }
        }

        if (_randomize == true)
        {
            pieceValues = RandomExtension.GenerateRandomNumbers(AmountOfPieces, 0, AmountOfPieces);
        }

        pieceValues.Add(-1);
        _puzzleStatus.indices = pieceValues;
    }
}
