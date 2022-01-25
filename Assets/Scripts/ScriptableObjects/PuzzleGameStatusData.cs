using UnityEngine;
using ExtensionMethods;

[CreateAssetMenu(fileName = "Game Status Data", menuName = "Sliding Puzzle/Core/Create Puzzle Game Status")]
public class PuzzleGameStatusData : ScriptableObject
{
    [SerializeField] private string _folder = "Assets/Saved Game";
    [SerializeField] private string _fileName = "Puzzle Status Game";

    private PuzzleGameStatus puzzleStatus;

    public PuzzleGameStatus PuzzleStatus => puzzleStatus;

    public void SaveGame()
    {
        puzzleStatus.JsonSerialize(_folder, _fileName);
    }

    public void LoadGame()
    {
        puzzleStatus.JsonDeserialize(out puzzleStatus, _folder, _fileName);
    }
}
