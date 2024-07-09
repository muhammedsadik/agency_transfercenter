﻿using agency_transfercenter.Entities.Addresses;
using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.Entities.Units;
using agency_transfercenter.EntityDtos.AddressDtos;
using agency_transfercenter.EntityDtos.AgencyDtos;
using agency_transfercenter.EntityDtos.LineDtos;
using agency_transfercenter.EntityDtos.TransferCenterDtos;
using agency_transfercenter.EntityDtos.Units;
using AutoMapper;

namespace agency_transfercenter;

public class agency_transfercenterApplicationAutoMapperProfile : Profile
{
  public agency_transfercenterApplicationAutoMapperProfile()
  {
    CreateMap<Address, AddressDto>().ReverseMap();

    #region TransferCenter
    CreateMap<TransferCenter, TransferCenterDto>().ReverseMap();
    CreateMap<UpdateTransferCenterDto, TransferCenter>().ReverseMap();
    CreateMap<CreateTransferCenterDto, TransferCenter>().ReverseMap();
    #endregion
    
    #region Agency
    CreateMap<Agency, AgencyDto>().ReverseMap();
    CreateMap<UpdateAgencyDto, Agency>().ReverseMap();
    CreateMap<CreateAgencyDto, Agency>().ReverseMap();
    #endregion
    
    #region Line
    CreateMap<Line, LineDto>().ReverseMap();
    CreateMap<UpdateLineDto, Line>().ReverseMap();
    CreateMap<CreateLineDto, Line>().ReverseMap();
    #endregion


  }
}
