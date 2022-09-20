using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Train.UI
{
    public class TrainControlView : MonoBehaviour
    {
        [SerializeField]
        private Button _takeBonusButton = null;

        public Button TakeBonusButton => _takeBonusButton;
    }
}
