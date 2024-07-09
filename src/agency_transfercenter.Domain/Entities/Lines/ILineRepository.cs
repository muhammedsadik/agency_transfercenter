﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace agency_transfercenter.Entities.Lines
{
  public interface ILineRepository : IRepository<Line, int>
  {
    Task<List<Line>> GetListAsync(
      int skipCount,
      int maxResultCount,
      string sorting,
      string filter = null
      );
  }
}
