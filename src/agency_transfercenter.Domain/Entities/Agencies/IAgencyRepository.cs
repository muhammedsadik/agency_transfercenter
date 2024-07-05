using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace agency_transfercenter.Entities.Agencies
{
  public interface IAgencyRepository : IRepository<Agency, int>
  {
    Task<List<Agency>> GetListAsync(
      int skipCount,
      int maxResultCount,
      string sorting,
      string filter = null
      );

  }
}
