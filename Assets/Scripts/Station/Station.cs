using PathCreation;
using System;

namespace Train.Stations
{
    public class Station : IStation
    {
        private readonly AbstractStationView _stationView;
        private readonly string _id;
        private readonly int _amountOfBonuses;

        public string ID => _id;
        public int AmountOfBonuses => _amountOfBonuses;

        public event Action<string> onStationEnter;
        public event Action<string> onStationExit;

        public Station(AbstractStationView stationView, string stationID, float radius, 
            PathCreator pathCreator, float positionOnPath, int amountOfBonuses)
        {
            _stationView = stationView;
            _id = stationID;
            _amountOfBonuses = amountOfBonuses;
            _stationView.SetRadius(radius);
            _stationView.transform.position = pathCreator.path.GetPointAtDistance(positionOnPath);
            _stationView.transform.rotation = pathCreator.path.GetRotationAtDistance(positionOnPath);
            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void OnStationEnter() => onStationEnter?.Invoke(_id);

        private void OnStationExit() => onStationExit?.Invoke(_id);

        private void Subscribe()
        {
            _stationView.onStationEnter += OnStationEnter;
            _stationView.onStationExit += OnStationExit;
        }

        private void Unsubscribe()
        {
            _stationView.onStationEnter -= OnStationEnter;
            _stationView.onStationExit -= OnStationExit;
        }
    }
}
