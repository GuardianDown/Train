using UnityEngine;

namespace Train.Stations
{
    [CreateAssetMenu(fileName = "StationData", menuName = "GameData/StationData")]
    public class StationData : ScriptableObject
    {
        [SerializeField]
        private AbstractStationView _stationViewPrefab = null;

        [SerializeField]
        private string _id = string.Empty;

        [SerializeField]
        private float _stationRadius;

        [SerializeField]
        private float _positionOnPath;

        [SerializeField]
        private int _amountOfBonuses;

        public AbstractStationView StationViewPrefab => _stationViewPrefab;
        public string ID => _id;
        public float StationRadius => _stationRadius;
        public float PositionOnPath => _positionOnPath;
        public int AmountOfBonuses => _amountOfBonuses;
    }
}
