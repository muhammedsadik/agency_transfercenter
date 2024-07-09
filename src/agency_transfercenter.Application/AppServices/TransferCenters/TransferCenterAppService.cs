using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.EntityDtos.AgencyDtos;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using agency_transfercenter.EntityDtos.TransferCenterDtos;
using agency_transfercenter.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace agency_transfercenter.AppServices.TransferCenters
{
  [Authorize(agency_transfercenterPermissions.Transfercenters.Default)]
  public class TransferCenterAppService : CrudAppService<TransferCenter, TransferCenterDto, int, GetListPagedAndSortedDto, CreateTransferCenterDto, UpdateTransferCenterDto>, ITransferCenterAppService
  {
    private readonly TransferCenterManager _transferCenterManager;

    public TransferCenterAppService(IRepository<TransferCenter, int> repository, TransferCenterManager transferCenterManager) : base(repository)
    {
      _transferCenterManager = transferCenterManager;

      DeletePolicyName = agency_transfercenterPermissions.Transfercenters.Delete;
    }


    [Authorize(agency_transfercenterPermissions.Transfercenters.Create)]
    public override async Task<TransferCenterDto> CreateAsync(CreateTransferCenterDto input)
    {
      var transferCenter = await _transferCenterManager.CreateAsync(input);

      return transferCenter;
    }

    [Authorize(agency_transfercenterPermissions.Transfercenters.Edit)]
    public override async Task<TransferCenterDto> UpdateAsync(int id, UpdateTransferCenterDto input)
    {
      var transferCenter = await _transferCenterManager.UpdateAsync(id,input);

      return transferCenter;
    }

    public override async Task<PagedResultDto<TransferCenterDto>> GetListAsync(GetListPagedAndSortedDto input)
    {
      var transferCenters = await _transferCenterManager.GetListAsync(input);

      return transferCenters;
    }

    [Authorize(agency_transfercenterPermissions.Transfercenters.Delete)]
    public async Task DeleteHardAsync(int id)
    {
      await _transferCenterManager.DeleteHardAsync(id);
    }

    public async Task<PagedResultDto<AgencyDto>> GetListAgenciesByTransferCenterIdAsync(int id)
    {
      var transferCenters = await _transferCenterManager.GetListAgenciesByTransferCenterIdAsync(id);

      return transferCenters;
    }
  }
}
