using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class IngredientReposetory : GenericRepository<Ingredient>, IIngredientRepository
{
    public IngredientReposetory(AcaciaDbContext context) : base(context) {}
}

