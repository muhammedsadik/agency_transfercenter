using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.EntityDtos.AgencyDtos;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;

namespace agency_transfercenter.Entities.Agencies
{
  public class AgencyManager : DomainService
  {
    private readonly ITransferCenterRepository _transferCenterRepository;
    private readonly IAgencyRepository _agencyRepository;
    private readonly IObjectMapper _objectMapper;

    public AgencyManager(IAgencyRepository agencyRepository, IObjectMapper objectMapper, ITransferCenterRepository transferCenterRepository)
    {
      _transferCenterRepository = transferCenterRepository;
      _agencyRepository = agencyRepository;
      _objectMapper = objectMapper;
    }

    public async Task<AgencyDto> CreateAsync(CreateAgencyDto createAgencyDto)
    {
      var IsExistAgency = await _agencyRepository.AnyAsync(x => x.UnitName == createAgencyDto.UnitName);

      if (IsExistAgency)
        throw new AlreadyExistException(typeof(Agency), createAgencyDto.UnitName);

      var IsExistTransferCenter = await _transferCenterRepository.AnyAsync(x=> x.Id == createAgencyDto.TransferCenterId);

      if (!IsExistTransferCenter)
        throw new NotFoundException(typeof(TransferCenter), createAgencyDto.TransferCenterId.ToString());

      var createdAgency = _objectMapper.Map<CreateAgencyDto, Agency>(createAgencyDto);

      var agency = await _agencyRepository.InsertAsync(createdAgency);

      var agencyDto = _objectMapper.Map<Agency, AgencyDto>(agency);

      return agencyDto;
    }

    public async Task<AgencyDto> UpdateAsync(int id, UpdateAgencyDto updateAgency)
    {
      var IsExistAgency = await _agencyRepository.AnyAsync(x => x.UnitName == updateAgency.UnitName && x.Id != id);

      if (IsExistAgency)
        throw new AlreadyExistException(typeof(Agency), updateAgency.UnitName);

      var IsExistTransferCenter = await _transferCenterRepository.AnyAsync(x => x.Id == updateAgency.TransferCenterId);

      if (!IsExistTransferCenter)
        throw new NotFoundException(typeof(TransferCenter), updateAgency.TransferCenterId.ToString());

      var existingAgency = await _agencyRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(updateAgency, existingAgency);

      var agency = await _agencyRepository.UpdateAsync(existingAgency);

      var agencyDto = _objectMapper.Map<Agency, AgencyDto>(agency);

      return agencyDto;
    }

    public async Task<PagedResultDto<AgencyDto>> GetListAsync(GetListPagedAndSortedDto input)
    {
      var totalCount = input.Filter == null
        ? await _agencyRepository.CountAsync()
        : await _agencyRepository.CountAsync(a => a.UnitName.Contains(input.Filter));

      if (totalCount == 0)
        throw new NotFoundException(typeof(Agency), input.Filter ?? string.Empty);

      if (input.SkipCount >= totalCount)
        throw new BusinessException(AtcDomainErrorCodes.RequestLimitsError);

      if (input.Sorting.IsNullOrWhiteSpace())
        input.Sorting = nameof(Agency.UnitName);

      var agencyList = await _agencyRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

      var agencyDtoList = _objectMapper.Map<List<Agency>, List<AgencyDto>>(agencyList);

      return new PagedResultDto<AgencyDto>(totalCount, agencyDtoList);
    }

    public async Task DeleteHardAsync(int id)
    {
      var transferCenter = await _agencyRepository.GetAsync(x => x.Id == id);

      await _agencyRepository.HardDeleteAsync(transferCenter);
    }



  }
}
