using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Meal
    {
        [Required] public int MealId { get; set; }
        
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }
        
        [Required] public int MenuId { get; set; }
        public Menu Menu { get; set; }
        
        [Required] public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        [Required] 
        [MaxLength(128)]
        public string Name { get; set; }
        
        [Required] public float Price { get; set; }
        
        
        /*
        Americana
        5/8€
        juust, salaami, ananass, oliivid
        
        Bolognese
        6/9€
        hakkliha, sibul, küüslauk, mozarella, juust  
        
        Siciliana
        5/8€
        juust, sink, küüslauk, sibul
        
        Topolino
        5.5/8.2€
        juust, sink, šampinjonid, ananass
        
        Pepperoni
        5/8€
        juust, kaste, pepperoni, basiilik, mozzarella, oliivid
        
        Caesari Salat
        7.8€
        
        Coca-cola 0,5L
        1,5€
        
        Sprite 1L
        2,5€
        
        Vesi 0,5L
        1€
        
        */
    }
}