using System;
using System.Collections.Generic;

namespace Train.Stations
{
    public interface IActiveStationsQueue : IDisposable
    {
        IReadOnlyCollection<IStation> ActiveStations { get; }

        event Action onAnyStationActive;
        event Action onAllStationsUnactive;
    }
}