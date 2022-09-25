using UnityEngine;
using UnityEngine.UI;

namespace Train.Timer
{
    public class TextTimerView : AbstractTimerView
    {
        [SerializeField]
        private Text text = null;

        protected override void UpdateView(int currentTime) => text.text = currentTime.ToString();
    }
}
