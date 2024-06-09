using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace agency_transfercenter.Entities.Stations
{
  public class Station : Entity<int>
  {
    public int LineId { get; set; }
    public Line Line { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    public int StationNumber { get; set; }


    public Station() { }






  }
}
