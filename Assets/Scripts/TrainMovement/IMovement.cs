using System;

namespace Train.TrainMovement
{
    public interface IMovement : IDisposable
    {
        bool IsBrake { get; set; }

        void StartMovement();
        void StopMovement();
    }
}