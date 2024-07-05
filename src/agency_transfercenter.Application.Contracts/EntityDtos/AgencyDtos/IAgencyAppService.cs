using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace agency_transfercenter.EntityDtos.AgencyDtos
{
  public interface IAgencyAppService : ICrudAppService<AgencyDto, int, GetListPagedAndSortedDto, CreateAgencyDto, UpdateAgencyDto>
  {
    Task DeleteHardAsync(int id);
  }
}
