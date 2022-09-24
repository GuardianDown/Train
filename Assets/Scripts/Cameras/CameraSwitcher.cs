using UnityEngine;
using Cinemachine;

namespace Train.Cameras
{
    public class CameraSwitcher : ICameraSwitcher
    {
        private const int ActiveCameraPriority = 1;
        private const int UnactiveCameraPriority = 0;

        private CinemachineVirtualCameraBase[] _virtualCameras = null;
        private int _currentCameraIndex;

        public CameraSwitcher(CinemachineVirtualCameraBase[] virtualCameras, int defaultCameraIndex)
        {
            _virtualCameras = virtualCameras;
            _currentCameraIndex = defaultCameraIndex;
            SwitchToNextCamera();
        }

        public void SwitchToNextCamera()
        {
            _virtualCameras[_currentCameraIndex].Priority = UnactiveCameraPriority;
            ++_currentCameraIndex;
            if (_currentCameraIndex >= _virtualCameras.Length)
                _currentCameraIndex = 0;
            _virtualCameras[_currentCameraIndex].Priority = ActiveCameraPriority;
        }
    }
}
