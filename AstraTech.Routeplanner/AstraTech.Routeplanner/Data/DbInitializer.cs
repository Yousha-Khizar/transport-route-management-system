using AstraTech.Routeplanner.Models;
using AstraTech.RoutePlanner.Data;

namespace AstraTech.Routeplanner.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Stations.Any())
            {
                var stations = new List<Station>
                {
                    new Station { Name = "Orbital Hub Alpha", Type = "Station" },
                    new Station { Name = "Lunar Gate One", Type = "Außenposten" },
                    new Station { Name = "Mars Relay Prime", Type = "Relaisstaion" },
                    new Station { Name = "Titan Supply Dock", Type = "Versorgungsdock" },
                    new Station { Name = "Europa Transfer Node", Type = "Transfersknoten" }
                };

                context.Stations.AddRange(stations);
                context.SaveChanges();
            }

            if (!context.Connections.Any())
            {
                var stations = context.Stations.ToList();

                int alpha = stations.First(s => s.Name == "Orbital Hub Alpha").Id;
                int lunar = stations.First(s => s.Name == "Lunar Gate One").Id;
                int mars = stations.First(s => s.Name == "Mars Relay Prime").Id;
                int titan = stations.First(s => s.Name == "Titan Supply Dock").Id;
                int europa = stations.First(s => s.Name == "Europa Transfer Node").Id;

                var connections = new List<Connection>
                {
                    new Connection { FromStationId = alpha, ToStationId = lunar, Distance = 10 },
                    new Connection { FromStationId = lunar, ToStationId = alpha, Distance = 10 },

                    new Connection { FromStationId = alpha, ToStationId = mars, Distance = 25 },
                    new Connection { FromStationId = mars, ToStationId = alpha, Distance = 25 },

                    new Connection { FromStationId = lunar, ToStationId = europa, Distance = 15 },
                    new Connection { FromStationId = europa, ToStationId = lunar, Distance = 15 },

                    new Connection { FromStationId = europa, ToStationId = mars, Distance = 10 },
                    new Connection { FromStationId = mars, ToStationId = europa, Distance = 10 },

                    new Connection { FromStationId = mars, ToStationId = titan, Distance = 20 },
                    new Connection { FromStationId = titan, ToStationId = mars, Distance = 20 },

                    new Connection { FromStationId = lunar, ToStationId = titan, Distance = 50 },
                    new Connection { FromStationId = titan, ToStationId = lunar, Distance = 50 }
                };

                context.Connections.AddRange(connections);
                context.SaveChanges();

            }
            
        }
    }
}
