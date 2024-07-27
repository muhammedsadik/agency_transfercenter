using agency_transfercenter.AppServices.Lines;
using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.EntityConsts.LineConsts;
using agency_transfercenter.EntityDtos.LineDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;
using Xunit;

namespace agency_transfercenter.Line
{
  public abstract class LineAppService_Tests<TStartupModule> : agency_transfercenterApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
  {
    private readonly LineAppService _lineAppService;
    private readonly CreateLineDto _createLineDto;

    public LineAppService_Tests()
    {
      _lineAppService = GetRequiredService<LineAppService>();

      _createLineDto = new CreateLineDto()
      {
        Name = "line 5",
        IsActive = true,
        LineType = LineType.SubLine,
        UnitId = [1,5]
      };
    }

    #region Line

    [Fact]
    public async Task CreateAsync_LineInValid_AlreadyExistException()
    {
      _createLineDto.Name = "line 2";

      await Assert.ThrowsAsync<AlreadyExistException>(async () =>
      {
        await _lineAppService.CreateAsync(_createLineDto);
      });
    }

    [Fact]
    public async Task GetLineWithStationsAsync_LineInValid_NotFoundException()
    {
      var LineId = 5;

      await Assert.ThrowsAsync<NotFoundException>(async () =>
      {
        await _lineAppService.GetLineWithStationsAsync(LineId);
      });
    }
    
    [Fact]
    public async Task GetLineWithStationsAsync_StationInValid_NotFoundException()
    {
      var LineId = 4;

      await Assert.ThrowsAsync<NotFoundException>(async () =>
      {
        await _lineAppService.GetLineWithStationsAsync(LineId);
      });
    }

    #endregion

    #region Station
    
    [Fact]
    public async Task CreateAsync_CreateStationAsync_CheckDuplicateInputsInValid_BusinessException()
    {
      _createLineDto.UnitId = [1,5,5];

      await Assert.ThrowsAsync<BusinessException>(async () =>
      {
        await _lineAppService.CreateAsync(_createLineDto);
      });

    }
    
    [Fact]
    public async Task CreateAsync_CreateStationAsync_CheckStationInputsValid_StationInputInValid_BusinessException()
    {
      _createLineDto.UnitId = [1,5,6];

      await Assert.ThrowsAsync<BusinessException>(async () =>
      {
        await _lineAppService.CreateAsync(_createLineDto);
      });
    }
    
    [Fact]
    public async Task CreateAsync_CreateStationAsync_CheckStationInputsValid_MoreThanOneTransferCenter_BusinessException()
    {
      _createLineDto.UnitId = [1,5,2];

      await Assert.ThrowsAsync<BusinessException>(async () =>
      {
        await _lineAppService.CreateAsync(_createLineDto);
      });
    }

    #endregion
  }
}
