using agency_transfercenter.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace agency_transfercenter.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(agency_transfercenterEntityFrameworkCoreModule),
    typeof(agency_transfercenterApplicationContractsModule)
    )]
public class agency_transfercenterDbMigratorModule : AbpModule
{
}
