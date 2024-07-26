using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace agency_transfercenter.TransferCenter
{
  public class TransferCenterAppService_Tests<TStartupModule> : agency_transfercenterApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
  {


    public TransferCenterAppService_Tests()
    {

    }


  }
}
