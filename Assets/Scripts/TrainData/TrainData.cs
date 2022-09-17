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
        private float _speed;

        [SerializeField]
        private EndOfPathInstruction _endOfPathInstruction;

        public GameObject TrainViewPrefab => _trainViewPrefab;
        public float Speed => _speed;
        public EndOfPathInstruction EndOfPathInstruction => _endOfPathInstruction;
    }
}