using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class LangStrTranslation : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(5)] public string Culture { get; set; } = default!;
        [MaxLength(10240)] public string Value { get; set; } = default!;

        public Guid LangStrId { get; set; } = default!;
        public LangStr LangStr { get; set; } = default!;
    }
}