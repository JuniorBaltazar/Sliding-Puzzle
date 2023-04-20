using UnityEngine;
using ExtensionMethods;

namespace SlidingPuzzle.GameCore
{
    public sealed partial class PuzzleSystem
    {
        private void SetStatusGame()
        {
            _puzzleStatusData.LoadGame(out _puzzleStatus);

            bool isNewGame = _puzzleStatus.initialLoadGame == false || _puzzleData.PuzzleSize != _puzzleStatus.puzzleSize;

            if (isNewGame)
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
                        CreatePiece(row, collumn, i);

                        continue;
                    }

                    CreateEmptyPiece(i, row, collumn);
                }
            }

            //Local Methods

            void CreatePiece(int row, int collumn, int i)
            {
                GameObject pieceObj = Instantiate(_puzzleData.PiecePrefab, _puzzleTableObject.transform);
                Piece piece = pieceObj.GetComponent<Piece>();
                PieceInteraction pieceInteraction = piece.PieceInteraction;

                pieceInteraction.BoxCollider.enabled = false;

                piece.Index = _pieceValues[i];
                piece.SetRowCollumn(row, collumn);

                _rowsCollumns[row, collumn] = piece;

                _pieces.Add(piece);
                _dictPieceInteractions.Add(pieceInteraction, piece);

                SetPieceSize(piece);
                SetCorrectPosition(piece);
            }

            void CreateEmptyPiece(int emptyIndex, int row, int collumn)
            {
                Bounds bnds = _puzzleTableObject.transform.CalculateBounds();

                float rowSize = bnds.center.x - bnds.extents.x;
                float collumnSize = bnds.center.z - bnds.extents.z;
                float surface = bnds.center.y + bnds.extents.y;
                float size = _puzzleData.PuzzleSize;

                Vector3 piecePosition = new Vector3(rowSize + (row + 0.5f) / size, surface, collumnSize + (collumn + 0.5f) / size);

                _emptyPiece.piece.SetRowCollumn(row, collumn);
                _emptyPiece.piece.Index = emptyIndex;
                _emptyPiece.position = piecePosition;

                _puzzleStatus.indices[emptyIndex] = -1;
            }
        }

        private void LoadGame()
        {
            _rowsCollumns = new Piece[_puzzleStatus.puzzleSize, _puzzleStatus.puzzleSize];
            _pieceValues = _puzzleStatus.indices;

            for (int i = 0; i < _pieceValues.Count - 1; i++)
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
                    _pieceValues.Add(i);
                }
            }

            if (_randomize == true)
            {
                _pieceValues = RandomExtension.GenerateRandomNumbers(AmountOfPieces, 0, AmountOfPieces);
            }

            _pieceValues.Add(-1);
            _puzzleStatus.indices = _pieceValues;
        }
    }
}