using agency_transfercenter;
using agency_transfercenter.AppServices.Agencies;
using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.EntityDtos.AddressDtos;
using agency_transfercenter.EntityDtos.AgencyDtos;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;
using Xunit;
using Xunit.Abstractions;

namespace agency_transfercenter.Agency
{
  public abstract class AgencyAppService_Test<TStartupModule> : agency_transfercenterApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
  {
    private readonly AgencyAppService _agencyAppService;
    private readonly IObjectMapper _objectMapper;
    private readonly AgencyDto _agencyDto;
    private readonly IAgencyRepository _agencyRepository;

    public AgencyAppService_Test()
    {
      _agencyAppService = GetRequiredService<AgencyAppService>();
      _agencyRepository = GetRequiredService<IAgencyRepository>();
      _objectMapper = GetRequiredService<IObjectMapper>();

      _agencyDto = new()
      {
        UnitName = "Mardin Agency",
        UnitPhone = "01234567891",
        UnitMail = "testagency@gmail.com",
        ManagerName = "Ali",
        ManagerSurname = "Can",
        ManagerGsm = "01234567891",
        ManagerMail = "alican@gmail.com",
        Address = new AddressDto
        {
          City = "Mardin",
          Street = "Midyat",
          Number = "48/C"
        },
        TransferCenterId = 2
      };

    }


    #region CreateAsync

    [Fact]
    public async Task CreateAsync_UnitNameExist_AlreadyExistException()
    {
      var _createAgencyDto = _objectMapper.Map<AgencyDto, CreateAgencyDto>(_agencyDto);
      _createAgencyDto.UnitName = "İstanbul Kurtköy Agency";

      await Assert.ThrowsAsync<AlreadyExistException>(async () =>
      {
        await _agencyAppService.CreateAsync(_createAgencyDto);
      });

    }

    [Fact]
    public async Task CreateAsync_TransferCenterIdInValid_NotFoundException()
    {
      var _createAgencyDto = _objectMapper.Map<AgencyDto, CreateAgencyDto>(_agencyDto);
      _createAgencyDto.TransferCenterId = 5;

      await Assert.ThrowsAsync<NotFoundException>(async () =>
      {
        await _agencyAppService.CreateAsync(_createAgencyDto);
      });
    }

    [Fact]
    public async Task CreateAsync_ReturnValue_AgencyDto()
    {
      var _createAgencyDto = _objectMapper.Map<AgencyDto, CreateAgencyDto>(_agencyDto);
      var result = await _agencyAppService.CreateAsync(_createAgencyDto);

      Assert.NotNull(result);
      Assert.IsType<AgencyDto>(result);
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_AgencyExistInValid_AlreadyExistException()
    {
      var UpdateAgencyId = 5;// Ankara Sincan Agency => Id
      var updateAgencyDto = _objectMapper.Map<AgencyDto, UpdateAgencyDto>(_agencyDto);
      updateAgencyDto.UnitName = "İstanbul Kurtköy Agency";

      await Assert.ThrowsAsync<AlreadyExistException>(async () =>
      {
        await _agencyAppService.UpdateAsync(UpdateAgencyId, updateAgencyDto);
      });
    }

    [Fact]
    public async Task UpdateAsync_TransferCenterId_NotFoundException()
    {
      var updateAgencyId = 6; //İstanbul Kurtköy Agency => id
      var updateAgencyDto = _objectMapper.Map<AgencyDto, UpdateAgencyDto>(_agencyDto);
      updateAgencyDto.UnitName = "İstanbul Kurtköy Agency";
      updateAgencyDto.TransferCenterId = 5;

      await Assert.ThrowsAsync<NotFoundException>(async () =>
      {
        await _agencyAppService.UpdateAsync(updateAgencyId, updateAgencyDto);
      });
    }

    [Fact]
    public async Task UpdateAsync_ReturnValue_AgencyDto()
    {
      var updateAgencyId = 6; //İstanbul Kurtköy Agency => id
      var updateAgencyDto = _objectMapper.Map<AgencyDto, UpdateAgencyDto>(_agencyDto);
      updateAgencyDto.UnitName = "İstanbul Kurtköy Agency";

      var result = await _agencyAppService.UpdateAsync(updateAgencyId, updateAgencyDto);

      Assert.NotNull(result);
      Assert.IsType<AgencyDto>(result);
    }

    #endregion

    #region GetListAsync

    [Fact]
    public async Task GetListAsync_FilterNotExist_NotFoundException()
    {
      await Assert.ThrowsAsync<NotFoundException>(async () =>
      {
        await _agencyAppService.GetListAsync(new GetListPagedAndSortedDto() { Filter = "Bingöl" });
      });
    }

    [Fact]
    public async Task GetListAsync_Retrun_FilterData()
    {
      var agencyList = await _agencyAppService.GetListAsync(new GetListPagedAndSortedDto() { Filter = "Diyarbakır" });

      agencyList.Items.ShouldContain(u => u.UnitName == "Diyarbakır Suriçi Agency");
    }

    [Fact]
    public async Task GetListAsync_SkipCountBiggerThanTotalCount_BusinessException()
    {
      await Assert.ThrowsAsync<BusinessException>(async () =>
      {
        await _agencyAppService.GetListAsync(new GetListPagedAndSortedDto() { SkipCount = 15 });
      });
    }

    [Fact]
    public async Task GetListAsync_ReturnValue_GratherThanZero()
    {
      var agencyList = await _agencyAppService.GetListAsync(new GetListPagedAndSortedDto());

      agencyList.TotalCount.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetListAsync_ReturnValue_PagedResultDto()
    {
      var agencyList = await _agencyAppService.GetListAsync(new GetListPagedAndSortedDto());

      Assert.IsType<PagedResultDto<AgencyDto>>(agencyList);
    }

    #endregion

    #region DeleteHardAsync

    [Fact]
    public async Task DeleteHardAsync_AgencyIdInvalid_EntityNotFoundException()
    {
      await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
      {
        await _agencyAppService.DeleteHardAsync(15);
      });
    }

    [Fact]
    public async Task DeleteHardAsync_AgencyIdvalid_DeletingEntity()
    {
      var agency = await _agencyRepository.GetAsync(10);
      agency.ShouldNotBeNull();

      await _agencyAppService.DeleteHardAsync(agency.Id);
      await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
      {
        await _agencyRepository.GetAsync(10);
      });
    }

    #endregion
  }
}
