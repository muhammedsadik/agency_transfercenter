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
    public ICollection<Station> Stations { get; private set; }


    internal Line()
    {
    }

    public Line AddLine(string name, bool isActive, LineType type)
    {
      Name = name;
      IsActive = isActive;
      LineType = type;

      Stations = new List<Station>();

      return this;
    }

    public void SetStatus(bool isActive)
    {
      IsActive = isActive;
    }

    public void SetName(string name)
    {
      Name = name;
    }


    #region Station

    public void AddStation(int unitId)//List olarak
    {
      Check.NotNull(unitId, nameof(unitId));

      if (Stations.Count >= 10 || !IsActive) //Line aktif ise ekleme yapılabilsin
        throw new UserFriendlyException("BURADA SABİT MESAJ KULLANILACAK");

      if (IsInStation(unitId))
        throw new UserFriendlyException("BURADA SABİT MESAJ KULLANILACAK");

      Stations.Add(new Station(Id, unitId));
    }

    public void RemoveStation(int unitId)
    {
      Check.NotNull(unitId, nameof(unitId));

      if (!IsInStation(unitId))
        throw new UserFriendlyException("BURADA SABİT MESAJ KULLANILACAK");

      Stations.RemoveAll(x => x.UnitId == unitId);
    }

    public void RemoveAllStationsExceptGivenIds(List<int> unitIds)
    {
      Check.NotNullOrEmpty(unitIds, nameof(unitIds));

      Stations.RemoveAll(x => !unitIds.Contains(x.UnitId));
    }

    public void RemoveAllStations()
    {
      Stations.RemoveAll(x => x.LineId == Id);
    }

    private bool IsInStation(int unitId)
    {
      return Stations.Any(x => x.UnitId == unitId);
    }

    #endregion


  }
}
