using agency_transfercenter.EntityConsts.LineConsts;
using agency_transfercenter.EntityDtos.LineDtos;
using agency_transfercenter.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.Validation.LineValidation
{
  public class CreateLineDtoValidator : AbstractValidator<CreateLineDto>
  {
    public CreateLineDtoValidator(IStringLocalizer<agency_transfercenterResource> localizer)
    {
      RuleFor(l => l.Name).NotEmpty().MinimumLength(3);

      RuleFor(l => l.LineType).IsInEnum().WithMessage(localizer[AtcDomainErrorCodes.NotInEnumType]);

      RuleFor(l => l.UnitId).Must(ids => ids == null || ids.Length <= LineConst.LimitOfStation)
        .WithMessage(x => string.Format(localizer[AtcDomainErrorCodes.StationLimitError], LineConst.LimitOfStation, x.UnitId?.Length ?? 0));
    }
  }
}
