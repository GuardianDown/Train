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
        private EndOfPathInstruction _endOfPathInstruction;

        public GameObject TrainViewPrefab => _trainViewPrefab;
        public float MaxSpeed => _maxSpeed;
        public EndOfPathInstruction EndOfPathInstruction => _endOfPathInstruction;
    }
}