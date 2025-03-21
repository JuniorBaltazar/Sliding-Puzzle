﻿using TMPro;
using UnityEngine;

namespace BaltaRed.SlidingPuzzle.GameCore
{
    public sealed class Piece : PieceBase
    {
        [SerializeField] private TextMeshPro _pieceText = null;
        [SerializeField] private PieceInteraction _pieceInteraction = null;

        public PieceInteraction PieceInteraction => _pieceInteraction;
        public TextMeshPro PieceText => _pieceText;

        public override int Index
        {
            get => _index;
            set
            {
                _index = value;
                _pieceText.text = _index.ToString();
            }
        }
    }
}