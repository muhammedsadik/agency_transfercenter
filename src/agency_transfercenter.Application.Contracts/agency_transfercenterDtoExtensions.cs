using agency_transfercenter.EntityConsts.UserConts;
using Microsoft.Extensions.Options;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace agency_transfercenter;

public static class agency_transfercenterDtoExtensions
{
  private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

  public static void Configure()
  {
    OneTimeRunner.Run(() =>
    {
      /*
       https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Overriding-Services#definition-check 
      */
      ObjectExtensionManager.Instance
        .AddOrUpdateProperty<int?>(
          new[]
          {
              typeof(IdentityUserDto),
              typeof(GetIdentityUsersInput),
              typeof(IdentityUserCreateDto),
              typeof(IdentityUserUpdateDto),
          },
          UserConst.UserUnitId,
          options =>
          {
          }
        );
      

      /* You can add extension properties to DTOs
       * defined in the depended modules.
       *
       * Example:
       *
       * ObjectExtensionManager.Instance
       *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
       *
       * See the documentation for more:
       * https://docs.abp.io/en/abp/latest/Object-Extensions
       */
    });
  }
}
