using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.Stations;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.EntityConsts.RoleConsts;
using agency_transfercenter.EntityConsts.UserConts;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Xunit;

namespace agency_transfercenter.Line
{
  public abstract class LineManager_Tests<TStartupModule> : agency_transfercenterDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
  {
    private readonly LineManager _lineManager;
    private readonly IRepository<Station> _stationRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IObjectMapper _objectMapper;
    private readonly IRepository<IdentityUser> _userRepository;
    private readonly ITransferCenterRepository _transferCenterRepository;
    private readonly IAgencyRepository _agencyRepository;
    private readonly ILineRepository _lineRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly List<Station> _stations;

    protected LineManager_Tests()
    {
      _objectMapper = Substitute.For<IObjectMapper>();
      _currentUser = Substitute.For<ICurrentUser>();
      _stationRepository = Substitute.For<IRepository<Station>>();
      _userRepository = GetRequiredService<IRepository<IdentityUser>>();
      _lineRepository = Substitute.For<ILineRepository>();
      _transferCenterRepository = Substitute.For<ITransferCenterRepository>();
      _agencyRepository = Substitute.For<IAgencyRepository>();
      _guidGenerator = Substitute.For<IGuidGenerator>();

      _lineManager = new LineManager(
        _objectMapper,
        _lineRepository,
        _stationRepository,
        _transferCenterRepository,
        _agencyRepository,
        _currentUser,
        _userRepository
      );

      _stations = new List<Station>()
      {
        new Station {LineId = 1, UnitId = 1, StationNumber = 1},
        new Station {LineId = 1, UnitId = 2, StationNumber = 2},
        new Station {LineId = 1, UnitId = 3, StationNumber = 3},
        new Station {LineId = 1, UnitId = 4, StationNumber = 4}
      };
    }

    [Fact]               
    public async Task GetLineWithStationsAsync_CheckStationPermitRequest_UserRoleValid_NoAction()
    {
      _currentUser.IsInRole(RoleConst.ViewAllLine).Returns(true);

      var exception = await Record.ExceptionAsync(async () =>
      {
        await _lineManager.CheckStationPermitRequest(_stations);
      });

      Assert.Null(exception);
    }

    [Fact]           
    public async Task GetLineWithStationsAsync_CheckStationPermitRequest_UserUnitInValid_BusinessException()
    {
      var userMehmet = await _userRepository.GetAsync(u => u.Email == "mehmet@gmail.com");//unit id ye sahip değil

      _currentUser.IsInRole(RoleConst.ViewAllLine).Returns(false);

      _currentUser.Id.Returns(userMehmet.Id);

      await Assert.ThrowsAsync<BusinessException>(async () =>
      {
        await _lineManager.CheckStationPermitRequest(_stations);
      });
    }

    [Fact]              
    public async Task GetLineWithStationsAsync_CheckStationPermitRequest_UserInUnit_NoAction()
    {
      var userOmer = await _userRepository.GetAsync(u => u.Email == "omer@gmail.com");//4 numara unit id ye sahip

      _currentUser.IsInRole(RoleConst.ViewAllLine).Returns(false);

      _currentUser.Id.Returns(userOmer.Id);

      var exception = await Record.ExceptionAsync(async () =>
      {
        await _lineManager.CheckStationPermitRequest(_stations);
      });

      Assert.Null(exception);
    }


  }
}
