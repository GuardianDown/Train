using UnityEngine;
using Train.Bonuses;

namespace Train.Results
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField]
        private AbstractAmountOfBonusesView _amountOfBonusesView = null;

        public AbstractAmountOfBonusesView AmountOfBonusesView => _amountOfBonusesView;
    }
}
