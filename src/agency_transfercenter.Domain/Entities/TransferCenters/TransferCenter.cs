using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Stations;
using agency_transfercenter.Entities.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agency_transfercenter.Entities.TransferCenters
{
  public class TransferCenter : Unit
  {
    public ICollection<Agency> Agencies { get; set; }

    private TransferCenter()
    {
    }


  }
}
