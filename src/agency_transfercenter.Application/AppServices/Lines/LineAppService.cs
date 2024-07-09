using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.Stations;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.EntityDtos.LineDtos;
using agency_transfercenter.EntityDtos.PagedAndSortedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace agency_transfercenter.AppServices.Lines
{
  public class LineAppService : CrudAppService<Line, LineDto, int, GetListPagedAndSortedDto, CreateLineDto, UpdateLineDto>, ILineAppService
  {
    private readonly LineManager _lineManager;
    public LineAppService(IRepository<Line, int> repository, LineManager lineManager) : base(repository)
    {
      _lineManager = lineManager;
    }

    public override async Task<LineDto> CreateAsync(CreateLineDto input)
    {
      var line = await _lineManager.CreateAsync(input);

      return line;
    }

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

  }
}
