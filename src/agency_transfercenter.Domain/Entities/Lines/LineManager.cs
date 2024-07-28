using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Exceptions;
using agency_transfercenter.Entities.Stations;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.Entities.Units;
using agency_transfercenter.EntityConsts.LineConsts;
using agency_transfercenter.EntityDtos.LineDtos;
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
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Users;
using agency_transfercenter.EntityConsts.RoleConsts;
using Volo.Abp.Identity;
using Volo.Abp.Data;
using agency_transfercenter.EntityConsts.UserConts;

namespace agency_transfercenter.Entities.Lines
{
  public class LineManager : DomainService
  {
    private readonly ITransferCenterRepository _transferCenterRepository;
    private readonly IRepository<Station> _stationRepository;
    private readonly IAgencyRepository _agencyRepository;
    private readonly ILineRepository _lineRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly ICurrentUser _currentUser;
    private readonly IdentityUserManager _identityUserManager;
    private readonly IdentityRoleManager _identityRoleManager;

    public LineManager(
      IObjectMapper objectMapper,
      ILineRepository lineRepository,
      IRepository<Station> stationRepository,
      ITransferCenterRepository transferCenterRepository,
      IAgencyRepository agencyRepository,
      ICurrentUser currentUser,
      IdentityUserManager identityUserManager,
      IdentityRoleManager identityRoleManager
      )
    {
      _transferCenterRepository = transferCenterRepository;
      _stationRepository = stationRepository;
      _agencyRepository = agencyRepository;
      _lineRepository = lineRepository;
      _objectMapper = objectMapper;
      _currentUser = currentUser;
      _identityUserManager = identityUserManager;
      _identityRoleManager = identityRoleManager;
    }

    #region Line
    public async Task<LineDto> CreateAsync(CreateLineDto createLineDto)//Test edildi
    {
      var isExistLine = await _lineRepository.AnyAsync(x => x.Name == createLineDto.Name);

      if (isExistLine)
        throw new AlreadyExistException(typeof(Line), createLineDto.Name);

      var createdLine = _objectMapper.Map<CreateLineDto, Line>(createLineDto);

      var line = await _lineRepository.InsertAsync(createdLine, autoSave: true);

      var unitIdsLength = createLineDto.UnitId?.Length ?? 0;
      if (unitIdsLength != 0)
        await CreateStationAsync(line, createLineDto.UnitId);

      var lineDto = _objectMapper.Map<Line, LineDto>(line);

      return lineDto;
    }

    public async Task<LineDto> UpdateAsync(int id, UpdateLineDto updateLine)//Teste Gerek Yok
    {
      var isExistName = await _lineRepository.AnyAsync(x => x.Name == updateLine.Name && x.Id != id);

      if (isExistName)
        throw new AlreadyExistException(typeof(Line), updateLine.Name);

      var existingLine = await _lineRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(updateLine, existingLine);

      var line = await _lineRepository.UpdateAsync(existingLine);

      var unitIdsLength = updateLine.UnitId?.Length ?? 0;
      if (unitIdsLength != 0)
        await ReCreateStationAsync(line, updateLine.UnitId);

      var lineDto = _objectMapper.Map<Line, LineDto>(line);

      return lineDto;
    }

    public async Task<PagedResultDto<LineDto>> GetListAsync(GetListPagedAndSortedDto input)//Teste Gerek Yok
    {
      var totalCount = input.Filter == null
        ? await _lineRepository.CountAsync()
        : await _lineRepository.CountAsync(l => l.Name.Contains(input.Filter));

      if (totalCount == 0)
        throw new NotFoundException(typeof(Line), input.Filter ?? string.Empty);

      if (input.SkipCount >= totalCount)
        throw new BusinessException(AtcDomainErrorCodes.RequestLimitsError);

      if (input.Sorting.IsNullOrWhiteSpace())
        input.Sorting = nameof(Line.Name);

      var lineList = await _lineRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

      var lineDtoList = _objectMapper.Map<List<Line>, List<LineDto>>(lineList);

      return new PagedResultDto<LineDto>(totalCount, lineDtoList);
    }

    public async Task<LineWithStationsDto> GetLineWithStationsAsync(int lineId)//Test edildi, Unit test veya Moq
    {
      var line = await _lineRepository.FindAsync(lineId);
      if (line == null)
        throw new NotFoundException(typeof(Line), lineId.ToString());//Test edildi

      var stations = await _stationRepository.GetListAsync(s => s.LineId == lineId);
      if (stations.Count == 0)
        throw new NotFoundException(typeof(Station), lineId.ToString());//Test edildi

      await CheckStationPermitRequest(stations);//Unit test veya Moq

      var lineWithStationsDto = _objectMapper.Map<Line, LineWithStationsDto>(line);

      _objectMapper.Map(stations, lineWithStationsDto.Stations);

      return lineWithStationsDto;
    }

    public async Task CheckStationPermitRequest(List<Station> stations)//Unit test veya Moq
    {
      if (!_currentUser.IsInRole(RoleConst.ViewAllLine))
      {
        var userId = _currentUser.Id.Value;
        var user = await _identityUserManager.GetByIdAsync(userId);
        var userUnitId = user.GetProperty<int>(UserConst.UserUnitId);

        if (!stations.Any(x => x.UnitId == userUnitId))
          throw new BusinessException(AtcDomainErrorCodes.NotAuthForRequest);
      }
    }

    #endregion


    #region Station

    public async Task CreateStationAsync(Line line, int[] unitId)//Test edildi, Unit test
    {
      CheckDuplicateInputs(unitId);//Test edildi

      await CheckCountStation(line.Id, unitId);//validation hatasından dolayı buray test edemedik,  Unit test

      if (line.LineType == LineType.MainLine)//Teste Gerek yok
        await CreateMainLineAsync(line.Id, unitId);


      if (line.LineType == LineType.SubLine)//Test edildi
        await CreateSubLineAsync(line.Id, unitId);
    }

    public async Task ReCreateStationAsync(Line line, int[] unitId)//Teste gerek yok
    {
      var isExistStation = await _stationRepository.GetListAsync(s => s.LineId == line.Id);
      if (isExistStation.Count() != 0)
        await _stationRepository.DeleteManyAsync(isExistStation, autoSave: true);

      await CreateStationAsync(line, unitId);
    }

    internal async Task CreateMainLineAsync(int lineId, int[] unitId)//Teste Gerek yok
    {
      var stationNumber = 1;
      foreach (var unit in unitId)
      {
        if (!await _transferCenterRepository.AnyAsync(tc => tc.Id == unit))
          throw new NotFoundException(typeof(TransferCenter), unit.ToString());

        await _stationRepository.InsertAsync(
              new Station(lineId, unit, stationNumber));

        stationNumber++;
      }
    }

    internal async Task CreateSubLineAsync(int lineId, int[] unitId)//Test edildi
    {
      unitId = await CheckStationInputsValid(lineId, unitId);

      var stationNumber = 1;
      foreach (var unit in unitId)
      {
        await _stationRepository.InsertAsync(
               new Station(lineId, unit, stationNumber));

        stationNumber++;
      }
    }

    internal async Task<int[]> CheckStationInputsValid(int lineId, int[] unitId)//Test edildi
    {
      var queryableTransferCenter = await _transferCenterRepository.GetQueryableAsync();

      var transferCenterIds = await queryableTransferCenter
         .Where(tc => unitId.Contains(tc.Id))
         .Select(tc => tc.Id).ToListAsync();

      if (transferCenterIds.Count() == 0 || transferCenterIds.Count() > 1)
        throw new BusinessException(AtcDomainErrorCodes.MustHaveOneTransferCenter);

      var transferCenterId = transferCenterIds.First();

      var transferCenterWithAgencyIds = new int[] { transferCenterId }
          .Concat((await _agencyRepository.GetListAsync(l => l.TransferCenterId == transferCenterId))
          .Select(l => l.Id))
          .ToArray();

      unitId = new int[] { transferCenterId }
          .Concat(unitId.Where(u => u != transferCenterId))
          .ToArray();

      var allInTransferCenterAgencies = unitId.All(u => transferCenterWithAgencyIds.Contains(u));

      if (!allInTransferCenterAgencies)
        throw new BusinessException(AtcDomainErrorCodes.AgenciesMustBeAffiliatedToTheTransferCenter);

      return unitId;
    }

    internal void CheckDuplicateInputs(int[] unitId)//Test edildi
    {
      var duplicates = unitId.GroupBy(x => x)
        .Where(u => u.Count() > 1).Select(u => u.Key).ToList();

      if (duplicates.Count > 0)
      {
        var duplicateUnits = string.Join(", ", duplicates);
        throw new BusinessException(AtcDomainErrorCodes.RepeatedDataError).WithData("repeat", duplicateUnits);
      }
    }

    internal async Task<int> StationNumberGenerator(int lineId)//Kullanılmadı
    {
      if (await _stationRepository.CountAsync(x => x.LineId == lineId) <= 0)
        return 1;

      var stations = await _stationRepository.GetQueryableAsync();

      var maxStationNumber = stations
          .Where(x => x.LineId == lineId)
          .Max(x => x.StationNumber);

      if (maxStationNumber >= LineConst.LimitOfStation)
        throw new BusinessException(AtcDomainErrorCodes.StationLimitError)
          .WithData("0", LineConst.LimitOfStation).WithData("1", maxStationNumber);

      return maxStationNumber++;
    }

    internal async Task CheckCountStation(int lineId, int[]? unitId)//validation hatası test edemedik, Unit test
    {
      var lineCount = await _stationRepository.CountAsync(s => s.LineId == lineId);

      lineCount += unitId?.Length ?? 0;

      if (lineCount > LineConst.LimitOfStation)
        throw new BusinessException(AtcDomainErrorCodes.StationLimitError)
            .WithData("0", LineConst.LimitOfStation).WithData("1", lineCount);
    }

    #endregion

  }
}
