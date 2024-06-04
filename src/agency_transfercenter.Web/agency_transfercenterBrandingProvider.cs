using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace agency_transfercenter.Web;

[Dependency(ReplaceServices = true)]
public class agency_transfercenterBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "agency_transfercenter";
}
