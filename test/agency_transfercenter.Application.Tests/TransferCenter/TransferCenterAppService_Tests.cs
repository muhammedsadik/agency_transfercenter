using agency_transfercenter.AppServices.TransferCenters;
using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.EntityDtos.AddressDtos;
using agency_transfercenter.EntityDtos.AgencyDtos;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using agency_transfercenter.EntityDtos.TransferCenterDtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace agency_transfercenter.TransferCenter
{
  public abstract class TransferCenterAppService_Tests<TStartupModule> : agency_transfercenterApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
  {
    private readonly TransferCenterAppService _transferCenterAppService;
    private readonly IObjectMapper _objectMapper;
    private readonly TransferCenterDto _transferCenterDto;

    public TransferCenterAppService_Tests()
    {
      _transferCenterAppService = GetRequiredService<TransferCenterAppService>();
      _objectMapper = GetRequiredService<IObjectMapper>();

      _transferCenterDto = new()
      {
        UnitName = "Mardin Transfer Center",
        UnitPhone = "01234567891",
        UnitMail = "testtransfercenter@gmail.com",
        ManagerName = "Ali",
        ManagerSurname = "Can",
        ManagerGsm = "01234567891",
        ManagerMail = "alican@gmail.com",
        Address = new AddressDto
        {
          City = "Mardin",
          Street = "Midyat",
          Number = "48/C"
        }
      };

    }


    [Fact]
    public async Task CreateAsync_UnitNameExist_AlreadyExistException()
    {
      var _createTransferCenterDto = _objectMapper.Map<TransferCenterDto, CreateTransferCenterDto>(_transferCenterDto);
      _createTransferCenterDto.UnitName = "Ankara Tranfer Center";

      await Assert.ThrowsAsync<AlreadyExistException>(async () =>
      {
        await _transferCenterAppService.CreateAsync(_createTransferCenterDto);
      });
    }
    
    [Fact]
    public async Task UpdateAsync_UnitNameExist_AlreadyExistException()
    {
      var UpdateAgencyId = 1;// Ankara Tranfer Center => Id
      var _updateTransferCenterDto = _objectMapper.Map<TransferCenterDto, UpdateTransferCenterDto>(_transferCenterDto);
      _updateTransferCenterDto.UnitName = "İstanbul Tranfer Center";

      await Assert.ThrowsAsync<AlreadyExistException>(async () =>
      {
        await _transferCenterAppService.UpdateAsync(UpdateAgencyId, _updateTransferCenterDto);
      });

    }

    [Fact]
    public async Task GetListAsync_ReturnValue_GratherThanZero()
    {
      var transferCenterList = await _transferCenterAppService.GetListAsync(new GetListPagedAndSortedDto());

      transferCenterList.TotalCount.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task DeleteHardAsync_TransferCenterIdvalid_DeletingEntity()
    {
      var transferCenter = await _transferCenterAppService.GetAsync(1);
      transferCenter.ShouldNotBeNull();

      await _transferCenterAppService.DeleteHardAsync(transferCenter.Id);
      await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
      {
        await _transferCenterAppService.GetAsync(1);
      });
    }
    
    [Fact]
    public async Task DeleteHardAsync_TransferCenterIdInValid_EntityNotFoundException()
    {
      await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
      {
        await _transferCenterAppService.DeleteHardAsync(144);
      });
    }

    [Fact]
    public async Task GetListAgenciesByTransferCenterIdAsync_TransferCenterIdInValid_NotFoundException()
    {
      var transferCenterId = 5;

      await Assert.ThrowsAsync<NotFoundException>(async () =>
      {
        await _transferCenterAppService.GetListAgenciesByTransferCenterIdAsync(transferCenterId);
      });
    }

    [Fact]
    public async Task GetListAgenciesByTransferCenterIdAsync_ReturnData_PagedResultDto()
    {
      var transferCenterId = 1;

      var result =  await _transferCenterAppService.GetListAgenciesByTransferCenterIdAsync(transferCenterId);

      Assert.IsType<PagedResultDto<AgencyDto>>(result);
    }

  }
}
