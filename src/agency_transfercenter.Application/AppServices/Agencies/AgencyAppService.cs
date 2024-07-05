using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.EntityDtos.AgencyDtos;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using agency_transfercenter.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace agency_transfercenter.AppServices.Agencies
{
  [Authorize(agency_transfercenterPermissions.Agencies.Default)]
  public class AgencyAppService : CrudAppService<Agency, AgencyDto, int, GetListPagedAndSortedDto, CreateAgencyDto, UpdateAgencyDto>, IAgencyAppService
  {
    private readonly AgencyManager _agencyManager;

    public AgencyAppService(IRepository<Agency, int> repository, AgencyManager agencyManager) : base(repository)
    {
      _agencyManager = agencyManager; 
      DeletePolicyName = agency_transfercenterPermissions.Agencies.Delete;
    }

    [Authorize(agency_transfercenterPermissions.Agencies.Create)]
    public override async Task<AgencyDto> CreateAsync(CreateAgencyDto input)
    {
      var agency = await _agencyManager.CreateAsync(input);

      return agency;
    }

    [Authorize(agency_transfercenterPermissions.Agencies.Edit)]
    public override async Task<AgencyDto> UpdateAsync(int id, UpdateAgencyDto input)
    {
      var agency = await _agencyManager.UpdateAsync(id, input);

      return agency;
    }

    public override async Task<PagedResultDto<AgencyDto>> GetListAsync(GetListPagedAndSortedDto input)
    {
      var transferCenters = await _agencyManager.GetListAsync(input);

      return transferCenters;
    }

    [Authorize(agency_transfercenterPermissions.Agencies.Delete)]
    public async Task DeleteHardAsync(int id)
    {
      await _agencyManager.DeleteHardAsync(id);
    }
  }
}
