using PathCreation;
using System;
using Train.Bonuses;
using Train.Stations;
using Train.TrainMovement;
using Train.UI;
using UnityEngine;
using UnityEngine.UI;
using PathCreatorData = Train.TrainMovement.PathCreatorData;

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
        private Button _takeBonusButtonPrefab = null;

        [SerializeField]
        private Joystick _joystickPrefab = null;

        [SerializeField]
        private Text _bonusViewPrefab = null;

        private PathCreator _pathCreator;
        private GameObject _trainView;
        private IPathFollower _pathFollower;
        private IStation[] _stations;
        private IActiveStationsQueue _activeStationQueue;
        private IDisposable _takeBonusButtonActivator;
        private Button _takeBonusButton;
        private Joystick _joystick;
        private IMovement _trainMovement;
        private Canvas _canvas;
        private Text _bonusView;
        private IDisposable _bonusCounter;

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
            _takeBonusButtonActivator = new TakeBonusButtonActivator(_takeBonusButton, _activeStationQueue, _pathFollower);
            _joystick = Instantiate(_joystickPrefab, _canvas.transform);
            _trainMovement = new Movement(_pathFollower, _joystick);
            _trainMovement.StartMovement();
            _bonusCounter = new BonusCounter(_takeBonusButton, _activeStationQueue, _bonusView);
        }

        private void OnDestroy()
        {
            _pathFollower.Dispose();
            _activeStationQueue.Dispose();
            _takeBonusButtonActivator.Dispose();
            foreach (IStation station in _stations)
                station.Dispose();
            _trainMovement.Dispose();
            _bonusCounter.Dispose();
        }
    }
}
