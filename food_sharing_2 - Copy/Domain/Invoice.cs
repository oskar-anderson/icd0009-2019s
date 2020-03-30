﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
 using DAL.Base;

 namespace Domain
{
    public class Invoice : DomainEntity
    {
        [MaxLength(32)] public string PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        [MaxLength(32)] public string RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        [MaxLength(32)] public string PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
        
        [Required]
        [MaxLength(32)]
        public string InvoiceCode
        {
            get { return InvoiceCode; }
            set { InvoiceCode = RandomString(20); }
        }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalNet { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalTax { get; set; } = default!;
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalGross { get; set; } = default!;
        
        public virtual ICollection<InvoiceLine>? InvoiceLines { get; set; }
        
        
        
        
        
        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[32];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}