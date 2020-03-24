using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Author : DomainEntity
    {
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

        [MaxLength(36)] public string AppUserId { get; set; } = default!; 
        public AppUser? AppUser { get; set; }
        
        [MaxLength(36)] public string? AuthorPictureId { get; set; }

        //[InverseProperty(nameof(Domain.AuthorPicture.Author))]
        [ForeignKey(nameof(AuthorPictureId))] public AuthorPicture? AuthorPicture { get; set; }

        [InverseProperty(nameof(Post.PersonWhoHasWrittenIt))]
        public ICollection<Post>? AuthoredPosts { get; set; }

        [InverseProperty(nameof(Post.CoAuthor))]
        public ICollection<Post>? CoAuthoredPosts { get; set; }

        [NotMapped] //if it is just a getter - then notmapped is by convention
        public string FirstLastName
        {
            get => FirstName + " " + LastName;
            set
            {
                var parts = value.Split(" ");
                FirstName = parts[0];
                if (parts.Length > 0)
                {
                    LastName = string.Join("-", parts);
                }
                else
                {
                    LastName = "noname";
                }
            }
        }
    }
}