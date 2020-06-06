using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppRole : IdentityRole
    {
        // fix the pk length
        [MaxLength(36)] public override string Id { get; set; } = default!;
    }
}