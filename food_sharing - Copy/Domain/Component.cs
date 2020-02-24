using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Component
    {
        [Required] public int ComponentId { get; set; }
        
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        [Range(0, 4)]
        public int Max { get; set; }
        
                
        /*
        0.6 €
        Ananass
        Basiilik
        Jalapeno
        Mais
        Marineeritud kurk
        Mustad oliivid
        Paprika
        Peekon
        Punane sibul
        Salaami
        Sinihallitusjuust
        Tomat
        Vorst
        
        1.2 €
        Hakkliha
        Juust
        Suitsukana
        Šampinjonid
        Tex-mex kaste
        BBQ kaste
        
        1.5 €
        Mozarella juust
        Tuunikala
        Krevetid
        
        */
        
    }
}