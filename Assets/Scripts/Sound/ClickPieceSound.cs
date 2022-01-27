using UnityEngine;
using SlidingPuzzle.GameCore;

namespace SlidingPuzzle.Sounds
{
    public class ClickPieceSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _clickPieceSfx = null;

        private void OnEnable()
        {
            PuzzleData.OnPieceClicked += HandlerClickPiece;
        }

        private void OnDisable()
        {
            PuzzleData.OnPieceClicked -= HandlerClickPiece;
        }

        public void HandlerClickPiece()
        {
            _clickPieceSfx.Play();
        }
    }
}