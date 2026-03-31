using AstraTech.Routeplanner.Models;

namespace AstraTech.Routeplanner.Services
{
    public class NavigationService
    {
        public RouteCalculationResult CalculateShortestPath(
            List<Station> stations,
            List<Connection> connections,
            int startId,
            int destinationId)
        {
            var distances = new Dictionary<int, int>();
            var previous = new Dictionary<int, int?>();
            var unvisited = new List<int>();

            foreach (var station in stations)
            {
                distances[station.Id] = int.MaxValue;
                previous[station.Id] = null;
                unvisited.Add(station.Id);
            }

            distances[startId] = 0;

            while (unvisited.Any())
            {
                var currentId = unvisited
                    .OrderBy(id => distances[id])
                    .First();

                if (distances[currentId] == int.MaxValue)
                    break;
                
                if(currentId == destinationId)
                    break;
                unvisited.Remove(currentId);

                var neighbors = connections.Where(c => c.FromStationId == currentId);

                foreach (var neighbor in neighbors)
                {
                    int alternativeDistance = distances[currentId] + neighbor.Distance;

                    if (alternativeDistance < distances[neighbor.ToStationId])
                    {
                        distances[neighbor.ToStationId] = currentId;
                    }
                }
            }

            var result = new RouteCalculationResult
            {
                FromStationId = startId,
                ToStationId = destinationId,
                StartStationName = stations.FirstOrDefault(s => s.Id == startId)?.Name ?? string.Empty,
                DestinationStationName = stations.FirstOrDefault(s => s.Id == destinationId)?.Name ?? string.Empty,
                RouteFound = distances[destinationId] != int.MaxValue,
                TotalDistance = distances[destinationId] == int.MaxValue ? 0 : distances[destinationId],
            };

            if (!result.RouteFound)
                return result;

            var path = new List<int>();
            int? current = destinationId;

            while (current != null)
            {
                path.Add(current.Value);
                current = previous[current.Value];
            }

            path.Reverse();

            result.Path = path
                .Select(id => stations.First(s => s.Id == id).Name)
                .ToList();

            return result;
                
        }
    }
}
