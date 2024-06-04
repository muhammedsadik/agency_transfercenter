using agency_transfercenter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace agency_transfercenter.Permissions;

public class agency_transfercenterPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(agency_transfercenterPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(agency_transfercenterPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<agency_transfercenterResource>(name);
    }
}
