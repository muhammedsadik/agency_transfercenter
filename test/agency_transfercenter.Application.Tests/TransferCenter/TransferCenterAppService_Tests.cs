using agency_transfercenter.AppServices.TransferCenters;
using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.EntityDtos.AddressDtos;
using agency_transfercenter.EntityDtos.TransferCenterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
      };

    }

    #region 

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
      var _updateTransferCenterDto = _objectMapper.Map<TransferCenterDto, UpdateTransferCenterDto>(_transferCenterDto);
      _updateTransferCenterDto.UnitName = "Ankara Tranfer Center";

      await Assert.ThrowsAsync<AlreadyExistException>(async () =>
      {
        await _transferCenterAppService.UpdateAsync(_updateTransferCenterDto);
      });

    }


    #endregion

  }
}
