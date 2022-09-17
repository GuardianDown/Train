using PathCreation;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Train.TrainMovement
{
    public class PathFollower : IPathFollower
    {
        private readonly PathCreator _pathCreator;
        private readonly Transform _followerView;

        private float _speed;
        private EndOfPathInstruction _endOfPathInstruction;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _followTask;

        public PathFollower(PathCreator pathCreator, Transform followerView, float speed, EndOfPathInstruction endOfPathInstruction)
        {
            _pathCreator = pathCreator;
            _followerView = followerView;
            _speed = speed;
            _endOfPathInstruction = endOfPathInstruction;
        }

        public async void StartFollow()
        {
            if (_followTask == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _followTask = Follow(_cancellationTokenSource.Token);
                await _followTask;
            }
        }

        public void StopFollow()
        {
            if (_followTask != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _followTask = null;
            }
        }

        public void Dispose() => StopFollow();

        private async Task Follow(CancellationToken token)
        {
            float distanceTravelled = 0f;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    distanceTravelled += _speed * Time.deltaTime;
                    _followerView.position = _pathCreator.path.GetPointAtDistance(distanceTravelled, _endOfPathInstruction);
                    _followerView.rotation = _pathCreator.path.GetRotationAtDistance(distanceTravelled, _endOfPathInstruction);
                    await Task.Yield();
                }
            }
            catch(TaskCanceledException)
            {
                return;
            }
        }
    }
}
