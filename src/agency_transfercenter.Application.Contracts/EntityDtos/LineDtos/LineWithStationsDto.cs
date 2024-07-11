using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.EntityDtos.LineDtos
{
  public class LineWithStationsDto : LineDto
  {
    public List<StationDto> Stations { get; set; }
  }
}
