using UnityEngine;

namespace Train.Stations
{
    [CreateAssetMenu(fileName = "StationData", menuName = "GameData/StationData")]
    public class StationData : ScriptableObject
    {
        [SerializeField]
        private AbstractStationView _stationViewPrefab = null;

        [SerializeField]
        private float _stationRadius;

        [SerializeField]
        private float _positionOnPath;

        public AbstractStationView StationViewPrefab => _stationViewPrefab;
        public float StationRadius => _stationRadius;
        public float PositionOnPath => _positionOnPath;
    }
}
