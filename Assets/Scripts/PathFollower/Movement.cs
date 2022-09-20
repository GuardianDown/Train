using System.Threading;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Train.TrainMovement
{
    public class Movement : IMovement
    {
#if UNITY_EDITOR
        private const string VerticalAxisName = "Vertical";
#endif

        private readonly IPathFollower _pathFollower;
        private readonly Joystick _joystick;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _movementTask;

        public Movement(IPathFollower pathFollower, Joystick joystick)
        {
            _pathFollower = pathFollower;
            _joystick = joystick;
        }

        public async void StartMovement()
        {
            if (_movementTask == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _movementTask = Move(_cancellationTokenSource.Token);
                await _movementTask;
            }
        }

        public void StopMovement()
        {
            if (_movementTask != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _movementTask = null;
            }
        }

        public void Dispose() => StopMovement();

        private async Task Move(CancellationToken token)
        {
            float currentInput;
            try
            {
                while (!token.IsCancellationRequested)
                {
                    currentInput = 0f;
#if UNITY_EDITOR
                    currentInput += Input.GetAxis(VerticalAxisName);
#endif
                    currentInput += _joystick.Vertical;
                    _pathFollower.Input = currentInput;
                    await Task.Yield();
                }
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }
}
