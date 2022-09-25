using System;

namespace Train.GameOver
{
    public interface IGameOver : IDisposable
    {
        event Action onGameOver;
    }
}