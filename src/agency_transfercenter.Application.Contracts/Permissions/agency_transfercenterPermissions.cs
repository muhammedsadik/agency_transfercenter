namespace agency_transfercenter.Permissions;

public static class agency_transfercenterPermissions
{
  public const string GroupName = "TransferCenterAndAgency";

  public static class Transfercenters
  {
    public const string Default = GroupName + ".Transfercenters";
    public const string Create = Default + ".Create";
    public const string Edit = Default + ".Edit";
    public const string Delete = Default + ".Delete";

  }



}
