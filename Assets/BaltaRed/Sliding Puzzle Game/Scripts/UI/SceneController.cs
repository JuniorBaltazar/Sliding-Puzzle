using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BaltaRed.SlidingPuzzle.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class SceneController : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex = 0;

        private Button _button = null;

        private void Awake() => _button = GetComponent<Button>();

        private void Start()
        {
            _button.onClick.AddListener(() => { SetScene(); });

            // Local Methods
            void SetScene()
            {
                SceneManager.LoadScene(_sceneIndex);
            }
        }
    }
}