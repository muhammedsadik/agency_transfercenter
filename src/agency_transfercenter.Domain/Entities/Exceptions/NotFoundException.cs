using agency_transfercenter.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Localization;

namespace agency_transfercenter.Entities.Exceptions
{
  public class NotFoundException : BusinessException, ILocalizeErrorMessage
  {
    public Type EntityType { get; }
    public string EntityCode{ get; }

    public NotFoundException(Type entityType, string entityCode) : base(AtcDomainErrorCodes.NotFound)
    {
      EntityType = entityType;
      EntityCode = entityCode;
    }

    public string LocalizeMessage(LocalizationContext context)
    {
      var localizer = context.LocalizerFactory.Create<agency_transfercenterResource>();

      Data["EntityType"] = localizer[EntityType.Name!].Value;
      Data["EntityCode"] = EntityCode;

      return localizer[Code!, Data["EntityType"], Data["EntityCode"]];
    }
  }
}
