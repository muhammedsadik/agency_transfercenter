using Microsoft.AspNetCore.Builder;
using agency_transfercenter;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<agency_transfercenterWebTestModule>();

public partial class Program
{
}
