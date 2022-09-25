using System;
using System.Collections;
using System.Collections.Generic;
using Train.TrainMovement;
using UnityEngine;

namespace Train.Breaking
{
    public class BreakingMovement : IDisposable
    {
        private readonly IBreaking _breaking;
        private readonly IRepair _repair;
        private IMovement _movement;
        private IPathFollower _pathFollower;

        public BreakingMovement(IBreaking breaking, IRepair repair, IMovement movement, IPathFollower pathFollower)
        {
            _breaking = breaking;
            _repair = repair;
            _movement = movement;
            _pathFollower = pathFollower;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _breaking.onBreaking += StopMovement;
            _repair.onFullRepair += StartMovement;
        }

        private void Unsubscribe()
        {
            _breaking.onBreaking -= StopMovement;
            _repair.onFullRepair -= StartMovement;
        }

        private void StopMovement(float fixDelta)
        {
            _movement.IsBrake = true;
            _breaking.StopBreaking();
        }

        private void StartMovement()
        {
            _movement.IsBrake = false;
            _breaking.StartBreaking();
        }
    }
}
