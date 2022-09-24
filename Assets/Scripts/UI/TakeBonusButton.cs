using Train.Bonuses;
using UnityEngine;
using UnityEngine.UI;

namespace Train.UI
{
    public class TakeBonusButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;

        private IBonusCounter _bonusCounter;

        public void Construct(IBonusCounter bonusCounter) => _bonusCounter = bonusCounter;

        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe() => _button.onClick.AddListener(TakeBonus);

        private void Unsubscribe() => _button.onClick.RemoveListener(TakeBonus);

        private void TakeBonus() => _bonusCounter.TakeBonus();
    }
}