using agency_transfercenter.Entities.TransferCenters;
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
using Volo.Abp.Validation;

namespace agency_transfercenter.AppServices.TransferCenters
{
  [Authorize(agency_transfercenterPermissions.Transfercenters.Default)]
  public class TransferCenterAppService : CrudAppService<TransferCenter, TransferCenterDto, int, GetTransferCenterListDto, CreateTransferCenterDto, UpdateTransferCenterDto>, ITransferCenterAppService
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

    [Authorize(agency_transfercenterPermissions.Transfercenters.Edit)]
    public async Task<TransferCenterDto> UpdateManagerOfTransferCenterAsync(int id, UpdateManagerOfTransferCenterDto input)
    {
      var transferCenter =await _transferCenterManager.UpdateManagerAsync(id, input);

      return transferCenter;
    }

    [Authorize(agency_transfercenterPermissions.Transfercenters.Edit)]
    public async Task<TransferCenterDto> UpdateUnitOfTransferCenterAsync(int id, UpdateUnitOfTransferCenterDto input)
    {
      var transferCenter =await _transferCenterManager.UpdateUnitAsync(id, input);

      return transferCenter;
    }

    public override async Task<PagedResultDto<TransferCenterDto>> GetListAsync(GetTransferCenterListDto input)
    {
      var transferCenters =await _transferCenterManager.GetListAsync(input);

      return transferCenters;
    }

    [Authorize(agency_transfercenterPermissions.Transfercenters.Delete)]
    public async Task DeleteHardAsync(int id)
    {
      await _transferCenterManager.DeleteHardAsync(id);
    }
  }
}
