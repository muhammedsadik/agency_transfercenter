using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.EntityFrameworkCore; 
using System.Text;



namespace agency_transfercenter.TransferCenters
{
  public class EfCoreTransferCenterRepository : EfCoreRepository<agency_transfercenterDbContext, TransferCenter, int>, ITransferCenterRepository
  {
    public EfCoreTransferCenterRepository(IDbContextProvider<agency_transfercenterDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }


    public async Task<List<TransferCenter>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
      var dbSet = await GetDbSetAsync();

      return await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(),tc => tc.UnitName.Contains(filter))
        .OrderBy(sorting).Skip(skipCount).Take(maxResultCount).ToListAsync();
    
    } 
  }
}
