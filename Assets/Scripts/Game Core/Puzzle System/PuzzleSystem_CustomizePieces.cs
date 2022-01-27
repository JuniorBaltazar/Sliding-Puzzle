using UnityEngine;

namespace SlidingPuzzle.GameCore
{
    public sealed partial class PuzzleSystem
    {
        private void CreateEmptyPiece(int emptyIndex, int row, int collumn)
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

        private void CreatePiece(Piece tile)
        {
            Bounds bnds = _puzzleTableObject.transform.CalculateBounds();

            float left = bnds.center.x - bnds.extents.x;
            float top = bnds.center.z - bnds.extents.z;
            float surface = bnds.center.y + bnds.extents.y;

            float size = _puzzleData.PuzzleSize;

            float scaledInset = _puzzleData.PieceSeparation / size;

            Vector3 piecePosition = new Vector3(left + (tile.Row + 0.5f) / size, surface, top + (tile.Collumn + 0.5f) / size);
            Vector3 pieceScale = new Vector3((1f / size) - (2 * scaledInset), 1, (1f / size) - (2 * scaledInset));

            tile.transform.position = piecePosition;
            tile.transform.localScale = pieceScale;
        }

        private void CustomizeAllPieces()
        {
            for (int i = 0; i < _pieces.Count; i++)
            {
                Piece piece = _pieces[i];
                _pieceCustomizationData.SetPieceCustomizations(piece.PieceText, piece.PieceInteraction.Renderer);
            }
        }
    }
}