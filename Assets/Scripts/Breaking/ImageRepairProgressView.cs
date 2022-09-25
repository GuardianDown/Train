using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Train.Breaking
{
    public class ImageRepairProgressView : AbstractRepairProgressView
    {
        [SerializeField]
        private Image _image = null;

        protected override void OnEnable() => base.OnEnable();

        protected override void UpdateView(float currentRepairValue, float fullRepairValue) => 
            _image.fillAmount = currentRepairValue / fullRepairValue;
    }
}
