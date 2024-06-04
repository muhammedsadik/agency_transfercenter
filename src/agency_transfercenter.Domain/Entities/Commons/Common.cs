using agency_transfercenter.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace agency_transfercenter.Entities.Commons
{
  public class Common : FullAuditedAggregateRoot<int>
  {
    public string Name { get; set; }
    public CarrierType? CarrierType { get; set; }

  }
}
