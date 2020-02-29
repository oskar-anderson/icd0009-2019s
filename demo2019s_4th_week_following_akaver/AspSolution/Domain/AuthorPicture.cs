using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class AuthorPicture : DomainEntityMetadata
    {
        [MaxLength(255)] public string PictureURL { get; set; } = default!;

        [MaxLength(36)] public string AuthorId { get; set; } = default!;
        [ForeignKey(nameof(AuthorId))]
        public Author? Author { get; set; }
    }
}