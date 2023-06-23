using UnityEngine;
using System.Collections.Generic;
using BaltaRed.Editor.AttributeExtensions;

namespace BaltaRed.SlidingPuzzle.Customization
{
    [CreateAssetMenu(fileName = "Piece Customization Manager", menuName = "Sliding Puzzle/Piece/Create Piece Customization Manager")]
    public sealed class PieceCustomizatinoManagerData : ScriptableObject
    {
        public static System.Action OnUpdateCustomizationManager;

        [MinMaxSlider(0, nameof(_valueMax))]
        [SerializeField] private int _value = 0;

        [SerializeField] private PieceCustomizationData[] _pieceCustomizationDatas = new PieceCustomizationData[0];

        [SerializeField, HideInInspector] private int _valueMax;

        private void OnEnable()
        {
            foreach (PieceCustomizationData pieceCustomizationData in _pieceCustomizationDatas)
            {
                pieceCustomizationData.OnUpdateCustomization += HandlerInvokeUpdateCustomizationManager;
            }
        }

        private void OnValidate()
        {
            _valueMax = Mathf.Max(0, _pieceCustomizationDatas.Length - 1);

            if (_value > _valueMax)
            {
                _value = _valueMax;
            }

            HandlerInvokeUpdateCustomizationManager();
        }

        public void HandlerInvokeUpdateCustomizationManager() => OnUpdateCustomizationManager?.Invoke();

        public PieceCustomizationData GetCurrentCustomizationData()
        {
            PieceCustomizationData pieceCustomizationData = _pieceCustomizationDatas[_value];

            return pieceCustomizationData;
        }
    }
}