using System;
using UnityEngine;

namespace Train.TrainMovement
{
    public interface IPathFollower : IDisposable
    {
        float Speed { get; set; }
        Transform FollowerView { get; }

        event Action onStartMovement;
        event Action onStopMovement;

        void StartFollow();

        void StopFollow();
    }
}