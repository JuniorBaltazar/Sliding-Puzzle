using UnityEngine;

namespace SlidingPuzzle.GameCore
{
    public sealed partial class PuzzleSystem
    {
        private void SwapPositions(Piece piece)
        {
            Vector3 piecePosition = piece.transform.position;

            piece.transform.position = _emptyPiece.position;
            _emptyPiece.position = piecePosition;
        }

        private void SwapRowCollumn(Piece piece)
        {
            int pieceRow = piece.Row;
            int pieceCollumn = piece.Collumn;

            int emptyRow = _emptyPiece.piece.Row;
            int emptyCollumn = _emptyPiece.piece.Collumn;

            //Switch value between Piece and Empty Piece
            piece.SetRowCollumn(emptyRow, emptyCollumn);
            _rowsCollumns[emptyRow, emptyCollumn] = piece;

            _emptyPiece.piece.SetRowCollumn(pieceRow, pieceCollumn);
            _rowsCollumns[pieceRow, pieceCollumn] = null;

            //Save piece positions
            int pieceIndexPosition = IndexPiecePosition(emptyRow, emptyCollumn);
            _puzzleStatus.indices[pieceIndexPosition] = piece.Index;

            int emptyPieceIndexPosition = IndexPiecePosition(pieceRow, pieceCollumn);
            _puzzleStatus.indices[emptyPieceIndexPosition] = -1;
        }

        private int IndexPiecePosition(int row, int collumn)
        {
            return row * _puzzleData.PuzzleSize + collumn;
        }
    }
}