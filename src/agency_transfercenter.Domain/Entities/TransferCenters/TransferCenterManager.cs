using agency_transfercenter.EntityDtos.TransferCenterDtos;
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

      var ExistingTransferCenter = await _transferCenterRepository.FindAsync(x => x.UnitName == createTransferCenterDto.UnitName);

      if (ExistingTransferCenter != null)
        throw new TransferCeneterAlreadyExistsException(ExistingTransferCenter.UnitName);

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
        throw new TransferCeneterAlreadyExistsException(checkNameIsExist.UnitName);

      var existingTransferCenter = await _transferCenterRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(updateTransferCenter, existingTransferCenter);

      var transferCenter = await _transferCenterRepository.UpdateAsync(existingTransferCenter);

      var transferCenterDto = _objectMapper.Map<TransferCenter, TransferCenterDto>(transferCenter);

      return transferCenterDto;
    }

    public async Task<TransferCenterDto> UpdateManagerAsync(int id, UpdateManagerOfTransferCenterDto managerTransferCenter)
    {
      Check.NotNullOrWhiteSpace(managerTransferCenter.ManagerName, nameof(managerTransferCenter.ManagerName));

      var checkManagerExist = await _transferCenterRepository.FindAsync(x => x.ManagerName == managerTransferCenter.ManagerName && x.ManagerSurname == managerTransferCenter.ManagerSurname);

      if (checkManagerExist != null)
        throw new TransferCeneterAlreadyExistsException(checkManagerExist.ManagerName);//bunun mesajını değiştir(Manager için yap)

      var existingTransferCenter = await _transferCenterRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(managerTransferCenter, existingTransferCenter);

      var transferCenter = await _transferCenterRepository.UpdateAsync(existingTransferCenter);

      var transferCenterDto = _objectMapper.Map<TransferCenter, TransferCenterDto>(transferCenter);

      return transferCenterDto;
    }

    public async Task<TransferCenterDto> UpdateUnitAsync(int id, UpdateUnitOfTransferCenterDto unitTransferCenter)
    {
      Check.NotNullOrWhiteSpace(unitTransferCenter.UnitName, nameof(unitTransferCenter.UnitName));
      
      var checkUnitExist = await _transferCenterRepository.FindAsync(x => x.UnitName == unitTransferCenter.UnitName && x.Id != id);

      if (checkUnitExist != null)
        throw new TransferCeneterAlreadyExistsException(checkUnitExist.UnitName);
      
      var existingTransferCenter = await _transferCenterRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(unitTransferCenter, existingTransferCenter);

      var transferCenter = await _transferCenterRepository.UpdateAsync(existingTransferCenter);

      var transferCenterDto = _objectMapper.Map<TransferCenter, TransferCenterDto>(transferCenter);

      return transferCenterDto;
    }

    public async Task<PagedResultDto<TransferCenterDto>> GetListAsync(GetTransferCenterListDto input)
    {
      if (input.Sorting.IsNullOrWhiteSpace())
        input.Sorting = nameof(TransferCenter.UnitName);

      var transferCenterList = await _transferCenterRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

      var totalCount = input.Filter == null
        ? await _transferCenterRepository.CountAsync()
        : await _transferCenterRepository.CountAsync(tc => tc.UnitName.Contains(input.Filter));

      var transferCenterDtoList = _objectMapper.Map<List<TransferCenter>, List<TransferCenterDto>>(transferCenterList);

      return new PagedResultDto<TransferCenterDto>(totalCount, transferCenterDtoList);
    }

    
    public async Task DeleteHardAsync(int id)
    {
      var transferCenter =await _transferCenterRepository.GetAsync(x => x.Id == id);

      await _transferCenterRepository.HardDeleteAsync(transferCenter);
    }

  }
}
