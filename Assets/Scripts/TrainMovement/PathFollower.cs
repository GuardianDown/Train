using PathCreation;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Train.TrainMovement
{
    public class PathFollower : IPathFollower
    {
        private readonly PathCreator _pathCreator;
        private readonly Transform _followerView;
        private readonly float _maxSpeed;
        private readonly float _acceleration;
        private readonly EndOfPathInstruction _endOfPathInstruction;

        private float _speed;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _followTask;

        public event Action onStartMovement;
        public event Action onStopMovement;

        public float Speed
        {
            get => _speed;
            set
            {
                if (_speed >= 0f && _speed <= _maxSpeed)
                {
                    if (value > 0f && _speed == 0f)
                        onStartMovement?.Invoke();

                    _speed += value * _acceleration;
                }
                if (_speed < 0f)
                {
                    _speed = 0f;
                    onStopMovement?.Invoke();
                }
                else if(_speed > _maxSpeed)
                    _speed = _maxSpeed;
            }
        }

        public Transform FollowerView => _followerView;

        public PathFollower(PathCreator pathCreator, Transform followerView, 
            float maxSpeed, float acceleration, EndOfPathInstruction endOfPathInstruction)
        {
            _pathCreator = pathCreator;
            _followerView = followerView;
            _maxSpeed = maxSpeed;
            _acceleration = acceleration;
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
                    distanceTravelled += Speed * Time.deltaTime;
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
