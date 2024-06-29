using agency_transfercenter.EntityDtos.Units;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.Validation.UnitValidation
{
  public class CreateUnitDtoValidator : AbstractValidator<CreateUnitDto>
  {
    public CreateUnitDtoValidator()
    {
      RuleFor(x=> x.UnitName).NotEmpty();

      RuleFor(x=> x.UnitPhone).Matches(@"^0\d{10}$");

      RuleFor(x=> x.UnitMail).NotEmpty();
      RuleFor(x=> x.UnitMail).EmailAddress();


      RuleFor(x=> x.ManagerName).NotEmpty();
      RuleFor(x=> x.ManagerSurname).NotEmpty();

      RuleFor(x=> x.ManagerGsm).Matches(@"^0\d{10}$");

      RuleFor(x=> x.ManagerMail).NotEmpty();
      RuleFor(x=> x.ManagerMail).EmailAddress();
      
    }
  }
}
