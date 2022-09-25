using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Train.Bonuses
{
    public class AbstractTakeBonusView : MonoBehaviour
    {
        protected IBonusCounter _bonusCounter;

        public void Construct(IBonusCounter bonusCounter) => _bonusCounter = bonusCounter;

        public void TakeBonus() => _bonusCounter.TakeBonus();
    }
}
