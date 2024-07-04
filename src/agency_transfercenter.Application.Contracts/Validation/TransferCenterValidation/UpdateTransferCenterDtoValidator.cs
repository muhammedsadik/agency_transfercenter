using agency_transfercenter.EntityDtos.TransferCenterDtos;
using agency_transfercenter.Localization;
using agency_transfercenter.Validation.AddressValidation;
using agency_transfercenter.Validation.UnitValidation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.Validation.TransferCenterValidation
{
  public class UpdateTransferCenterDtoValidator : AbstractValidator<UpdateTransferCenterDto>
  {
    public UpdateTransferCenterDtoValidator(IStringLocalizer<agency_transfercenterResource> localizer)
    {
      Include(new UpdateUnitDtoValidator(localizer));

    }
  }
}
