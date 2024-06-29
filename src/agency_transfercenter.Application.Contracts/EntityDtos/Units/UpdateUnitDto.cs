using agency_transfercenter.EntityDtos.AddressDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace agency_transfercenter.EntityDtos.Units
{
  public class UpdateUnitDto : EntityDto
  {
    public string UnitName { get; set; }
    public string UnitPhone { get; set; }
    public string UnitMail { get; set; }
    public string ManagerName { get; set; }
    public string ManagerSurname { get; set; }
    public string ManagerGsm { get; set; }
    public string ManagerMail { get; set; }
    public AddressDto Address { get; set; }
  }
}
