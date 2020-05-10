﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class PaymentMethod : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; } 
        
        public DateTime Since { get; set; } = default!;
        public DateTime Until { get; set; } = default!;

    }
}