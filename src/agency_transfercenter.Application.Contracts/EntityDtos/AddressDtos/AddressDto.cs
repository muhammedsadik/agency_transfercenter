using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace agency_transfercenter.EntityDtos.AddressDtos
{
  public class AddressDto : EntityDto
  {
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
  }
}
