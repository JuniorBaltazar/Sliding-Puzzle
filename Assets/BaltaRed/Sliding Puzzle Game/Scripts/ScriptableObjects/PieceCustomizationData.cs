using TMPro;
using UnityEngine;

namespace BaltaRed.SlidingPuzzle.Customization
{
    [CreateAssetMenu(fileName = "Piece Customization", menuName = "Sliding Puzzle/Piece/Create Piece Customization")]
    public sealed class PieceCustomizationData : ScriptableObject
    {
        public System.Action OnUpdateCustomization;

        [SerializeField] private TMP_FontAsset _fontAsset = null;
        [SerializeField] private Color _fontColor = Color.white;
        [SerializeField] private Color _backgroundColor = Color.white;

        private void OnValidate()
        {
            OnUpdateCustomization?.Invoke();
        }

        public void SetPieceCustomizations(TMP_Text text, MeshRenderer renderer)
        {
            text.font = _fontAsset;
            text.color = _fontColor;
            renderer.material.color = _backgroundColor;
        }
    }
}