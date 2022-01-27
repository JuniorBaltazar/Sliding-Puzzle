using UnityEngine;

namespace SlidingPuzzle.GameCore
{
    [CreateAssetMenu(fileName = "Piece Interactions", menuName = "Sliding Puzzle/Piece/Create Piece Interaction")]
    public sealed class PieceInteractionData : ScriptableObject
    {
        public delegate void EventHandler(PieceInteraction pieceInteraction);

        public static event EventHandler OnPieceClicked;

        public void HandlerPieceClicked(PieceInteraction pieceInteraction)
        {
            OnPieceClicked?.Invoke(pieceInteraction);
        }
    }
}