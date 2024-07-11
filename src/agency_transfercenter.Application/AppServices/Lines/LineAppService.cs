using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.Stations;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.EntityDtos.LineDtos;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using agency_transfercenter.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace agency_transfercenter.AppServices.Lines
{
  [Authorize(agency_transfercenterPermissions.Lines.Default)]
  public class LineAppService : CrudAppService<Line, LineDto, int, GetListPagedAndSortedDto, CreateLineDto, UpdateLineDto>, ILineAppService
  {
    private readonly LineManager _lineManager;
    public LineAppService(IRepository<Line, int> repository, LineManager lineManager) : base(repository)
    {
      _lineManager = lineManager;
     
      DeletePolicyName = agency_transfercenterPermissions.Lines.Delete;
    }

    [Authorize(agency_transfercenterPermissions.Lines.Create)]
    public override async Task<LineDto> CreateAsync(CreateLineDto input)
    {
      var line = await _lineManager.CreateAsync(input);

      return line;
    }

    [Authorize(agency_transfercenterPermissions.Lines.Edit)]
    public override async Task<LineDto> UpdateAsync(int id, UpdateLineDto input)
    {
      var line = await _lineManager.UpdateAsync(id, input);

      return line;
    }

    public override async Task<PagedResultDto<LineDto>> GetListAsync(GetListPagedAndSortedDto input)
    {
      var line = await _lineManager.GetListAsync(input);

      return line;
    }

    public async Task<LineWithStationsDto> GetLineWithStationsAsync(int lineId)
    {
      var lineWithStationsDto = await _lineManager.GetLineWithStationsAsync(lineId);

      return lineWithStationsDto;
    }

  }
}
