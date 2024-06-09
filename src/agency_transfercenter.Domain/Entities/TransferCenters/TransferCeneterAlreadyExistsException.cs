using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace agency_transfercenter.Entities.TransferCenters
{
  public class TransferCeneterAlreadyExistsException : BusinessException
  {
    public TransferCeneterAlreadyExistsException(string name) : base(AgencyTransfercenterDomainErrorCodes.TransfercenterAlreadyExists)
    {
      WithData("name", name);
    }
  }
}
