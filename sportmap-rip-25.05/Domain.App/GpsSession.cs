using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DAL.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class GpsSession : DomainEntityIdMetadataUser<AppUser>
    {
        [Display(Name = nameof(Name), Prompt = nameof(Name) + "_Prompt",
            ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Name { get; set; } = default!;
        
        [MaxLength(4096)]
        [Required]
        public string Description { get; set; } = default!;

        [DataType(DataType.DateTime)]
        public DateTime RecordedAt { get; set; }
        
        public double Duration { get; set; } // in seconds
        public double Speed { get; set; } // in seconds per kilometer
        public double Distance { get; set; } // in meters
        public double Climb { get; set; } // total climb in meters
        public double Descent { get; set; } // total descent in meters

        // for color coding the track visualisation
        public double PaceMin { get; set; } // pace in seconds per kilometer
        public double PaceMax { get; set; } // pace in seconds per kilometer


        public Guid GpsSessionTypeId { get; set; }
        public GpsSessionType? GpsSessionType { get; set; }
        
        public ICollection<GpsLocation>? GpsLocations { get; set; } 
    }
}