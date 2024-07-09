using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace agency_transfercenter.EntityDtos.LineDtos
{
  public class UpdateLineDto : EntityDto
  {
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public int[]? UnitId { get; set; }
  }
}
