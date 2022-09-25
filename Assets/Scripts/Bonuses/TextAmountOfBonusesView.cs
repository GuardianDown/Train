using UnityEngine;
using UnityEngine.UI;

namespace Train.Bonuses
{
    public class TextAmountOfBonusesView : AbstractAmountOfBonusesView
    {
        [SerializeField]
        private Text text = null;
        public override void UpdateView(int amountOfBonuses) => text.text = amountOfBonuses.ToString();
    }
}
