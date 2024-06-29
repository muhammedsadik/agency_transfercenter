//using agency_transfercenter.EntityDtos.TransferCenterDtos;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace agency_transfercenter.Validation.TransferCenterValidation
//{
//  public class UpdateUnitOfTransferCenterDtoValidator : AbstractValidator<UpdateUnitOfTransferCenterDto>
//  {
//    public UpdateUnitOfTransferCenterDtoValidator()
//    {
//      RuleFor(x => x.UnitName).NotEmpty().WithMessage("Unit Namer alanı boş olamaz").MinimumLength(4);

//      RuleFor(x => x.UnitPhone).Matches(@"^0\d{10}$");

//      RuleFor(x => x.UnitMail).NotEmpty();
//      RuleFor(x => x.UnitMail).EmailAddress();

//    }
//  }
//}
