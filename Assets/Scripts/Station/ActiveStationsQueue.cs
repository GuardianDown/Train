using System;
using System.Collections.Generic;

namespace Train.Stations
{
    public class ActiveStationsQueue : IActiveStationsQueue
    {
        private readonly Dictionary<string, IStation> _allStations;

        private List<IStation> _activeStations;

        public IReadOnlyCollection<IStation> ActiveStations => _activeStations;

        public event Action onAnyStationActive;
        public event Action onAllStationsUnactive;

        public ActiveStationsQueue(IStation[] stations)
        {
            _allStations = new Dictionary<string, IStation>(stations.Length);
            foreach(IStation station in stations)
            {
                _allStations.Add(station.ID, station);
            }
            _activeStations = new List<IStation>(_allStations.Count);

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        public void Clear()
        {
            _activeStations.Clear();
            onAllStationsUnactive?.Invoke();
        }

        private void Subscribe()
        {
            foreach (KeyValuePair<string, IStation> station in _allStations)
            {
                station.Value.onStationEnter += AddStation;
                station.Value.onStationExit += RemoveStation;
            }
        }

        private void Unsubscribe()
        {
            foreach (KeyValuePair<string, IStation> station in _allStations)
            {
                station.Value.onStationEnter -= AddStation;
                station.Value.onStationExit -= RemoveStation;
            }
        }

        private void AddStation(string stationID)
        {
            _activeStations.Add(_allStations[stationID]);
            onAnyStationActive?.Invoke();
        }

        private void RemoveStation(string stationID)
        {
            if(_activeStations.Contains(_allStations[stationID]))
            {
                _activeStations.Remove(_allStations[stationID]);
                if (_activeStations.Count == 0)
                    onAllStationsUnactive?.Invoke();
            }
        }
    }
}
