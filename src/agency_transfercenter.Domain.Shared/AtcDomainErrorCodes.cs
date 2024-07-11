namespace agency_transfercenter;

public static class AtcDomainErrorCodes
{


  #region ExceptionErrorCodes

  public const string NotFound = "agency_transfercenter:500";
  public const string AlreadyExists = "agency_transfercenter:501";
  public const string RequestLimitsError = "agency_transfercenter:502";
  public const string MustHaveOneTransferCenter = "agency_transfercenter:503";
  public const string AgenciesMustBeAffiliatedToTheTransferCenter = "agency_transfercenter:504";
  public const string RepeatedDataError = "agency_transfercenter:505";
  public const string LimitOfStationError = "agency_transfercenter:506";

  
  #endregion


  #region ValidationErrorCodes

  public const string NotInPhoneFormat = "agency_transfercenter:v401";
  public const string NotInEnumType = "agency_transfercenter:v402";
  public const string StationLimitError = "agency_transfercenter:v403";


  #endregion

}