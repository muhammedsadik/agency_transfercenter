using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.EntityDtos.AgencyDtos;
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
    private readonly IAgencyRepository _agencyRepository;
    private readonly IObjectMapper _objectMapper;

    public TransferCenterManager(ITransferCenterRepository transferCenterRepsitory, IObjectMapper objectMapper, IAgencyRepository agencyRepository)
    {
      _transferCenterRepository = transferCenterRepsitory;
      _agencyRepository = agencyRepository;
      _objectMapper = objectMapper;
    }

    public async Task<TransferCenterDto> CreateAsync(CreateTransferCenterDto createTransferCenterDto)
    {
      var isExistTransferCenter = await _transferCenterRepository.AnyAsync(x => x.UnitName == createTransferCenterDto.UnitName);

      if (isExistTransferCenter)
        throw new AlreadyExistException(typeof(TransferCenter), createTransferCenterDto.UnitName);

      var createdTransferCenter = _objectMapper.Map<CreateTransferCenterDto, TransferCenter>(createTransferCenterDto);

      var transferCenter = await _transferCenterRepository.InsertAsync(createdTransferCenter);

      var transferCenterDto = _objectMapper.Map<TransferCenter, TransferCenterDto>(transferCenter);

      return transferCenterDto;
    }

    public async Task<TransferCenterDto> UpdateAsync(int id, UpdateTransferCenterDto updateTransferCenter)
    {
      var isExistName = await _transferCenterRepository.AnyAsync(x => x.UnitName == updateTransferCenter.UnitName && x.Id != id);

      if (isExistName)
        throw new AlreadyExistException(typeof(TransferCenter), updateTransferCenter.UnitName);

      var existingTransferCenter = await _transferCenterRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(updateTransferCenter, existingTransferCenter);

      var transferCenter = await _transferCenterRepository.UpdateAsync(existingTransferCenter);

      var transferCenterDto = _objectMapper.Map<TransferCenter, TransferCenterDto>(transferCenter);

      return transferCenterDto;
    }

    public async Task<PagedResultDto<TransferCenterDto>> GetListAsync(GetListPagedAndSortedDto input)
    {
      var totalCount = input.Filter == null
        ? await _transferCenterRepository.CountAsync()
        : await _transferCenterRepository.CountAsync(tc => tc.UnitName.Contains(input.Filter));

      if (totalCount == 0)
        throw new NotFoundException(typeof(TransferCenter), input.Filter ?? string.Empty);

      if (input.SkipCount >= totalCount)
        throw new BusinessException(AtcDomainErrorCodes.RequestLimitsError);

      if (input.Sorting.IsNullOrWhiteSpace())
        input.Sorting = nameof(TransferCenter.UnitName);

      var transferCenterList = await _transferCenterRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

      var transferCenterDtoList = _objectMapper.Map<List<TransferCenter>, List<TransferCenterDto>>(transferCenterList);

      return new PagedResultDto<TransferCenterDto>(totalCount, transferCenterDtoList);
    }

    public async Task DeleteHardAsync(int id)
    {
      var transferCenter = await _transferCenterRepository.GetAsync(x => x.Id == id);

      await _transferCenterRepository.HardDeleteAsync(transferCenter);
    }


    public async Task<PagedResultDto<AgencyDto>> GetListAgenciesByTransferCenterIdAsync(int id)
    {
      var agencies = await _agencyRepository.GetListAsync(x => x.TransferCenterId == id);

      var agencyDto = _objectMapper.Map<List<Agency>, List<AgencyDto>>(agencies);

      var pagedAgencyDto = new PagedResultDto<AgencyDto>(12, agencyDto);

      var totalCount = await _agencyRepository.CountAsync(a=> a.TransferCenterId == id);

      return new PagedResultDto<AgencyDto>(totalCount, agencyDto);
    }

  }
}
