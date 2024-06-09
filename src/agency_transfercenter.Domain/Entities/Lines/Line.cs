using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agency_transfercenter.Entities.LineConsts;
using agency_transfercenter.Entities.Units;
using agency_transfercenter.Entities.Stations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace agency_transfercenter.Entities.Lines
{
  public class Line : Entity<int>
  {
    public int[]? StationId { get; set; } = new int[LineConst.LimitOfStation];

    public bool IsActive { get; set; }
    public LineType LineType { get; internal set; }

    public ICollection<Station> Stations { get; set; }


    private Line()
    {
    }



  }
}
