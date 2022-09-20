using PathCreation;
using System;
using Train.Stations;
using Train.TrainMovement;
using Train.UI;
using UnityEngine;
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
        private TrainControlView _trainControlViewPrefab = null;

        [SerializeField]
        private Joystick joystick = null;

        private PathCreator _pathCreator;
        private GameObject _trainView;
        private IPathFollower _pathFollower;
        private IStation[] _stations;
        private IActiveStationsQueue _activeStationQueue;
        private IDisposable _takeBonusButtonActivator;
        private TrainControlView _trainControlView;
        private Joystick _joystick;
        private IMovement _trainMovement;

        private void Awake()
        {
            _pathCreator = Instantiate(_pathCreatorData.PathCreatorPrefab, _pathCreatorData.SpawnPosition, Quaternion.identity);
            _trainView = Instantiate(_trainData.TrainViewPrefab);
            _trainControlView = Instantiate(_trainControlViewPrefab);
            _stations = new IStation[_stationsData.Length];
            for(int i = 0; i < _stationsData.Length; ++i)
            {
                AbstractStationView stationView = Instantiate(_stationsData[i].StationViewPrefab);
                IStation station = new Station(stationView, _stationsData[i].ID, _stationsData[i].StationRadius, 
                    _pathCreator, _stationsData[i].PositionOnPath, _stationsData[i].AmountOfBonuses);
                _stations[i] = station;
            }
            _activeStationQueue = new ActiveStationsQueue(_stations);
            _takeBonusButtonActivator = new TakeBonusButtonActivator(_trainControlView.TakeBonusButton, _activeStationQueue);
            _pathFollower = new PathFollower(_pathCreator, _trainView.transform, 
                _trainData.MaxSpeed, _trainData.Acceleration, _trainData.EndOfPathInstruction);
            _pathFollower.StartFollow();
            _joystick = Instantiate(joystick, _trainControlView.transform);
            _trainMovement = new Movement(_pathFollower, _joystick);
            _trainMovement.StartMovement();
        }

        private void OnDestroy()
        {
            _pathFollower.Dispose();
            _activeStationQueue.Dispose();
            _takeBonusButtonActivator.Dispose();
            foreach (IStation station in _stations)
                station.Dispose();
            _trainMovement.Dispose();
        }
    }
}
