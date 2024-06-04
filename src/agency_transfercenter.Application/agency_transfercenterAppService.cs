using System;
using System.Collections.Generic;
using System.Text;
using agency_transfercenter.Localization;
using Volo.Abp.Application.Services;

namespace agency_transfercenter;

/* Inherit your application services from this class.
 */
public abstract class agency_transfercenterAppService : ApplicationService
{
    protected agency_transfercenterAppService()
    {
        LocalizationResource = typeof(agency_transfercenterResource);
    }
}
