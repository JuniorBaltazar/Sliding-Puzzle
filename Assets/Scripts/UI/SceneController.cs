using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SlidingPuzzle.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class SceneController : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex = 0;

        private Button _button = null;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() => { SetScene(); });
        }

        private void SetScene()
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}