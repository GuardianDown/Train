using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Train.Timer
{
    public abstract class AbstractTimerView : MonoBehaviour
    {
        private ITimer _timer;

        protected abstract void UpdateView(int currentTime);

        public void Construct(ITimer timer)
        {
            _timer = timer;
            Subscribe();
        }

        protected virtual void OnEnable()
        {
            if (_timer != null)
                Subscribe();
        }

        protected virtual void OnDisable() => Unsubscribe();

        private void Subscribe() => _timer.onTimeChange += UpdateView;

        private void Unsubscribe() => _timer.onTimeChange -= UpdateView;
    }
}
