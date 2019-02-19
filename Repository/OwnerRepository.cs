using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateOwner(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            Create(owner);
            Save();
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
            Save();
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FindAll().OrderBy(owner => owner.Name);
        }

        public Owner GetOwnerById(Guid Id)
        {
            return FindByCondition(owner => owner.Id.Equals(Id))
                    .DefaultIfEmpty(new Owner())
                    .FirstOrDefault();
        }

        public OwnerExtended GetOwnerWithDetails(Guid Id)
        {
            return new OwnerExtended(GetOwnerById(Id))
            {
                Accounts = RepositoryContext.Accounts.Where(a => a.Id == Id)
            };
        }

        public void UpdateOwner(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            Update(dbOwner);
            Save();
        }
    }
}