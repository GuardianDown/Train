using UnityEngine;
using UnityEngine.UI;

namespace Train.Bonuses
{
    public class TakeBonusButton : AbstractTakeBonusView
    {
        [SerializeField]
        private Button _button = null;

        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe() => _button.onClick.AddListener(TakeBonus);

        private void Unsubscribe() => _button.onClick.RemoveListener(TakeBonus);
    }
}