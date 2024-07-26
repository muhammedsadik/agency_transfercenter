using agency_transfercenter.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace agency_transfercenter.EntityFrameworkCore.Applications
{
  [Collection(agency_transfercenterTestConsts.CollectionDefinitionName)]
  public class EfCoreAgencyAppService_Tests : AgencyAppService_Test<agency_transfercenterEntityFrameworkCoreTestModule>
  {
  }
}
