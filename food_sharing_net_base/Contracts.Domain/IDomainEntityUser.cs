using System;
using Microsoft.AspNetCore.Identity;

namespace ee.itcollege.kaande.pitsariina.Contracts.Domain
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
    }
}