using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class OwnerAnimalCreateEditVM
    {
        public OwnerAnimal OwnerAnimal { get; set; } = default!;
        
        public SelectList? AnimalSelectList { get; set; }
        public SelectList? OwnerSelectList { get; set; }
    }
}