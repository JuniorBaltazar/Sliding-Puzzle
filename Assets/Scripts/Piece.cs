using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class Piece : PieceBase
{
    [SerializeField] private TextMeshPro _label = null;
    [SerializeField] private PieceInteraction _pieceInteraction = null;

    public override int Index {
        get => _index;
        set 
        {
            _index = value;
            _label.text = _index.ToString();
        }
    }

    public PieceInteraction PieceInteraction => _pieceInteraction;
}