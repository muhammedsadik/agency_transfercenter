using agency_transfercenter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace agency_transfercenter.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class agency_transfercenterController : AbpControllerBase
{
    protected agency_transfercenterController()
    {
        LocalizationResource = typeof(agency_transfercenterResource);
    }
}
