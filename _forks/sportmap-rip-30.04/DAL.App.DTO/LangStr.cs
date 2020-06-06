using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class LangStr : IDomainEntityId
    {
        public Guid Id { get; set; }

        public ICollection<LangStrTranslation>? Translations { get; set; }

        [InverseProperty(nameof(GpsSessionType.Name))]
        public ICollection<GpsSessionType>? GpsSessionTypeNames { get; set; }

        [InverseProperty(nameof(GpsSessionType.Description))]
        public ICollection<GpsSessionType>? GpsSessionTypeDescriptions { get; set; }

        [InverseProperty(nameof(GpsLocationType.Name))]
        public ICollection<GpsLocationType>? GpsLocationTypeNames { get; set; }

        [InverseProperty(nameof(GpsLocationType.Description))]
        public ICollection<GpsLocationType>? GpsLocationTypeDescriptions { get; set; }

        [InverseProperty(nameof(Track.Name))] public ICollection<Track>? TrackNames { get; set; }

        [InverseProperty(nameof(Track.Description))]
        public ICollection<Track>? TrackDescriptions { get; set; }

    }
}