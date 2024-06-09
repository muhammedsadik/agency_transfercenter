using agency_transfercenter.Entities.Units;
using agency_transfercenter.Entities.TransferCenters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agency_transfercenter.Entities.Agencies
{
  public class Agency : Unit
  {

    public int TransferCenterId { get; set; }
    public TransferCenter TransferCenter { get; set; }


    private Agency()
    {
    }






  }
}
