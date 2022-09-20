using PathCreation;
using UnityEngine;

namespace Train.TrainMovement
{
    [CreateAssetMenu(fileName = "TrainData", menuName = "GameData/TrainData")]
    public class TrainData : ScriptableObject
    {
        [SerializeField]
        private GameObject _trainViewPrefab = null;

        [SerializeField]
        private float _maxSpeed;

        [SerializeField]
        private float _acceleration;

        [SerializeField]
        private EndOfPathInstruction _endOfPathInstruction;

        public GameObject TrainViewPrefab => _trainViewPrefab;
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public EndOfPathInstruction EndOfPathInstruction => _endOfPathInstruction;
    }
}