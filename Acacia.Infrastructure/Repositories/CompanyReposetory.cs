using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class CompanyReposetory : GenericRepository<Company>, ICompanyReposetory
{
    public CompanyReposetory(AcaciaDbContext context) : base(context) {}
}

