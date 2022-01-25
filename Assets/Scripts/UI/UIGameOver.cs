using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        PuzzleData.OnAllPiecesInCorrectPosition += ActiveGameOverPanel;
    }

    private void OnDisable()
    {
        PuzzleData.OnAllPiecesInCorrectPosition -= ActiveGameOverPanel;
    }

    public void ActiveGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }
}
