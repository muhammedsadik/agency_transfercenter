using Volo.Abp.Modularity;

namespace agency_transfercenter;

[DependsOn(
    typeof(agency_transfercenterApplicationModule),
    typeof(agency_transfercenterDomainTestModule)
)]
public class agency_transfercenterApplicationTestModule : AbpModule
{

}
