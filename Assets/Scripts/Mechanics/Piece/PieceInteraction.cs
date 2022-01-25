using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public sealed class PieceInteraction : MonoBehaviour
{
    [SerializeField] private PieceInteractionData _pieceInteractionData;

    private BoxCollider _boxCollider;
    private MeshRenderer _renderer;

    public BoxCollider BoxCollider => _boxCollider;
    public MeshRenderer Renderer => _renderer;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        PuzzleData.OnAllPiecesInCorrectPosition += DesactiveInteraction;
    }

    private void OnDisable()
    {
        PuzzleData.OnAllPiecesInCorrectPosition -= DesactiveInteraction;
    }

    private void OnMouseDown()
    {
        _pieceInteractionData.HandlerPieceClicked(this);
    }

    public void DesactiveInteraction()
    {
        _boxCollider.enabled = false;
    }
}
