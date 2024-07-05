using agency_transfercenter.EntityDtos.Units;
using agency_transfercenter.Localization;
using agency_transfercenter.Validation.AddressValidation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.Validation.UnitValidation
{
  public class UpdateUnitDtoValidator : AbstractValidator<UpdateUnitDto>
  {
    public UpdateUnitDtoValidator(IStringLocalizer<agency_transfercenterResource> localizer)
    {
      RuleFor(x => x.UnitName).NotEmpty().MinimumLength(4);

      RuleFor(x => x.UnitPhone).Matches(@"^0\d{10}$").WithMessage(localizer[AtcDomainErrorCodes.NotInPhoneFormat]);

      RuleFor(x => x.UnitMail).NotEmpty();
      RuleFor(x => x.UnitMail).EmailAddress();

      RuleFor(x => x.ManagerName).NotEmpty().MinimumLength(3);

      RuleFor(x => x.ManagerSurname).NotEmpty();

      RuleFor(x => x.ManagerGsm).Matches(@"^0\d{10}$").WithMessage(localizer[AtcDomainErrorCodes.NotInPhoneFormat]);

      RuleFor(x => x.ManagerMail).NotEmpty();
      RuleFor(x => x.ManagerMail).EmailAddress();

      RuleFor(x => x.Address).SetValidator(new AddressDtoValidator());
    }
  }
}
