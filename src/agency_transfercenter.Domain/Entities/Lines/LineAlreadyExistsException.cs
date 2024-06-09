using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace agency_transfercenter.Entities.Lines
{
  public class LineAlreadyExistsException : BusinessException
  {
    public LineAlreadyExistsException(string name) : base(AgencyTransfercenterDomainErrorCodes.LineAlreadyExists)
    {
      WithData("name", name);
    }
  }
}
