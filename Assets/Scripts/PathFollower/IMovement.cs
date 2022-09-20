using System;

namespace Train.TrainMovement
{
    public interface IMovement : IDisposable
    {
        void StartMovement();
        void StopMovement();
    }
}