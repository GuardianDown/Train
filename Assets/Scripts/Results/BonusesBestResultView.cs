using UnityEngine;
using UnityEngine.UI;

namespace Train.Results
{
    public class BonusesBestResultView : AbstractBestResultView<int>
    {
        [SerializeField]
        private Text text = null;

        protected override void LoadResult() => _bestResult = PlayerPrefs.GetInt(Constants.BestResultPrefsKey, 0);

        protected override void SetToView() => text.text = _bestResult.ToString();
    }
}
