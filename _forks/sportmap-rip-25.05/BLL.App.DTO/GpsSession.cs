using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;
using com.akaver.sportmap.Contracts.Domain;

namespace BLL.App.DTO
{
    public class GpsSession : IDomainEntityId
    {
        public Guid Id { get; set; }


        [Display(Name = nameof(Name), Prompt = nameof(Name) + "_Prompt",
            ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Name { get; set; } = default!;

        [Display(Name = nameof(Description), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        [MaxLength(4096, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Description { get; set; } = default!;

        [Display(Name = nameof(RecordedAt), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public DateTime RecordedAt { get; set; }

        [Display(Name = nameof(Duration), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public double Duration { get; set; } // in seconds

        [Display(Name = nameof(Speed), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public double Speed { get; set; } // in seconds per kilometer

        [Display(Name = nameof(Distance), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public double Distance { get; set; } // in meters

        [Display(Name = nameof(Climb), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public double Climb { get; set; } // total climb in meters

        [Display(Name = nameof(Descent), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public double Descent { get; set; } // total descent in meters

        // for color coding the track visualisation
        [Display(Name = nameof(PaceMin), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public double PaceMin { get; set; } // pace in seconds per kilometer

        [Display(Name = nameof(PaceMax), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public double PaceMax { get; set; } // pace in seconds per kilometer

        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public Guid AppUserId { get; set; }

        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public AppUser? AppUser { get; set; }

        [Display(Name = nameof(GpsSessionType), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public Guid GpsSessionTypeId { get; set; }

        [Display(Name = nameof(GpsSessionType), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public GpsSessionType? GpsSessionType { get; set; }

        [Display(Name = nameof(GpsLocations), ResourceType = typeof(Resources.BLL.App.DTO.GpsSession))]
        public ICollection<GpsLocation>? GpsLocations { get; set; }
    }
}