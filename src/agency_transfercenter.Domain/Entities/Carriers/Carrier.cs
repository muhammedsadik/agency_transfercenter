using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agency_transfercenter.Carriers;
using agency_transfercenter.Entities.Commons;
using Volo.Abp.Domain.Entities.Auditing;

namespace agency_transfercenter.Entities.Carriers
{
  public class Carrier : CommonEntity
  {
    public string CarrierRoutes { get; internal set; }
    public bool IsActive { get; internal set; }
    public CarrierType CarrierType { get; internal set; }





    //internal Carrier(int id, string name, string phone, bool IsActive): base(id, name, phone)
    //{

    //}
    



  }
}
