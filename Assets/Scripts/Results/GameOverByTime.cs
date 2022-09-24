using System;
using Train.Timer;

namespace Train.Results
{
    public class GameOverByTime : IGameOver
    {
        private readonly ITimer _timer;

        public event Action onGameOver;

        public GameOverByTime(ITimer timer)
        {
            _timer = timer;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe() => _timer.onTimeEnd += GameOver;

        private void Unsubscribe() => _timer.onTimeEnd -= GameOver;

        private void GameOver() => onGameOver?.Invoke();
    }
}
