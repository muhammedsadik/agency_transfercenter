using agency_transfercenter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace agency_transfercenter.Permissions;

public class agency_transfercenterPermissionDefinitionProvider : PermissionDefinitionProvider
{
  public override void Define(IPermissionDefinitionContext context)
  {
    var TransferCenterAndAgency = context.AddGroup(agency_transfercenterPermissions.GroupName, L("Permission:TransferCenterAndAgency"));

    var transfercentersPermission = TransferCenterAndAgency.AddPermission(agency_transfercenterPermissions.Transfercenters.Default, L("Permission:Transfercenters"));
    transfercentersPermission.AddChild(agency_transfercenterPermissions.Transfercenters.Create, L("Permission:Transfercenters.Create"));
    transfercentersPermission.AddChild(agency_transfercenterPermissions.Transfercenters.Edit, L("Permission:Transfercenters.Edit"));
    transfercentersPermission.AddChild(agency_transfercenterPermissions.Transfercenters.Delete, L("Permission:Transfercenters.Delete"));



  }

  private static LocalizableString L(string name)
  {
    return LocalizableString.Create<agency_transfercenterResource>(name);
  }
}
