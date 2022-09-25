using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Train.Timer
{
    public class CustomTimer : ITimer
    {
        private readonly int _time;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _timerTask;
        private int _currentTime;

        public event Action<int> onTimeChange;
        public event Action onTimeEnd;

        public CustomTimer(int timeInSeconds)
        {
            _time = timeInSeconds;
        }

        public async void StartTimer()
        {
            if (_timerTask == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _timerTask = CountTime(_cancellationTokenSource.Token);
                await _timerTask;
            }
        }

        public void StopTimer()
        {
            if (_timerTask != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _timerTask = null;
            }
        }

        public void Dispose() => StopTimer();

        private async Task CountTime(CancellationToken token)
        {
            try
            {
                _currentTime = _time;
                onTimeChange(_currentTime);
                while (!token.IsCancellationRequested && _currentTime > 0)
                {
                    await Task.Delay(1000, token);
                    _currentTime -= 1;
                    onTimeChange?.Invoke(_currentTime);
                }

                onTimeEnd?.Invoke();
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }
}
