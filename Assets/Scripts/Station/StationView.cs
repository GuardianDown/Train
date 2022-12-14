using System;
using UnityEngine;

namespace Train.Stations
{
    public class StationView : AbstractStationView
    {
        [SerializeField]
        private SphereCollider _sphereCollider = null;

        [SerializeField]
        private GameObject _sphereMesh = null;

        public override event Action onStationEnter;
        public override event Action onStationExit;

        public override void SetRadius(float radius)
        {
            _sphereCollider.radius = radius;
            _sphereMesh.transform.localScale = transform.localScale * radius * 2f;
        }

        private void OnTriggerEnter(Collider other) => onStationEnter?.Invoke();

        private void OnTriggerExit(Collider other) => onStationExit?.Invoke();
    }
}
