using agency_transfercenter.EntityDtos.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.EntityDtos.AgencyDtos
{
  public class AgencyDto : UnitDto
  {
    public int TransferCenterId { get; set; }
  }
}
