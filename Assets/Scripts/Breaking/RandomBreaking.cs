using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Train.TrainMovement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Train.Breaking
{
    public class RandomBreaking : IBreaking
    {
        private readonly int _breakChanceInSecond;
        private readonly IEnumerable<float> _fixDeltas;
        private readonly IMovement _movement;

        public event Action<float> onBreaking;

        public RandomBreaking(int breakChanceInSecond, IEnumerable<float> fixDeltas, IMovement movement)
        {
            _breakChanceInSecond = breakChanceInSecond;
            _fixDeltas = fixDeltas;
            _movement = movement;
        }

        private CancellationTokenSource _cancellationTokenSource;
        private Task _breakingTask;

        public async void StartBreaking()
        {
            if (_breakingTask == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _breakingTask = Break(_cancellationTokenSource.Token);
                await _breakingTask;
            }
        }

        public void StopBreaking()
        {
            if (_breakingTask != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _breakingTask = null;
            }
        }

        public void Dispose() => StopBreaking();

        private async Task Break(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(1000, token);
                    if (Random.Range(0, 100) <= _breakChanceInSecond)
                    {
                        onBreaking?.Invoke(_fixDeltas.ElementAt(Random.Range(0, _fixDeltas.Count())));
                    }
                }
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }
}
