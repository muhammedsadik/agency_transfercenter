using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace agency_transfercenter.EntityDtos.TransferCenterDtos
{
  public interface ITransferCenterAppService : ICrudAppService<TransferCenterDto, int, GetTransferCenterListDto, CreateTransferCenterDto, UpdateTransferCenterDto>
  {
    Task<TransferCenterDto> UpdateUnitOfTransferCenterAsync(int id, UpdateUnitOfTransferCenterDto input);
    Task<TransferCenterDto> UpdateManagerOfTransferCenterAsync(int id, UpdateManagerOfTransferCenterDto input);

    Task DeleteHardAsync(int id);
  }
}
