using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace agency_transfercenter.Line
{
  public abstract class LineManager_Tests<TStartupModule> : agency_transfercenterDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
  {
    private readonly LineManager _lineManager;
    private readonly IRepository<Station> _stationRepository;

    protected LineManager_Tests()
    {
      _lineManager = GetRequiredService<LineManager>();

    }

    [Fact]                //User rolde var
    public async Task GetLineWithStationsAsync_CheckStationPermitRequest_UserRoleValid_BusinessException()
    {

    }

    [Fact]                //User Stationda yok => geçersiz istek
    public async Task GetLineWithStationsAsync_CheckStationPermitRequest_UserInValid_BusinessException()
    {

    }
    
    [Fact]               //User Stationda var
    public async Task GetLineWithStationsAsync_CheckStationPermitRequest_UserInUnit_BusinessException()
    {

    }


  }
}
