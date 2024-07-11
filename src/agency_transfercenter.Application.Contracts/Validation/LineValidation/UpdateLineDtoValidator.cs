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
  public class UpdateLineDtoValidator : AbstractValidator<UpdateLineDto>
  {
    public UpdateLineDtoValidator(IStringLocalizer<agency_transfercenterResource> localizer)
    {
      RuleFor(l => l.Name).NotEmpty();

      RuleFor(l => l.UnitId).Must(ids => ids == null || ids.Length <= LineConst.LimitOfStation)
        .WithMessage(x => string.Format(localizer[AtcDomainErrorCodes.StationLimitError], LineConst.LimitOfStation, x.UnitId?.Length ?? 0));

    }
  }
}
