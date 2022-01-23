using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle Interactions", menuName = "Sliding Puzzle/Puzzle/Create Puzzle Interactions")]
public class PieceInteractionData : ScriptableObject
{
    public delegate void EventHandler(PieceInteraction pieceInteraction);

    public static event EventHandler OnPieceClicked;

    public void HandlerPieceClicked(PieceInteraction pieceInteraction)
    {
        OnPieceClicked?.Invoke(pieceInteraction);
    }
}
