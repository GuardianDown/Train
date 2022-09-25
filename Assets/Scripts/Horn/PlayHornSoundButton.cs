using UnityEngine;
using UnityEngine.UI;

namespace Train.Horn
{
    public class PlayHornSoundButton : AbstractPlayHornSoundView
    {
        [SerializeField]
        private Button _button = null;

        private void OnEnable()
        {
            if (_hornSound != null)
                Subscribe();
        }

        public override void Construct(IHornSound hornSound)
        {
            base.Construct(hornSound);
            Subscribe();
        }

        private void OnDisable() => Unsubscribe();

        private void Subscribe() => _button.onClick.AddListener(PlayHornSound);

        private void Unsubscribe() => _button.onClick.RemoveListener(PlayHornSound);
    }
}
