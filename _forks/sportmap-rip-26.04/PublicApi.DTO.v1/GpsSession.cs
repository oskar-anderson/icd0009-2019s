using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class GpsSession : IDomainEntityId
    {
        public Guid Id { get; set; }

        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Name { get; set; } = default!;
        
        [MaxLength(4096)]
        [Required]
        public string Description { get; set; } = default!;

        public DateTime RecordedAt { get; set; }
        
        public int Duration { get; set; } // in seconds
        public double Speed { get; set; } // in seconds per kilometer
        public double Distance { get; set; } // in meters
        public double Climb { get; set; } // total climb in meters
        public double Descent { get; set; } // total descent in meters

        // for color coding the track visualisation
        public double PaceMin { get; set; } // pace in seconds per kilometer
        public double PaceMax { get; set; } // pace in seconds per kilometer

        public Guid AppUserId { get; set; }
    }
}