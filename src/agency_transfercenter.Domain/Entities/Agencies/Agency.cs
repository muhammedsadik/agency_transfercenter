using agency_transfercenter.Entities.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agency_transfercenter.Entities.Agencies
{
  public class Agency : CommonEntity
  {

    public int TransferCenterId { get; internal set; }
    public string Address { get; set; }

    internal Agency(int transferCenterId, string address)
    {
      TransferCenterId = transferCenterId;
      Address = address;
    }




  }
}
