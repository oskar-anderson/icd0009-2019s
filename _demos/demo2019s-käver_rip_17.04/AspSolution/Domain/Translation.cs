using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Principal;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Translation : Translation<Guid>, IDomainBaseEntity
    {
        
    }
    public class Translation<TKey> : DomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(5)] public string Culture { get; set; }
        [MaxLength(10240)] public string Value { get; set; }

        public TKey MultiLangStringId { get; set; }
        public virtual MultiLangString? MultiLangString { get; set; }
    }
}