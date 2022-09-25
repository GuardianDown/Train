using System.Collections.Generic;
using UnityEngine;

namespace Train.Breaking
{
    [CreateAssetMenu(fileName = "BreakingData", menuName = "GameData/BreakingData")]
    public class BreakingData : ScriptableObject
    {
        [Range(0, 99)]
        [SerializeField]
        private int _breakingChanceInSecond;

        [SerializeField]
        private float _fullRepairValue;

        [SerializeField]
        private float[] _fixDeltas = null;

        public int BreakingChanceInSecond => _breakingChanceInSecond;
        public float FullRepairValue => _fullRepairValue;
        public IReadOnlyCollection<float> FixDeltas => _fixDeltas;
    }
}
