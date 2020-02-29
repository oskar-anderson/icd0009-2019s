﻿using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntity : IDomainEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}