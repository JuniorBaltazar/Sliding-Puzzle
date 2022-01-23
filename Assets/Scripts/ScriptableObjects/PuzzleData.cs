using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle Data", menuName = "Sliding Puzzle/Puzzle/Create Puzzle Data")]
public sealed class PuzzleData : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField] private GameObject _piecePrefab = null;
    [SerializeField] private float _pieceSeparation = 0.03f;

    [Header("Game Settings")]
    [SerializeField] private int _puzzleSize = 4;

    public GameObject PiecePrefab => _piecePrefab;
    public float PieceSeparation => _pieceSeparation;
    public int PuzzleSize => _puzzleSize;
}