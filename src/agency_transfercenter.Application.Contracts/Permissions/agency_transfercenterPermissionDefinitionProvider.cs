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

    var AgencyPermission = TransferCenterAndAgency.AddPermission(agency_transfercenterPermissions.Agencies.Default, L("Permission:Agencies"));
    AgencyPermission.AddChild(agency_transfercenterPermissions.Agencies.Create, L("Permission:Agencies.Create"));
    AgencyPermission.AddChild(agency_transfercenterPermissions.Agencies.Edit, L("Permission:Agencies.Edit"));
    AgencyPermission.AddChild(agency_transfercenterPermissions.Agencies.Delete, L("Permission:Agencies.Delete"));

    var LinePermission = TransferCenterAndAgency.AddPermission(agency_transfercenterPermissions.Lines.Default, L("Permission:Lines"));
    LinePermission.AddChild(agency_transfercenterPermissions.Lines.Create, L("Permission:Lines.Create"));
    LinePermission.AddChild(agency_transfercenterPermissions.Lines.Edit, L("Permission:Lines.Edit"));
    LinePermission.AddChild(agency_transfercenterPermissions.Lines.Delete, L("Permission:Lines.Delete"));
  }

  private static LocalizableString L(string name)
  {
    return LocalizableString.Create<agency_transfercenterResource>(name);
  }
}
