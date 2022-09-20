using System.Threading;
using System.Threading.Tasks;

namespace Train.TrainMovement
{
    public class Movement : IMovement
    {
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
            try
            {
                while (!token.IsCancellationRequested)
                {
                    _pathFollower.Input = _joystick.Vertical;
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
