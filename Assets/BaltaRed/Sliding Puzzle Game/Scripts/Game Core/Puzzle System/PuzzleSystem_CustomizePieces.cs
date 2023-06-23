using UnityEngine;
using BaltaRed.ExtensionMethods;
using BaltaRed.SlidingPuzzle.Customization;

namespace BaltaRed.SlidingPuzzle.GameCore
{
    public sealed partial class PuzzleSystem
    {
        private void SetPieceSize(Piece piece)
        {
            Bounds bnds = _puzzleTableObject.transform.CalculateBounds();

            float left = bnds.center.x - bnds.extents.x;
            float top = bnds.center.z - bnds.extents.z;
            float surface = bnds.center.y + bnds.extents.y;

            float size = _puzzleData.PuzzleSize;

            float scaledInset = _puzzleData.PieceSeparation / size;

            Vector3 piecePosition = new Vector3(left + (piece.Row + 0.5f) / size, surface, top + (piece.Collumn + 0.5f) / size);
            Vector3 pieceScale = new Vector3((1f / size) - (2 * scaledInset), 1, (1f / size) - (2 * scaledInset));

            piece.transform.position = piecePosition;
            piece.transform.localScale = pieceScale;
        }

        [ContextMenu("Customize All Pieces")]
        private void CustomizeAllPieces()
        {
            PieceCustomizationData pieceCustomizationData = _pieceCustomizationManagerData.GetCurrentCustomizationData();

            foreach (Piece piece in _pieces)
            {
                pieceCustomizationData.SetPieceCustomizations(piece.PieceText, piece.PieceInteraction.Renderer);
            }
        }
    }
}