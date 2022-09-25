using PathCreation;
using System;
using Train.Bonuses;
using Train.Stations;
using Train.TrainMovement;
using Train.UI;
using UnityEngine;
using UnityEngine.UI;
using PathCreatorData = Train.TrainMovement.PathCreatorData;
using Cinemachine;
using Train.Cameras;
using Train.Timer;
using Train.Results;
using Train.Breaking;
using System.Collections.Generic;

namespace Train.Infrastucture
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private TrainData _trainData = null;

        [SerializeField]
        private PathCreatorData _pathCreatorData = null;

        [SerializeField]
        private StationData[] _stationsData = null;

        [SerializeField]
        private Canvas _canvasPrefab = null;

        [SerializeField]
        private TakeBonusButton _takeBonusButtonPrefab = null;

        [SerializeField]
        private Joystick _joystickPrefab = null;

        [SerializeField]
        private AbstractAmountOfBonusesView _bonusViewPrefab = null;

        [SerializeField]
        private CinemachineVirtualCameraBase[] _virtualCameraPrefabs = null;

        [SerializeField]
        private SetNextCameraButton _setNextCameraButtonPrefab = null;

        [SerializeField]
        private int _gameTime = 60;

        [SerializeField]
        private AbstractTimerView _timerViewPrefab = null;

        [SerializeField]
        private ResultView _resultViewPrefab = null;

        [SerializeField]
        private AbstractRepairProgressView _repairProgressViewPrefab = null;

        [SerializeField]
        private AbstractFixView _fixViewPrefab = null;

        [SerializeField]
        private BreakingData _breakingData = null;

        private PathCreator _pathCreator;
        private GameObject _trainView;
        private IPathFollower _pathFollower;
        private IStation[] _stations;
        private IActiveStationsQueue _activeStationQueue;
        private IDisposable _takeBonusButtonActivator;
        private TakeBonusButton _takeBonusButton;
        private Joystick _joystick;
        private IMovement _trainMovement;
        private Canvas _canvas;
        private AbstractAmountOfBonusesView _bonusView;
        private IBonusCounter _bonusCounter;
        private ICameraSwitcher _cameraSwitcher;
        private CinemachineVirtualCameraBase[] _cinemachineVirtialCameras;
        private SetNextCameraButton _setNextCameraButton;
        private BonusesData _bonusesData;
        private ITimer _timer;
        private AbstractTimerView _timerView;
        private IGameOver _gameOver;
        private IDisposable _resultViewInstaller;
        private IDisposable _repairViewActivator;
        private IRepair _repair;
        private IBreaking _breaking;
        private IDisposable _breakingMovement;
        private ISaver _saver;

        private void Awake()
        {
            _pathCreator = Instantiate(_pathCreatorData.PathCreatorPrefab, _pathCreatorData.SpawnPosition, Quaternion.identity);
            _trainView = Instantiate(_trainData.TrainViewPrefab);
            _canvas = Instantiate(_canvasPrefab);
            _takeBonusButton = Instantiate(_takeBonusButtonPrefab, _canvas.transform);
            _bonusView = Instantiate(_bonusViewPrefab, _canvas.transform);
            _stations = new IStation[_stationsData.Length];
            for(int i = 0; i < _stationsData.Length; ++i)
            {
                AbstractStationView stationView = Instantiate(_stationsData[i].StationViewPrefab);
                IStation station = new Station(stationView, _stationsData[i].ID, _stationsData[i].StationRadius, 
                    _pathCreator, _stationsData[i].PositionOnPath, _stationsData[i].AmountOfBonuses);
                _stations[i] = station;
            }
            _activeStationQueue = new ActiveStationsQueue(_stations);
            _pathFollower = new PathFollower(_pathCreator, _trainView.transform,
                _trainData.MaxSpeed, _trainData.Acceleration, _trainData.EndOfPathInstruction);
            _pathFollower.StartFollow();
            _takeBonusButtonActivator = new TakeBonusButtonActivator(_takeBonusButton.gameObject, _activeStationQueue, _pathFollower);
            _joystick = Instantiate(_joystickPrefab, _canvas.transform);
            _trainMovement = new Movement(_pathFollower, _joystick);
            _trainMovement.StartMovement();
            _bonusesData = new BonusesData();
            _bonusCounter = new BonusCounter(_activeStationQueue, _bonusesData);
            _bonusView.Construct(_bonusesData);
            _takeBonusButton.Construct(_bonusCounter);
            _cinemachineVirtialCameras = new CinemachineVirtualCameraBase[_virtualCameraPrefabs.Length];
            for (int i = 0; i < _virtualCameraPrefabs.Length; ++i)
            {
                CinemachineVirtualCameraBase virtualCamera = Instantiate(_virtualCameraPrefabs[i],
                    _virtualCameraPrefabs[i].transform.position,
                    _virtualCameraPrefabs[i].transform.rotation,
                    _pathFollower.FollowerView.transform);
                virtualCamera.Follow = _pathFollower.FollowerView;
                virtualCamera.LookAt = _pathFollower.FollowerView;
                _cinemachineVirtialCameras[i] = virtualCamera;
            }
            _cameraSwitcher = new CameraSwitcher(_cinemachineVirtialCameras, 0);
            _setNextCameraButton = Instantiate(_setNextCameraButtonPrefab, _canvas.transform);
            _setNextCameraButton.Construct(_cameraSwitcher);
            _timer = new CustomTimer(_gameTime);
            _timerView = Instantiate(_timerViewPrefab, _canvas.transform);
            _timerView.Construct(_timer);
            _timer.StartTimer();
            _gameOver = new GameOverByTime(_timer);
            _resultViewInstaller = new ResultViewInstaller(_gameOver, _resultViewPrefab, _bonusesData, _canvas);
            _breaking = new RandomBreaking(_breakingData.BreakingChanceInSecond, _breakingData.FixDeltas, _trainMovement);
            _repair = new Repair(_breaking, _breakingData.FullRepairValue, _trainMovement);
            _repairViewActivator = new RepairViewActivator(_breaking, _repair, _repairProgressViewPrefab, _fixViewPrefab, _canvas);
            _breaking.StartBreaking();
            _breakingMovement = new BreakingMovement(_breaking, _repair, _trainMovement, _pathFollower);
            List<ISaveData> saveData = new List<ISaveData>();
            saveData.Add(_bonusesData);
            _saver = new GameSaver(saveData);
        }

        private void OnDestroy()
        {
            _saver.Save();
            _pathFollower.Dispose();
            _activeStationQueue.Dispose();
            _takeBonusButtonActivator.Dispose();
            foreach (IStation station in _stations)
                station.Dispose();
            _trainMovement.Dispose();
            _timer.StopTimer();
            _gameOver.Dispose();
            _resultViewInstaller.Dispose();
            _breaking.Dispose();
            _repair.Dispose();
            _repairViewActivator.Dispose();
            _breakingMovement.Dispose();
        }
    }
}
