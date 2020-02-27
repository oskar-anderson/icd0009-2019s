using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Category : DomainEntityMetadata
    {
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        
        [MaxLength(36)]
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        
        public ICollection<Category>? ChildCategories { get; set; }
        
        public ICollection<PostCategory>? PostCategories { get; set; }
    }
}