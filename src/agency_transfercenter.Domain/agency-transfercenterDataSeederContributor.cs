﻿using agency_transfercenter.Entities.Addresses;
using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.Stations;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.Entities.Units;
using agency_transfercenter.EntityConsts.LineConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace agency_transfercenter
{
    public class agency_transfercenterDataSeederContributor : IDataSeedContributor, ITransientDependency
  {
    private readonly IRepository<TransferCenter, int> _transferCenterRepository;
    private readonly IRepository<Agency, int> _agencyRepository;
    private readonly IRepository<Station> _stationRepository;
    private readonly IRepository<Line, int> _lineRepository;
    private readonly IRepository<Unit, int> _unitRepository;

    public agency_transfercenterDataSeederContributor(
      IRepository<TransferCenter, int> transferCenterRepository,
      IRepository<Agency, int> agencyRepository,
      IRepository<Station> stationRepository,
      IRepository<Line, int> lineRepository,
      IRepository<Unit, int> unitRepository
      )
    {
      _transferCenterRepository = transferCenterRepository;
      _agencyRepository = agencyRepository;
      _lineRepository = lineRepository;
      _stationRepository = stationRepository;
      _unitRepository = unitRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
      await SeedTrasferCenterAsync();
      await SeedAgencyAsync();
      await SeedLineAsync();
      await SeedStationAsync();
    }

    #region TransferCenter
    private async Task SeedTrasferCenterAsync()
    {
      if (await _transferCenterRepository.GetCountAsync() > 0)
        return;

      await _transferCenterRepository.InsertAsync(
        new TransferCenter()
        {
          UnitName = "Ankara Tranfer Center",
          ManagerName = "Ahmet",
          ManagerSurname = "Göral",
          ManagerGsm = "05315971111",
          UnitPhone = "03120901111",
          ManagerMail = "ahmet@gmail.com",
          UnitMail = "ankara@gmail.com",

          Address = new Address()
          {
            City = "Ankara",
            Street = "polatlı",
            Number = 471
          }
        },
        autoSave: true
      );

      await _transferCenterRepository.InsertAsync(
        new TransferCenter()
        {
          UnitName = "İstanbul Tranfer Center",
          ManagerName = "Serkan",
          ManagerSurname = "Çelik",
          ManagerGsm = "05321862222",
          UnitPhone = "02123712222",
          ManagerMail = "serkan@gmail.com",
          UnitMail = "istanbul@gmail.com",

          Address = new Address()
          {
            City = "İstanbul",
            Street = "Beyoğlu",
            Number = 83
          }
        },
        autoSave: true
      );

      await _transferCenterRepository.InsertAsync(
        new TransferCenter()
        {
          UnitName = "Adana Tranfer Center",
          ManagerName = "Bayram",
          ManagerSurname = "Özcan",
          ManagerGsm = "05339853333",
          UnitPhone = "03224583333",
          ManagerMail = "bayram@gmail.com",
          UnitMail = "adana@gmail.com",

          Address = new Address()
          {
            City = "Adana",
            Street = "Ceyhan",
            Number = 826
          }
        },
        autoSave: true
      );

      await _transferCenterRepository.InsertAsync(
        new TransferCenter()
        {
          UnitName = "Diyarbakır Tranfer Center",
          ManagerName = "Ömer",
          ManagerSurname = "Tekin",
          ManagerGsm = "05347214444",
          UnitPhone = "04120354444",
          ManagerMail = "omer@gmail.com",
          UnitMail = "diyarbakir@gmail.com",

          Address = new Address()
          {
            City = "Diyarbakır",
            Street = "Yenişehir",
            Number = 507
          }
        },
        autoSave: true
      );


    }
    #endregion

    #region Agency
    private async Task SeedAgencyAsync()
    {
      if (await _agencyRepository.GetCountAsync() > 0)
        return;


      await _agencyRepository.InsertAsync(
        new Agency()
        {
          UnitName = "Ankara Sincan Agency",
          ManagerName = "Arif",
          ManagerSurname = "Kaplan",
          ManagerGsm = "05347211101",
          UnitPhone = "04120351101",
          ManagerMail = "arif@gmail.com",
          UnitMail = "ankarasincan@gmail.com",
          TransferCenterId = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "ankara@gmail.com")).Id,

          Address = new Address()
          {
            City = "Ankara",
            Street = "Sincan",
            Number = 911
          }
        },
        autoSave: true
      );

      await _agencyRepository.InsertAsync(
        new Agency()
        {
          UnitName = "İstanbul Kurtköy Agency",
          ManagerName = "Hakan",
          ManagerSurname = "Şahin",
          ManagerGsm = "05347212201",
          UnitPhone = "04120352201",
          ManagerMail = "hakan@gmail.com",
          UnitMail = "istanbulkurtkoy@gmail.com",
          TransferCenterId = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "istanbul@gmail.com")).Id,

          Address = new Address()
          {
            City = "İstanbul",
            Street = "Kurtköy",
            Number = 209
          }
        },
        autoSave: true
      );

      await _agencyRepository.InsertAsync(
        new Agency()
        {
          UnitName = "Adana Pozantı Agency",
          ManagerName = "Murat",
          ManagerSurname = "Gül",
          ManagerGsm = "05347213301",
          UnitPhone = "04120353301",
          ManagerMail = "murat@gmail.com",
          UnitMail = "adanapozanti@gmail.com",
          TransferCenterId = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "adana@gmail.com")).Id,

          Address = new Address()
          {
            City = "Adana",
            Street = "Pozantı",
            Number = 582
          }
        },
        autoSave: true
      );

      await _agencyRepository.InsertAsync(
        new Agency()
        {
          UnitName = "Adana Aladağ Agency",
          ManagerName = "Bekir",
          ManagerSurname = "Sarı",
          ManagerGsm = "05347213302",
          UnitPhone = "04120353302",
          ManagerMail = "bekir@gmail.com",
          UnitMail = "adanaaladag@gmail.com",
          TransferCenterId = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "adana@gmail.com")).Id,

          Address = new Address()
          {
            City = "Adana",
            Street = "Aladağ",
            Number = 388
          }
        },
        autoSave: true
      );

      await _agencyRepository.InsertAsync(
        new Agency()
        {
          UnitName = "Diyarbakır Bağlar Agency",
          ManagerName = "Mehmet",
          ManagerSurname = "Işık",
          ManagerGsm = "05347214401",
          UnitPhone = "04120354401",
          ManagerMail = "mehmet@gmail.com",
          UnitMail = "diyarbakirbaglar@gmail.com",
          TransferCenterId = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "diyarbakir@gmail.com")).Id,

          Address = new Address()
          {
            City = "Diyarbakır",
            Street = "Bağlar",
            Number = 342
          }
        },
        autoSave: true
      );

      await _agencyRepository.InsertAsync(
        new Agency()
        {
          UnitName = "Diyarbakır Suriçi Agency",
          ManagerName = "Halil",
          ManagerSurname = "Aktaş",
          ManagerGsm = "05347214402",
          UnitPhone = "04120354402",
          ManagerMail = "halil@gmail.com",
          UnitMail = "diyarbakirsurici@gmail.com",
          TransferCenterId = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "diyarbakir@gmail.com")).Id,

          Address = new Address()
          {
            City = "Diyarbakır",
            Street = "Suriçi",
            Number = 089
          }
        },
        autoSave: true
      );

      await _agencyRepository.InsertAsync(
        new Agency()
        {
          UnitName = "Diyarbakır Hani Agency",
          ManagerName = "Adem",
          ManagerSurname = "Yılmaz",
          ManagerGsm = "05347214403",
          UnitPhone = "04120354403",
          ManagerMail = "adem@gmail.com",
          UnitMail = "diyarbakirhani@gmail.com",
          TransferCenterId = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "diyarbakir@gmail.com")).Id,

          Address = new Address()
          {
            City = "Diyarbakır",
            Street = "Hani",
            Number = 987
          }
        },
        autoSave: true
      );

    }
    #endregion

    #region Line
    private async Task SeedLineAsync()
    {
      if (await _lineRepository.GetCountAsync() > 0)
        return;

      var line1 = "line 1";
      await CheckForAddingLine(line1);

      await _lineRepository.InsertAsync(
        new Line()
        {
          Name = line1,
          IsActive = true,
          LineType = LineType.SubLine
        },
        autoSave: true
      );

      var line2 = "line 2";
      await CheckForAddingLine(line2);
      await _lineRepository.InsertAsync(
        new Line()
        {
          Name = line2,
          IsActive = false,
          LineType = LineType.SubLine
        },
        autoSave: true
      );

      var line3 = "line 3";
      await CheckForAddingLine(line3);
      await _lineRepository.InsertAsync(
        new Line()
        {
          Name = line3,
          IsActive = true,
          LineType = LineType.MainLine
        },
        autoSave: true
      );
    }

    private async Task CheckForAddingLine(string lineName)
    {
      if (await _lineRepository.AnyAsync(x => x.Name == lineName))
        throw new UserFriendlyException("BURADA SABİT MESAJ KULLANILACAK");
    }

    #endregion

    #region Station
    private async Task SeedStationAsync()
    {
      if (await _stationRepository.GetCountAsync() > 0)
        return;

      var LineId1 = (await _lineRepository.FirstOrDefaultAsync(x => x.Name == "line 1")).Id;
      var UnitId1 = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "diyarbakir@gmail.com")).Id;
      var UnitId2 = (await _agencyRepository.FirstOrDefaultAsync(x => x.UnitMail == "diyarbakirbaglar@gmail.com")).Id;
      var UnitId3 = (await _agencyRepository.FirstOrDefaultAsync(x => x.UnitMail == "diyarbakirsurici@gmail.com")).Id;
      var UnitId4 = (await _agencyRepository.FirstOrDefaultAsync(x => x.UnitMail == "diyarbakirhani@gmail.com")).Id;

      var LineId2 = (await _lineRepository.FirstOrDefaultAsync(x => x.Name == "line 2")).Id;
      var UnitId5 = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "adana@gmail.com")).Id;
      var UnitId6 = (await _agencyRepository.FirstOrDefaultAsync(x => x.UnitMail == "adanapozanti@gmail.com")).Id;
      var UnitId7 = (await _agencyRepository.FirstOrDefaultAsync(x => x.UnitMail == "adanaaladag@gmail.com")).Id;

      var LineId3 = (await _lineRepository.FirstOrDefaultAsync(x => x.Name == "line 3")).Id;
      var UnitId8 = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "istanbul@gmail.com")).Id;
      var UnitId9 = (await _transferCenterRepository.FirstOrDefaultAsync(x => x.UnitMail == "ankara@gmail.com")).Id;

   
      await CheckForAddingStation(LineId1);
      await _stationRepository.InsertAsync(
        new Station(LineId1, UnitId1, await StationNumberGenerator(LineId1)),
        autoSave: true
      );

      await CheckForAddingStation(LineId1);
      await _stationRepository.InsertAsync(
        new Station(LineId1, UnitId2, await StationNumberGenerator(LineId1)),
        autoSave: true
      );

      await CheckForAddingStation(LineId1);
      await _stationRepository.InsertAsync(
        new Station(LineId1, UnitId3, await StationNumberGenerator(LineId1)),
        autoSave: true
      );

      await CheckForAddingStation(LineId1);
      await _stationRepository.InsertAsync(
        new Station(LineId1, UnitId4, await StationNumberGenerator(LineId1)),
        autoSave: true
      );

      await CheckForAddingStation(LineId2);
      await _stationRepository.InsertAsync(
        new Station(LineId2, UnitId5, await StationNumberGenerator(LineId2)),
        autoSave: true
      );

      await CheckForAddingStation(LineId2);
      await _stationRepository.InsertAsync(
        new Station(LineId2, UnitId6, await StationNumberGenerator(LineId2)),
        autoSave: true
      );

      await CheckForAddingStation(LineId2);
      await _stationRepository.InsertAsync(
        new Station(LineId2, UnitId7, await StationNumberGenerator(LineId2)),
        autoSave: true
      );

      await CheckForAddingStation(LineId3);
      await _stationRepository.InsertAsync(
        new Station(LineId3, UnitId1, await StationNumberGenerator(LineId3)),
        autoSave: true
      );

      await CheckForAddingStation(LineId3);
      await _stationRepository.InsertAsync(
        new Station(LineId3, UnitId5, await StationNumberGenerator(LineId3)),
        autoSave: true
      );

      await CheckForAddingStation(LineId3);
      await _stationRepository.InsertAsync(
        new Station(LineId3, UnitId8, await StationNumberGenerator(LineId3)),
        autoSave: true
      );

      await CheckForAddingStation(LineId3);
      await _stationRepository.InsertAsync(
        new Station(LineId3, UnitId9, await StationNumberGenerator(LineId3)),
        autoSave: true
      );
    }

    private async Task<int> StationNumberGenerator(int lineId)
    {
      if (await _stationRepository.CountAsync(x => x.LineId == lineId) <= 0)
      {
        return 1;
      }

      var stations = await _stationRepository.GetQueryableAsync();

      var maxStationNumber = stations
          .Where(x => x.LineId == lineId)
          .Max(x => x.StationNumber);

      if (maxStationNumber == 10)
        throw new UserFriendlyException("BURADA SABİT MESAJ KULLANILACAK");

      return maxStationNumber + 1;
    }

    private async Task CheckForAddingStation(int lineId)
    {
      if (await _stationRepository.CountAsync(x => x.LineId == lineId) > LineConst.LimitOfStation)
        throw new UserFriendlyException("BURADA SABİT MESAJ KULLANILACAK");
    }
    #endregion


  }
}
