using agency_transfercenter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace agency_transfercenter.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class agency_transfercenterPageModel : AbpPageModel
{
    protected agency_transfercenterPageModel()
    {
        LocalizationResourceType = typeof(agency_transfercenterResource);
    }
}
