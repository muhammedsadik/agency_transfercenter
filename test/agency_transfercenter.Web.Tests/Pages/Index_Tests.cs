using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace agency_transfercenter.Pages;

public class Index_Tests : agency_transfercenterWebTestBase
{
    //[Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
