using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace agency_transfercenter.EntityDtos.LineDtos
{
  public class StationDto : EntityDto
  {
    public int StationNumber { get; set; }
    public int LineId { get; set; }
    public int UnitId { get; set; }
  }
}
