using agency_transfercenter.Carriers;
using agency_transfercenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace agency_transfercenter.Entities.Commons
{
  public class CommonEntity : FullAuditedAggregateRoot<int>
  {
    public string Name { get; internal set; }
    
    public string Phone { get; internal set; }

    internal CommonEntity()
    {
    }











    
    //internal CommonEntity(int id, string name, string phone): base(id)
    //{       
    //  SetName(name);
    //  Phone = phone;
    //}

    //internal CommonEntity SetName(string name)
    //{
    //  Name = Check.NotNullOrWhiteSpace(
    //    name, nameof(name), maxLength: CommonEntityConst.MaxNameLength);
    //  return this;
    //}




  }
}
