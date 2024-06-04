using agency_transfercenter.Samples;
using Xunit;

namespace agency_transfercenter.EntityFrameworkCore.Applications;

[Collection(agency_transfercenterTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<agency_transfercenterEntityFrameworkCoreTestModule>
{

}
