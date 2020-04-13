using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainBaseEntity : IDomainBaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}