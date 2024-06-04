using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace agency_transfercenter.Data;

/* This is used if database provider does't define
 * Iagency_transfercenterDbSchemaMigrator implementation.
 */
public class Nullagency_transfercenterDbSchemaMigrator : Iagency_transfercenterDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
