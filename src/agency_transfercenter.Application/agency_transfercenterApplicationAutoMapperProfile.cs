using agency_transfercenter.Entities.Addresses;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.Entities.Units;
using agency_transfercenter.EntityDtos.AddressDtos;
using agency_transfercenter.EntityDtos.TransferCenterDtos;
using agency_transfercenter.EntityDtos.Units;
using AutoMapper;

namespace agency_transfercenter;

public class agency_transfercenterApplicationAutoMapperProfile : Profile
{
  public agency_transfercenterApplicationAutoMapperProfile()
  {
    CreateMap<TransferCenter, TransferCenterDto>().ReverseMap();
    CreateMap<UpdateTransferCenterDto, TransferCenter>().ReverseMap();
    CreateMap<CreateTransferCenterDto, TransferCenter>().ReverseMap();
    CreateMap<Address, AddressDto>().ReverseMap();
    CreateMap<Unit, UnitDto>().ReverseMap();

  }
}
