using System;

namespace Train.Bonuses
{
    public class BonusesData
    {
        private int _amountOfBonuses;

        public event Action<int> onAmountOfBonusesChange;

        public int _AmountOfBonuses
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
    }
}
