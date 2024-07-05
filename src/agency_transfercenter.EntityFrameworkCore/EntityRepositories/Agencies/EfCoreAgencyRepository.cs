using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace agency_transfercenter.EntityRepositories.Agencies
{
  public class EfCoreAgencyRepository : EfCoreRepository<agency_transfercenterDbContext, Agency, int>, IAgencyRepository
  {
    public EfCoreAgencyRepository(IDbContextProvider<agency_transfercenterDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }


    public async Task<List<Agency>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
      var dbSet = await GetDbSetAsync();

      return await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(), tc => tc.UnitName.Contains(filter))
        .OrderBy(sorting).Skip(skipCount).Take(maxResultCount).ToListAsync();
    }
  }
}
