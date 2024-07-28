using agency_transfercenter.Line;
using agency_transfercenter.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace agency_transfercenter.EntityFrameworkCore.Domains
{
  [Collection(agency_transfercenterTestConsts.CollectionDefinitionName)]
  public class EfCoreLineManager_Tests : LineManager_Tests<agency_transfercenterEntityFrameworkCoreTestModule>
  {
  }
}
