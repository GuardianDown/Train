using PathCreation;
using UnityEngine;

namespace Train.TrainMovement
{
    [CreateAssetMenu(fileName = "PathCreatorData", menuName = "GameData/PathCreatorData")]
    public class PathCreatorData : ScriptableObject
    {
        [SerializeField]
        private PathCreator _pathCreatorPrefab = null;

        [SerializeField]
        private Vector3 _spawnPosition;

        public PathCreator PathCreatorPrefab => _pathCreatorPrefab;
        public Vector3 SpawnPosition => _spawnPosition;
    }
}
