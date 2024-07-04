using agency_transfercenter.EntityDtos.AddressDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.Validation.AddressValidation
{
  public class AddressDtoValidator : AbstractValidator<AddressDto> 
  {
    public AddressDtoValidator()
    {
      RuleFor(x => x.City).NotEmpty().MinimumLength(3);
      RuleFor(x => x.Street).NotEmpty().MinimumLength(3);
      RuleFor(x => x.Number).NotEmpty();
    }
  }
}
