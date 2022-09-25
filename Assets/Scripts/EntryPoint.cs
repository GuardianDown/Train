using PathCreation;
using System;
using Train.Bonuses;
using Train.Stations;
using Train.TrainMovement;
using UnityEngine;
using PathCreatorData = Train.TrainMovement.PathCreatorData;
using Cinemachine;
using Train.Cameras;
using Train.Timer;
using Train.Results;
using Train.Breaking;
using System.Collections.Generic;
using Train.Save;
using Train.GameOver;

namespace Train.Infrastucture
{
    public class EntryPoint : MonoBehaviour
    {
        #region Serialize fields
        #region Bonuses
        [SerializeField]
        private AbstractTakeBonusView _takeBonusButtonPrefab = null;

        [SerializeField]
        private AbstractAmountOfBonusesView _bonusViewPrefab = null;
        #endregion
        #region Train movement
        [SerializeField]
        private TrainData _trainData = null;

        [SerializeField]
        private PathCreatorData _pathCreatorData = null;

        [SerializeField]
        private Joystick _joystickPrefab = null;
        #endregion
        #region Stations
        [SerializeField]
        private StationData[] _stationsData = null;

        #endregion
        #region Cameras
        [SerializeField]
        private CinemachineVirtualCameraBase[] _virtualCameraPrefabs = null;

        [SerializeField]
        private SetNextCameraButton _setNextCameraButtonPrefab = null;
        #endregion
        #region Timer
        [SerializeField]
        private int _gameTime = 60;

        [SerializeField]
        private AbstractTimerView _timerViewPrefab = null;
        #endregion
        #region Results
        [SerializeField]
        private ResultView _resultViewPrefab = null;
        #endregion
        #region Breaking
        [SerializeField]
        private AbstractRepairProgressView _repairProgressViewPrefab = null;

        [SerializeField]
        private AbstractFixView _fixViewPrefab = null;

        [SerializeField]
        private BreakingData _breakingData = null;
        #endregion
        #region Canvas
        [SerializeField]
        private Canvas _canvasPrefab = null;
        #endregion
        #endregion

        #region Private fields
        #region Bonuses
        private AbstractTakeBonusView _takeBonusButton;
        private AbstractAmountOfBonusesView _bonusView;
        private IBonusCounter _bonusCounter;
        private BonusesData _bonusesData;
        private IDisposable _takeBonusButtonActivator;
        #endregion
        #region Train Movement
        private IPathFollower _pathFollower;
        private IMovement _trainMovement;
        private PathCreator _pathCreator;
        private GameObject _trainView;
        private Joystick _joystick;
        #endregion
        #region Stations
        private IStation[] _stations;
        private IActiveStationsQueue _activeStationQueue;
        #endregion
        #region Cameras
        private ICameraSwitcher _cameraSwitcher;
        private CinemachineVirtualCameraBase[] _cinemachineVirtialCameras;
        private SetNextCameraButton _setNextCameraButton;
        #endregion
        #region Timer
        private ITimer _timer;
        private AbstractTimerView _timerView;
        private IGameOver _gameOver;
        #endregion
        #region Results
        private IDisposable _resultViewInstaller;
        private IDisposable _repairViewActivator;
        #endregion
        #region Breaking
        private IRepair _repair;
        private IBreaking _breaking;
        private IDisposable _breakingMovement;
        #endregion
        #region Save
        private ISaver _saver;
        #endregion
        #region Canvas
        private Canvas _canvas;
        #endregion
        #endregion

        private void Awake()
        {
            InitializeCanvas();
            InitializeTrainMovement();
            InitializeStations();
            InitializeBonuses();
            InitializeCameras();
            InitializeTimer();
            InitializeResults();
            InitializeBreaking();
            InitializeSaving();
        }

        private void OnDestroy()
        {
            _pathFollower.Dispose();
            _trainMovement.Dispose();

            foreach (IStation station in _stations)
                station.Dispose();
            _activeStationQueue.Dispose();

            _takeBonusButtonActivator.Dispose();

            _timer.StopTimer();
            _gameOver.Dispose();

            _resultViewInstaller.Dispose();

            _breaking.Dispose();
            _repair.Dispose();
            _repairViewActivator.Dispose();
            _breakingMovement.Dispose();

            _saver.Save();
        }

        private void InitializeCanvas() => _canvas = Instantiate(_canvasPrefab);

        private void InitializeTrainMovement()
        {
            _pathCreator = Instantiate(_pathCreatorData.PathCreatorPrefab, _pathCreatorData.SpawnPosition, Quaternion.identity);
            _trainView = Instantiate(_trainData.TrainViewPrefab);
            _pathFollower = new PathFollower(_pathCreator, _trainView.transform,
                _trainData.MaxSpeed, _trainData.Acceleration, _trainData.EndOfPathInstruction);
            _joystick = Instantiate(_joystickPrefab, _canvas.transform);
            _trainMovement = new Movement(_pathFollower, _joystick);
            _pathFollower.StartFollow();
            _trainMovement.StartMovement();
        }

        private void InitializeStations()
        {
            _stations = new IStation[_stationsData.Length];
            for (int i = 0; i < _stationsData.Length; ++i)
            {
                AbstractStationView stationView = Instantiate(_stationsData[i].StationViewPrefab);
                IStation station = new Station(stationView, _stationsData[i].ID, _stationsData[i].StationRadius,
                    _pathCreator, _stationsData[i].PositionOnPath, _stationsData[i].AmountOfBonuses);
                _stations[i] = station;
            }
            _activeStationQueue = new ActiveStationsQueue(_stations);
        }

        private void InitializeBonuses()
        {
            _bonusView = Instantiate(_bonusViewPrefab, _canvas.transform);
            _takeBonusButton = Instantiate(_takeBonusButtonPrefab, _canvas.transform);
            _takeBonusButtonActivator = new TakeBonusButtonActivator(_takeBonusButton, _activeStationQueue, _pathFollower);
            _bonusesData = new BonusesData();
            _bonusCounter = new BonusCounter(_activeStationQueue, _bonusesData);
            _bonusView.Construct(_bonusesData);
            _takeBonusButton.Construct(_bonusCounter);
        }

        private void InitializeCameras()
        {
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
        }

        private void InitializeTimer()
        {
            _timer = new CustomTimer(_gameTime);
            _timerView = Instantiate(_timerViewPrefab, _canvas.transform);
            _timerView.Construct(_timer);
            _timer.StartTimer();
            _gameOver = new GameOverByTime(_timer);
        }

        private void InitializeResults() => 
            _resultViewInstaller = new ResultViewInstaller(_gameOver, _resultViewPrefab, _bonusesData, _canvas);

        private void InitializeBreaking()
        {
            _breaking = new RandomBreaking(_breakingData.BreakingChanceInSecond, _breakingData.FixDeltas);
            _repair = new Repair(_breaking, _breakingData.FullRepairValue);
            _repairViewActivator = new RepairViewActivator(_breaking, _repair, _repairProgressViewPrefab, _fixViewPrefab, _canvas);
            _breaking.StartBreaking();
            _breakingMovement = new BreakingMovement(_breaking, _repair, _trainMovement);
        }

        private void InitializeSaving()
        {
            List<ISaveData> saveData = new List<ISaveData>();
            saveData.Add(_bonusesData);
            _saver = new GameSaver(saveData);
        }
    }
}
