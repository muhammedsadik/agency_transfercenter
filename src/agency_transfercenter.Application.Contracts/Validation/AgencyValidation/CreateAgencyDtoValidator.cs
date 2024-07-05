using agency_transfercenter.EntityDtos.AgencyDtos;
using agency_transfercenter.Localization;
using agency_transfercenter.Validation.UnitValidation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.Validation.AgencyValidation
{
  public class CreateAgencyDtoValidator : AbstractValidator<CreateAgencyDto>
  {
    public CreateAgencyDtoValidator(IStringLocalizer<agency_transfercenterResource> localizer)
    {
      Include(new CreateUnitDtoValidator(localizer));

      RuleFor(x=>x.TransferCenterId).NotEmpty();
    }
  }
}
