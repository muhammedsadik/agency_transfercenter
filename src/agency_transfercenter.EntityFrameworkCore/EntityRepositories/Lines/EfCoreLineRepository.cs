using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.TransferCenters;
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


namespace agency_transfercenter.EntityRepositories.Lines
{
  public class EfCoreLineRepository : EfCoreRepository<agency_transfercenterDbContext, Line, int>, ILineRepository
  {
    public EfCoreLineRepository(IDbContextProvider<agency_transfercenterDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Line>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
      var dbSet = await GetDbSetAsync();

      return await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(), l => l.Name.Contains(filter))
        .OrderBy(sorting).Skip(skipCount).Take(maxResultCount).ToListAsync();
    }

  }
}
