using System;
using Microsoft.AspNetCore.Identity;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityUser<TUser> : IDomainEntityUser<Guid, TUser>
        where TUser : IdentityUser<Guid>
    {
        
    }

    public interface IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        TKey AppUserId { get; set; }
        // TUser? AppUser { get; set; }
        // why cannot AppUser navigation property be nullable?
    }
}