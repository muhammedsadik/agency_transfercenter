using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agency_transfercenter.Entities.Units;
using agency_transfercenter.Entities.Stations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.Collections.ObjectModel;
using Volo.Abp;
using agency_transfercenter.EntityConsts.LineConsts;

namespace agency_transfercenter.Entities.Lines
{
  public class Line : Entity<int>
  {
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public LineType LineType { get; set; }
    public ICollection<Station> Stations { get; set; }


    internal Line()
    {
    }

  }
}
