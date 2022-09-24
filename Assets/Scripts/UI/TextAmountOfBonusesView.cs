using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Train.UI
{
    public class TextAmountOfBonusesView : AbstractAmountOfBonusesView
    {
        [SerializeField]
        private Text text = null;
        public override void UpdateView(int amountOfBonuses) => text.text = amountOfBonuses.ToString();
    }
}
