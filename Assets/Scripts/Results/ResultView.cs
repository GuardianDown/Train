using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Train.UI;

namespace Train.Results
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField]
        private AbstractAmountOfBonusesView _amountOfBonusesView = null;

        public AbstractAmountOfBonusesView AmountOfBonusesView => _amountOfBonusesView;
    }
}
