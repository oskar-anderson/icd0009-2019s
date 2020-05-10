using System;

namespace DAL.App.DTO
{
    public class GpsSessionView
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public DateTime RecordedAt { get; set; }

        public int Duration { get; set; } // in seconds
        public double Speed { get; set; } // in seconds per kilometer
        public double Distance { get; set; } // in meters
        public double Climb { get; set; } // total climb in meters
        public double Descent { get; set; } // total descent in meters

        // for color coding the track visualisation
        public double MinSpeed { get; set; } // in seconds per kilometer
        public double MaxSpeed { get; set; } // in seconds per kilometer

        public int GpsLocationsCount { get; set; }

        public string UserFirstLastName { get; set; } = default!;
    }
}