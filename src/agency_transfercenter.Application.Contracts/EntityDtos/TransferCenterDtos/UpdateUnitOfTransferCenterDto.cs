using agency_transfercenter.EntityDtos.AddressDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace agency_transfercenter.EntityDtos.TransferCenterDtos
{
  public class UpdateUnitOfTransferCenterDto : EntityDto
  {
    public string UnitName { get; set; }
    public string UnitPhone { get; set; }
    public string UnitMail { get; set; }
    public AddressDto Address { get; set; }

  }
}