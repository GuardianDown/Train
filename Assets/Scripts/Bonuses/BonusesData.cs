using System;
using Train.Save;
using UnityEngine;

namespace Train.Bonuses
{
    public class BonusesData : ISaveData
    {
        private int _amountOfBonuses;

        public event Action<int> onAmountOfBonusesChange;

        public int AmountOfBonuses
        {
            get => _amountOfBonuses;
            set
            {
                if (value >= 0 && value != _amountOfBonuses)
                {
                    _amountOfBonuses = value;
                    onAmountOfBonusesChange?.Invoke(_amountOfBonuses);
                }
            }
        }

        public void Save()
        {
            if(PlayerPrefs.GetInt(Constants.BestResultPrefsKey, 0) < _amountOfBonuses)
                PlayerPrefs.SetInt(Constants.BestResultPrefsKey, _amountOfBonuses);
        }
    }
}
