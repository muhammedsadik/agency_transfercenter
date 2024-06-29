using agency_transfercenter.EntityDtos.TransferCenterDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace agency_transfercenter.Validation.TransferCenterValidation
{
  public class UpdateManagerOfTransferCenterDtoValidator : AbstractValidator<UpdateManagerOfTransferCenterDto>
  {
    public UpdateManagerOfTransferCenterDtoValidator()
    {
      RuleFor(x => x.ManagerName).NotEmpty();
      RuleFor(x => x.ManagerSurname).NotEmpty();

      RuleFor(x => x.ManagerGsm).Matches(@"^0\d{10}$");

      RuleFor(x => x.ManagerMail).NotEmpty();
      RuleFor(x => x.ManagerMail).EmailAddress();
    }
  }
}
