using System;
using System.Collections.Generic;
using Entities.Models;

namespace Entities.ExtendedModels
{
    public class OwnerExtended : IEntity
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public OwnerExtended()
        {
        }

        public OwnerExtended(Owner owner)
        {
            OwnerId = owner.OwnerId;
            Name = owner.Name;
            DateOfBirth = owner.DateOfBirth;
            Address = owner.Address;
        }
    }
}