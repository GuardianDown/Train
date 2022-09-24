using System;
using UnityEngine;

namespace Train.TrainMovement
{
    public interface IPathFollower : IDisposable
    {
        float Input { get; set; }
        float Speed { get; }
        Transform FollowerView { get; }

        event Action onStartMovement;
        event Action onStopMovement;

        void StartFollow();

        void StopFollow();
    }
}