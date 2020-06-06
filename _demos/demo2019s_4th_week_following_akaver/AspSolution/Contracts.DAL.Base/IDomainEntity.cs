using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity
    {
        string Id { get; set; }
    }
}