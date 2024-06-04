using Volo.Abp.Modularity;

namespace agency_transfercenter;

/* Inherit from this class for your domain layer tests. */
public abstract class agency_transfercenterDomainTestBase<TStartupModule> : agency_transfercenterTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
