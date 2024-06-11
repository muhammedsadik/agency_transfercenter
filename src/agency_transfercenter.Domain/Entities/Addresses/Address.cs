using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Values;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace agency_transfercenter.Entities.Addresses
{
  [Owned]
  public class Address : ValueObject
  {
    public string City { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }

    internal Address()
    {
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
      yield return City;
      yield return Street;
      yield return Number;
    }

  }
}
