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

namespace agency_transfercenter.Entities.Lines
{
  public class LineManager : DomainService
  {
    private readonly ITransferCenterRepository _transferCenterRepository;
    private readonly IRepository<Station> _stationRepository;
    private readonly IAgencyRepository _agencyRepository;
    private readonly ILineRepository _lineRepository;
    private readonly IObjectMapper _objectMapper;

    public LineManager(IObjectMapper objectMapper, ILineRepository lineRepository, IRepository<Station> stationRepository, ITransferCenterRepository transferCenterRepository, IAgencyRepository agencyRepository)
    {
      _transferCenterRepository = transferCenterRepository;
      _stationRepository = stationRepository;
      _agencyRepository = agencyRepository;
      _lineRepository = lineRepository;
      _objectMapper = objectMapper;
    }

    #region Line
    public async Task<LineDto> CreateAsync(CreateLineDto createLineDto)
    {
      var isExistLine = await _lineRepository.AnyAsync(x => x.Name == createLineDto.Name);

      if (isExistLine)
        throw new AlreadyExistException(typeof(Line), createLineDto.Name);

      var createdLine = _objectMapper.Map<CreateLineDto, Line>(createLineDto);

      var line = await _lineRepository.InsertAsync(createdLine,autoSave:true);

      var unitIdsLength = createLineDto.UnitId?.Length ?? 0;
      if (unitIdsLength != 0)
        await CreateStationAsync(line, createLineDto.UnitId);

      var lineDto = _objectMapper.Map<Line, LineDto>(line);

      return lineDto;
    }

    public async Task<LineDto> UpdateAsync(int id, UpdateLineDto updateLine)
    {
      var isExistName = await _lineRepository.AnyAsync(x => x.Name == updateLine.Name && x.Id != id);

      if (isExistName)
        throw new AlreadyExistException(typeof(Line), updateLine.Name);

      var existingLine = await _lineRepository.GetAsync(x => x.Id == id);

      _objectMapper.Map(updateLine, existingLine);

      var line = await _lineRepository.UpdateAsync(existingLine);

      var lineDto = _objectMapper.Map<Line, LineDto>(line);

      return lineDto;
    }

    public async Task<PagedResultDto<LineDto>> GetListAsync(GetListPagedAndSortedDto input)
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

    #endregion


    #region Station

    public async Task CreateStationAsync(Line line, int[] unitId)
    {
      var duplicates = unitId.GroupBy(x => x)
        .Where(u => u.Count() > 1).Select(u => u.Key).ToList();

      if (duplicates.Count > 0)
      {
        var duplicateUnits = string.Join(", ", duplicates);
        throw new UserFriendlyException($"The duplicate number {duplicateUnits}");
      }

      await CheckCountStation(line.Id, unitId);

      if (line.LineType == LineType.MainLine)
      {
        foreach (var item in unitId)
        {
          if (!await _transferCenterRepository.AnyAsync(tc => tc.Id == item))
            throw new NotFoundException(typeof(TransferCenter), item.ToString());

          await _stationRepository.InsertAsync(
                new Station(line.Id, item, await StationNumberGenerator(line.Id)), autoSave: true);
        }
      }


      if (line.LineType == LineType.SubLine)
      {
        var queryableTransferCenter = await _transferCenterRepository.GetQueryableAsync();

        var transferCenterIds = await queryableTransferCenter
           .Where(tc => unitId.Contains(tc.Id))
           .Select(tc => tc.Id).ToListAsync();

        if (transferCenterIds.Count() == 0 || transferCenterIds.Count() > 1)
          throw new BusinessException();//en az - en çok bir transfer center. Hata mesajını özelleştir

        var transferCenterId = transferCenterIds.First();

        unitId = new int[] { transferCenterId }.Concat(unitId.Where(x => x != transferCenterId)).ToArray();

        foreach (var item in unitId)
        {
          if (item != transferCenterId && !await _agencyRepository.AnyAsync(tc => tc.Id == item))
            throw new NotFoundException(typeof(Agency), item.ToString());


          await _stationRepository.InsertAsync(
                 new Station(line.Id, item, await StationNumberGenerator(line.Id)), autoSave: true);
        }
      }
    }

    public async Task UpdateStationAsync()
    {

    }

    private async Task<int> StationNumberGenerator(int lineId)
    {
      if (await _stationRepository.CountAsync(x => x.LineId == lineId) <= 0)
        return 1;

      var stations = await _stationRepository.GetQueryableAsync();

      var maxStationNumber = stations
          .Where(x => x.LineId == lineId)
          .Max(x => x.StationNumber);

      if (maxStationNumber >= LineConst.LimitOfStation)
        throw new UserFriendlyException("Limit hatasıyla ilgili error");//Create yaparken ihtiyaç duymadım. update yaparken ihtiyaç yoksa kaldır.

      return maxStationNumber + 1;
    }

    internal async Task CheckCountStation(int lineId, int[]? unitId)
    {
      var lineCount = await _stationRepository.CountAsync(x => x.LineId == lineId);
      var totalCount = lineCount + unitId?.Length ?? 0;

      if (totalCount > LineConst.LimitOfStation)
        throw new BusinessException("Limit hatasıyla ilgili error (gerek duyulursa => error sınıfı) kullanılacak KULLANILACAK");
    }

    #endregion

  }
}
