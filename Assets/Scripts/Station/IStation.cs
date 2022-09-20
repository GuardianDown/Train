using System;

namespace Train.Stations
{
    public interface IStation : IDisposable
    {
        string ID { get; }
        int AmountOfBonuses { get; }

        event Action<string> onStationEnter;
        event Action<string> onStationExit;
    }
}