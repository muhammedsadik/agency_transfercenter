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



    internal Station() { }


    public Station(int lineId, int initId)
    {
      LineId = initId;
      UnitId = lineId;
    }

    internal Station(int lineId, int unitId, int stationNumber)
    {
      LineId = lineId;
      UnitId = unitId;
      StationNumber = stationNumber; //bunu burada yaparken update için  kullana bilirsin ve bu key bilgisini de sorgu için kullana bilirsin
    }


    public override object[] GetKeys()
    {
      return new object[] { LineId, UnitId };
    }


  }
}
