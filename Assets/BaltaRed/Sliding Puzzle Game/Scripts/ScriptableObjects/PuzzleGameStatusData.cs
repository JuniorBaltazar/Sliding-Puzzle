using UnityEngine;
using BaltaRed.ExtensionMethods;

namespace BaltaRed.SlidingPuzzle.SaveGame
{
    [CreateAssetMenu(fileName = "Game Status Data", menuName = "Sliding Puzzle/Core/Create Puzzle Game Status")]
    public sealed class PuzzleGameStatusData : ScriptableObject
    {
        [SerializeField] private string _folder = "Assets/Saved Game";
        [SerializeField] private string _fileName = "Puzzle Status Game";

        public void SaveGame(PuzzleGameStatus puzzleStatus) => puzzleStatus.JsonSerialize(_folder, _fileName);

        public void LoadGame(out PuzzleGameStatus puzzleStatus)
        {
            puzzleStatus = new PuzzleGameStatus();
            puzzleStatus.JsonDeserialize(out puzzleStatus, _folder, _fileName);
        }
    }
}