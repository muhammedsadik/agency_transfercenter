namespace agency_transfercenter;

public static class AtcDomainErrorCodes
{


  #region ExceptionErrorCodes

  public const string NotFound = "agency_transfercenter:500";
  public const string AlreadyExists = "agency_transfercenter:501";
  public const string RequestLimitsError = "agency_transfercenter:502";

  #endregion


  #region ValidationErrorCodes

  public const string NotInPhoneFormat = "agency_transfercenter:v401";


  #endregion

}
/*

{
  "unitName": "string",
  "unitPhone": "01234567899",
  "unitMail": "strin@cdfg",
  "managerName": "string",
  "managerSurname": "string",
  "managerGsm": "01234567899",
  "managerMail": "strin@cdfg",
  "address": {
    "city": "string",
    "street": "string",
    "number": "string"
  },
  "transferCenterId": 4
}

*/