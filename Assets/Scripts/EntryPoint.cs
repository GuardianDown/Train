using PathCreation;
using Train.TrainMovement;
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

        private PathCreator _pathCreator;
        private GameObject _trainView;
        private IPathFollower _pathFollower;

        private void Awake()
        {
            _pathCreator = Instantiate(_pathCreatorData.PathCreatorPrefab, _pathCreatorData.SpawnPosition, Quaternion.identity);
            _trainView = Instantiate(_trainData.TrainViewPrefab);
            _pathFollower = new PathFollower(_pathCreator, _trainView.transform, _trainData.Speed, _trainData.EndOfPathInstruction);
            _pathFollower.StartFollow();
        }

        private void OnDestroy() => _pathFollower.Dispose();
    }
}
