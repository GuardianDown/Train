using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Train.Results
{
    public class LoadButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;

        [SerializeField]
        private string _loadSceneName = string.Empty;

        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe() => _button.onClick.AddListener(LoadScene);

        private void Unsubscribe() => _button.onClick.RemoveListener(LoadScene);

        private void LoadScene() => SceneManager.LoadSceneAsync(_loadSceneName);
    }
}
