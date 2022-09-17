using PathCreation;

namespace Train.Stations
{
    public class Station
    {
        private readonly AbstractStationView _stationView;

        public Station(AbstractStationView stationView, float radius, PathCreator pathCreator, float positionOnPath)
        {
            _stationView = stationView;
            _stationView.SetRadius(radius);
            _stationView.transform.position = pathCreator.path.GetPointAtDistance(positionOnPath);
            _stationView.transform.rotation = pathCreator.path.GetRotationAtDistance(positionOnPath);
        }
    }
}
