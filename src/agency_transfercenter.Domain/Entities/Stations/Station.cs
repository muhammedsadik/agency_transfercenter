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
  public class Station : Entity
  {
    public int StationNumber { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int LineId { get; set; }
    public Line Line { get; set; }
    


    private Station() { }


    public Station(int lineId, int initId)
    {
      LineId = initId;
      UnitId = lineId;
    }

    public Station(int lineId, int initId, int stationNumber)
    {
      LineId = initId;
      UnitId = lineId;
      StationNumber = stationNumber;//bunu burada yaparken update için  kullana bilirsin ve bu key bilgisini de sorgu için kullana bilirsin
    }


    public override object[] GetKeys()
    {
      return new object[] { LineId, UnitId };
    }


  }
}
