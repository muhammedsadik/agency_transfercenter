using Volo.Abp.Modularity;

namespace agency_transfercenter;

[DependsOn(
    typeof(agency_transfercenterDomainModule),
    typeof(agency_transfercenterTestBaseModule)
)]
public class agency_transfercenterDomainTestModule : AbpModule
{

}
