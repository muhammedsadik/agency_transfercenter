using agency_transfercenter.EntityDtos.TransferCenterDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agency_transfercenter.Validations
{
  public class UpdateTransferCenterDtoValidator : AbstractValidator<UpdateUnitOfTransferCenterDto>
  {

    public UpdateTransferCenterDtoValidator()
    {
      RuleFor(x => x.UnitName).NotEmpty().WithMessage("Unit Namer alanı boş olamaz").MinimumLength(4);

      RuleFor(x => x.UnitPhone).Matches(@"^0\d{10}$");

      RuleFor(x => x.UnitMail).NotEmpty();
      RuleFor(x => x.UnitMail).EmailAddress();


    }
  }
}
