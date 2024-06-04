using System.Threading.Tasks;

namespace agency_transfercenter.Data;

public interface Iagency_transfercenterDbSchemaMigrator
{
    Task MigrateAsync();
}
