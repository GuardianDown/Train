using System;
using UnityEngine;

namespace Train.Stations
{
    public abstract class AbstractStationView : MonoBehaviour
    {
        public abstract event Action onStationEnter;
        public abstract event Action onStationExit;

        public abstract void SetRadius(float radius);
    }
}