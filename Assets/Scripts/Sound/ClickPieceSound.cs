using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
