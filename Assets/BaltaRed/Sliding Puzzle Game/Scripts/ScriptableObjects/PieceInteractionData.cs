using UnityEngine;

namespace BaltaRed.SlidingPuzzle.GameCore
{
    [CreateAssetMenu(fileName = "Piece Interactions", menuName = "Sliding Puzzle/Piece/Create Piece Interaction")]
    public sealed class PieceInteractionData : ScriptableObject
    {
        public static event System.Action<PieceInteraction> OnPieceClicked;

        public void HandlerPieceClicked(PieceInteraction pieceInteraction) => OnPieceClicked?.Invoke(pieceInteraction);
    }
}