using agency_transfercenter.Samples;
using Xunit;

namespace agency_transfercenter.EntityFrameworkCore.Domains;

[Collection(agency_transfercenterTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<agency_transfercenterEntityFrameworkCoreTestModule>
{

}
