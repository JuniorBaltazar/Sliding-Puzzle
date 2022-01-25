using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Piece Customization", menuName = "Sliding Puzzle/Piece/Create Piece Customization")]
public class PieceCustomizationData : ScriptableObject
{
    public TMP_FontAsset fontAsset = null;
    public Color fontColor = Color.white;
    public Color backgroundColor = Color.white;

    public void SetPieceCustomizations(TMP_Text text, MeshRenderer renderer)
    {
        text.font = fontAsset;
        text.color = fontColor;
        renderer.material.color = backgroundColor;
    }
}
