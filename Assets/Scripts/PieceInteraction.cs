using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PieceInteraction : MonoBehaviour
{
    [SerializeField] private PieceInteractionData _pieceInteractionData;

    private BoxCollider _boxCollider;

    public BoxCollider BoxCollider => _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        _pieceInteractionData.HandlerPieceClicked(this);
    }
}
