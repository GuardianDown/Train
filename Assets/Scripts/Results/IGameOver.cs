using System;

namespace Train.Results
{
    public interface IGameOver : IDisposable
    {
        event Action onGameOver;
    }
}