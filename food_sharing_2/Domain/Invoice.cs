﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
 using DAL.Base;

 namespace Domain
{
    public class Invoice : DomainEntityMetadata
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
        
        
        
        
        
        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}