using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace agency_transfercenter.Entities.TransferCenters
{
  public interface ITransferCenterRepository : IRepository<TransferCenter, int>
  {
    Task<List<TransferCenter>> GetListAsync(
      int skipCount,
      int maxResultCount,
      string sorting,
      string filter = null
      );

  }
}
