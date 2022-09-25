using UnityEngine;

namespace Train.Bonuses
{
    public abstract class AbstractAmountOfBonusesView : MonoBehaviour
    {
        protected BonusesData _bonusesData;

        public abstract void UpdateView(int amountOfBonuses);

        public void Construct(BonusesData bonusesData)
        {
            _bonusesData = bonusesData;
            Subscribe();
        }

        protected virtual void OnEnable()
        {
            if (_bonusesData != null)
                Subscribe();
        }

        protected virtual void OnDisable() => Unsubscribe();

        private void Subscribe() => _bonusesData.onAmountOfBonusesChange += UpdateView;

        private void Unsubscribe() => _bonusesData.onAmountOfBonusesChange -= UpdateView;
    }
}
