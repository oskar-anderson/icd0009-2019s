﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using Domain.Base;

 namespace Domain
{
    public class Component : DomainEntityIdMetadata
    {
        [MaxLength(32)] public string Name { get; set; } = default!;

        [Range(0, 4)] public int Max { get; set; } = default!;

        public ICollection<ComponentPrice>? ComponentPrices { get; set; }
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }
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