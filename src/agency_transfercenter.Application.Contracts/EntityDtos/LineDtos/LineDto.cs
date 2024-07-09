using agency_transfercenter.EntityConsts.LineConsts;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace agency_transfercenter.EntityDtos.LineDtos
{
  public class LineDto : EntityDto<int>
  {
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public LineType LineType { get; set; }
  }
}
