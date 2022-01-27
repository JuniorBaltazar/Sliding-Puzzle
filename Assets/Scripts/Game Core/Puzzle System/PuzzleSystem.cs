using System.Collections.Generic;
using UnityEngine;
using SlidingPuzzle.SaveGame;
using SlidingPuzzle.Customization;

namespace SlidingPuzzle.GameCore
{
    public sealed partial class PuzzleSystem : MonoBehaviour
    {
        [SerializeField] private PuzzleData _puzzleData = null;
        [SerializeField] private PuzzleGameStatusData _puzzleStatusData;
        [SerializeField] private PieceCustomizationData _pieceCustomizationData = null;
        [SerializeField] private GameObject _puzzleTableObject = null;
        [SerializeField] private bool _randomize = true;

        private bool _isFInishGame = false;
        private PuzzleGameStatus _puzzleStatus;
        private EmptyPiece _emptyPiece;

        private List<int> _pieceValues = new List<int>();
        private List<bool> _piecesCorrectPosition = new List<bool>();
        private List<BoxCollider> _activePieces = new List<BoxCollider>();
        private List<Piece> _pieces = new List<Piece>();

        private PieceBase[,] _rowsCollumns;

        private Dictionary<PieceInteraction, Piece> _dictPieceInteractions = new Dictionary<PieceInteraction, Piece>();

        private int AmountOfPieces => (int)Mathf.Pow(_puzzleData.PuzzleSize, 2) - 1;

        private void OnEnable()
        {
            PieceInteractionData.OnPieceClicked += HandlerSetPiecePosition;
        }

        private void OnDisable()
        {
            PieceInteractionData.OnPieceClicked -= HandlerSetPiecePosition;
        }

        private void Start()
        {
            _emptyPiece = new EmptyPiece();
            _pieceValues = new List<int>(AmountOfPieces);

            SetStatusGame();

            SetGameSettings();

            CustomizeAllPieces();

            if (_isFInishGame == true)
            {
                _puzzleStatus.initialLoadGame = false;
                _puzzleStatusData.SaveGame(_puzzleStatus);
                return;
            }
            else
            {
                _puzzleStatus.initialLoadGame = true;
                _puzzleStatusData.SaveGame(_puzzleStatus);
            }

            EnablePieces();
        }
    }
}