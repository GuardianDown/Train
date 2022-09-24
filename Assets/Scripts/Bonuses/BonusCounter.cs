using System;
using Train.Stations;
using UnityEngine.UI;

namespace Train.Bonuses
{
    public class BonusCounter : IBonusCounter
    {
        private readonly IActiveStationsQueue _activeStationsQueue;
        private readonly BonusesData _bonusesData;

        public BonusCounter(IActiveStationsQueue activeStationsQueue, BonusesData bonusesData)
        {
            _activeStationsQueue = activeStationsQueue;
            _bonusesData = bonusesData;
        }

        public void TakeBonus()
        {
            _bonusesData._AmountOfBonuses += _activeStationsQueue.ActiveStations.Count;
            _activeStationsQueue.Clear();
        }
    }
}
