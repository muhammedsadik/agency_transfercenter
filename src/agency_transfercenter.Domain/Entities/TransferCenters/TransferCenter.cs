using agency_transfercenter.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agency_transfercenter.Entities.TransferCenters
{
  public class TransferCenter : CommonEntity
  {
    public string Address { get; set; }



    internal TransferCenter(string address)
    {
      Address = address;
    }



  }
}
