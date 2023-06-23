using UnityEngine;

namespace BaltaRed.SlidingPuzzle.GameCore
{
    [CreateAssetMenu(fileName = "Puzzle Data", menuName = "Sliding Puzzle/Core/Create Puzzle Data")]
    public sealed class PuzzleData : ScriptableObject
    {
        public static event System.Action OnPieceClicked;
        public static event System.Action OnAllPiecesInCorrectPosition;

        [Header("General Settings")]
        [SerializeField] private GameObject _piecePrefab = null;
        [SerializeField] private float _pieceSeparation = 0.03f;

        [Header("Game Settings")]
        [SerializeField] private int _puzzleSize = 4;

        public GameObject PiecePrefab => _piecePrefab;
        public float PieceSeparation => _pieceSeparation;
        public int PuzzleSize => _puzzleSize;

        public void HandlerAllPiecesInCorrectPosition() => OnAllPiecesInCorrectPosition?.Invoke();
        public void HandlerPieceClicked() => OnPieceClicked?.Invoke();
    }
}