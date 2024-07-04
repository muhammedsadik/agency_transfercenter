using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using agency_transfercenter.EntityDtos.TransferCenterDtos;
using agency_transfercenter.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;

namespace agency_transfercenter.Entities.TransferCenters
{
  public class TransferCenterManager : DomainService
  {
    private readonly ITransferCenterRepository _transferCenterRepository;
    private readonly IObjectMapper _objectMapper;

    public TransferCenterManager(ITransferCenterRepository transferCenterRepsitory, IObjectMapper objectMapper)
    {
      _transferCenterRepository = transferCenterRepsitory;
      _objectMapper = objectMapper;
    }

    public async Task<TransferCenterDto> CreateAsync(CreateTransferCenterDto createTransferCenterDto)
    {
      Check.NotNullOrWhiteSpace(createTransferCenterDto.UnitName, nameof(createTransferCenterDto.UnitName));

      var existingTransferCenter = await _transferCenterRepository.FindAsync(x => x.UnitName == createTransferCenterDto.UnitName);


      if (existingTransferCenter != null)
        throw new AlreadyExistException(typeof(TransferCenter), existingTransferCenter.UnitName);

      var createdTransferCenter = _objectMapper.Map<CreateTransferCenterDto, TransferCenter>(createTransferCenterDto);

      var transferCenter = await _transferCenterRepository.InsertAsync(createdTransferCenter);

      var transferCenterDto = _objectMapper.Map<TransferCenter, TransferCenterDto>(transferCenter);

      return transferCenterDto;
    }

    public async Task<TransferCenterDto> UpdateAsync(int id, UpdateTransferCenterDto updateTransferCenter)
    {
      Check.NotNullOrWhiteSpace(updateTransferCenter.UnitName, nameof(updateTransferCenter.UnitName));

      var checkNameIsExist = await _transferCenterRepository.FindAsync(x => x.UnitName == updateTransferCenter.UnitName && x.Id != id);

      if (checkNameIsExist != null)
        throw new AlreadyExistException(typeof(TransferCenter), checkNameIsExist.UnitName);

      var existingTransferCenter = await _transferCenterRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(updateTransferCenter, existingTransferCenter);

      var transferCenter = await _transferCenterRepository.UpdateAsync(existingTransferCenter);

      var transferCenterDto = _objectMapper.Map<TransferCenter, TransferCenterDto>(transferCenter);

      return transferCenterDto;
    }

    public async Task<PagedResultDto<TransferCenterDto>> GetListAsync(GetListPagedAndSortedDto input)
    {
      if (input.Sorting.IsNullOrWhiteSpace())
        input.Sorting = nameof(TransferCenter.UnitName);

      var totalCount = input.Filter == null
        ? await _transferCenterRepository.CountAsync()
        : await _transferCenterRepository.CountAsync(tc => tc.UnitName.Contains(input.Filter));

      if (input.SkipCount >= totalCount)
        throw new BusinessException(TCDomainErrorCodes.RequestLimitsError);

      var transferCenterList = await _transferCenterRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);


      var transferCenterDtoList = _objectMapper.Map<List<TransferCenter>, List<TransferCenterDto>>(transferCenterList);

      return new PagedResultDto<TransferCenterDto>(totalCount, transferCenterDtoList);
    }

    public async Task DeleteHardAsync(int id)
    {
      var transferCenter = await _transferCenterRepository.GetAsync(x => x.Id == id);

      await _transferCenterRepository.HardDeleteAsync(transferCenter);
    }

  }
}
