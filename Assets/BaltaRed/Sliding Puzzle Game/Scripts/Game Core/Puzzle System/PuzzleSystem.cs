using UnityEngine;
using System.Collections.Generic;
using BaltaRed.SlidingPuzzle.SaveGame;
using BaltaRed.SlidingPuzzle.Customization;

namespace BaltaRed.SlidingPuzzle.GameCore
{
    public sealed partial class PuzzleSystem : MonoBehaviour
    {
        [SerializeField] private PuzzleData _puzzleData = null;
        [SerializeField] private PuzzleGameStatusData _puzzleStatusData;
        [SerializeField] private PieceCustomizatinoManagerData _pieceCustomizationManagerData = null;
        [SerializeField] private GameObject _puzzleTableObject = null;
        [SerializeField] private bool _randomize = true;

        private EmptyPiece _emptyPiece;
        private PuzzleGameStatus _puzzleStatus;
        private bool _isFInishGame = false;

        private List<Piece> _pieces = new List<Piece>();
        private List<int> _pieceValues = new List<int>();
        private List<bool> _piecesCorrectPosition = new List<bool>();
        private List<BoxCollider> _activePieces = new List<BoxCollider>();

        private PieceBase[,] _rowsCollumns;

        private Dictionary<PieceInteraction, Piece> _dictPieceInteractions = new Dictionary<PieceInteraction, Piece>();

        private int AmountOfPieces => (int)Mathf.Pow(_puzzleData.PuzzleSize, 2) - 1;

        private void OnEnable()
        {
            PieceInteractionData.OnPieceClicked += HandlerSetPiecePosition;
            PieceCustomizatinoManagerData.OnUpdateCustomizationManager += CustomizeAllPieces;
        }

        private void OnDisable()
        {
            PieceInteractionData.OnPieceClicked -= HandlerSetPiecePosition;
            PieceCustomizatinoManagerData.OnUpdateCustomizationManager -= CustomizeAllPieces;
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
                SavePuzzleStatus();
                return;
            }
            else
            {
                _puzzleStatus.initialLoadGame = true;
                SavePuzzleStatus();
            }

            EnablePieces();
        }

        private void SavePuzzleStatus() => _puzzleStatusData.SaveGame(_puzzleStatus);
    }
}