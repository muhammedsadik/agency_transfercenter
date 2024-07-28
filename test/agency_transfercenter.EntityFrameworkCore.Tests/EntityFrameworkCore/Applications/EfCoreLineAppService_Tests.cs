using agency_transfercenter.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace agency_transfercenter.EntityFrameworkCore.Applications
{
  [Collection(agency_transfercenterTestConsts.CollectionDefinitionName)]
  public class EfCoreLineAppService_Tests : LineAppService_Tests<agency_transfercenterEntityFrameworkCoreTestModule>
  {
  }
}
