using System;
using Train.Cameras;
using UnityEngine;
using UnityEngine.UI;

namespace Train.UI
{
    public class SetNextCameraButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;

        private ICameraSwitcher _cameraSwitcher;

        public void Construct(ICameraSwitcher cameraSwitcher) => _cameraSwitcher = cameraSwitcher;

        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe() => _button.onClick.AddListener(SetNextCamera);

        private void Unsubscribe() => _button.onClick.RemoveListener(SetNextCamera);

        private void SetNextCamera() => _cameraSwitcher.SwitchToNextCamera();
    }
}
