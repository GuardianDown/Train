using UnityEngine;
using UnityEngine.UI;

namespace Train.Breaking
{
    public class ButtonFixView : AbstractFixView
    {
        [SerializeField]
        private Button _button = null;

        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe() => _button.onClick.AddListener(Fix);

        private void Unsubscribe() => _button.onClick.RemoveListener(Fix);
    }
}
