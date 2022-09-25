using UnityEngine;

namespace Train.Breaking
{
    public class AbstractFixView : MonoBehaviour
    {
        protected IRepair _repair;

        public void Construct(IRepair repair) => _repair = repair;

        public void Fix() => _repair.Fix();
    }
}
