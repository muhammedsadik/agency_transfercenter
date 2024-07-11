using agency_transfercenter.Entities.Addresses;
using agency_transfercenter.Entities.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace agency_transfercenter.Entities.Units
{
  public class Unit : FullAuditedAggregateRoot<int>
  {
    public string UnitName { get; set; }
    public string UnitPhone { get; set; }
    public string UnitMail { get; set; }
    public string ManagerName { get; set; }
    public string ManagerSurname { get; set; }
    public string ManagerGsm { get; set; }
    public string ManagerMail { get; set; }

    public Address Address { get; set; }

    public ICollection<Station> Stations { get; set; }


    internal Unit()
    {
    }

  }
}
