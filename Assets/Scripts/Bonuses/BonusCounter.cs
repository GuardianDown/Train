using System;
using Train.Stations;
using UnityEngine.UI;

namespace Train.Bonuses
{
    public class BonusCounter : IDisposable
    {
        private readonly Button _takeBonusButton;
        private readonly IActiveStationsQueue _activeStationsQueue;
        private readonly Text _bonusView;
        private readonly BonusesData _bonusesData;

        public BonusCounter(Button takeBonusButton, IActiveStationsQueue activeStationsQueue, Text bonusView)
        {
            _takeBonusButton = takeBonusButton;
            _activeStationsQueue = activeStationsQueue;
            _bonusView = bonusView;
            _bonusesData = new BonusesData();

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _takeBonusButton.onClick.AddListener(TakeBonus);
            _bonusesData.onAmountOfBonusesChange += UpdateView;
        }

        private void Unsubscribe()
        {
            if(_takeBonusButton != null)
                _takeBonusButton.onClick.RemoveListener(TakeBonus);
            _bonusesData.onAmountOfBonusesChange -= UpdateView;
        }

        private void TakeBonus()
        {
            _bonusesData._AmountOfBonuses += _activeStationsQueue.ActiveStations.Count;
            _activeStationsQueue.Clear();
        }

        private void UpdateView(int value) => _bonusView.text = value.ToString();
    }
}
