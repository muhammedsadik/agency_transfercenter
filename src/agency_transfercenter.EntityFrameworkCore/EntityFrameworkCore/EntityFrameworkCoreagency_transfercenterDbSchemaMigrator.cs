using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using agency_transfercenter.Data;
using Volo.Abp.DependencyInjection;

namespace agency_transfercenter.EntityFrameworkCore;

public class EntityFrameworkCoreagency_transfercenterDbSchemaMigrator
    : Iagency_transfercenterDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreagency_transfercenterDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the agency_transfercenterDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<agency_transfercenterDbContext>()
            .Database
            .MigrateAsync();
    }
}
