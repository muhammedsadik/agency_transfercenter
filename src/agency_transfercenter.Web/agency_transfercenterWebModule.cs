using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using agency_transfercenter.EntityFrameworkCore;
using agency_transfercenter.Localization;
using agency_transfercenter.MultiTenancy;
using agency_transfercenter.Web.Menus;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.OpenIddict;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace agency_transfercenter.Web;

[DependsOn(
    typeof(agency_transfercenterHttpApiModule),
    typeof(agency_transfercenterApplicationModule),
    typeof(agency_transfercenterEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
    )]
public class agency_transfercenterWebModule : AbpModule
{
  public override void PreConfigureServices(ServiceConfigurationContext context)
  {
    var hostingEnvironment = context.Services.GetHostingEnvironment();
    var configuration = context.Services.GetConfiguration();

    context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
    {
      options.AddAssemblyResource(
              typeof(agency_transfercenterResource),
              typeof(agency_transfercenterDomainModule).Assembly,
              typeof(agency_transfercenterDomainSharedModule).Assembly,
              typeof(agency_transfercenterApplicationModule).Assembly,
              typeof(agency_transfercenterApplicationContractsModule).Assembly,
              typeof(agency_transfercenterWebModule).Assembly
          );
    });

    PreConfigure<OpenIddictBuilder>(builder =>
    {
      builder.AddValidation(options =>
          {
          options.AddAudiences("agency_transfercenter");
          options.UseLocalServer();
          options.UseAspNetCore();
        });
    });

    if (!hostingEnvironment.IsDevelopment())
    {
      PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
      {
        options.AddDevelopmentEncryptionAndSigningCertificate = false;
      });

      PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
      {
        serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", "2cb01f41-c2cb-4780-bab6-f0bb86b11f82");
      });
    }
  }

  public override void ConfigureServices(ServiceConfigurationContext context)
  {
    var hostingEnvironment = context.Services.GetHostingEnvironment();
    var configuration = context.Services.GetConfiguration();

    ConfigureAuthentication(context);
    ConfigureUrls(configuration);
    ConfigureBundles();
    ConfigureAutoMapper();
    ConfigureVirtualFileSystem(hostingEnvironment);
    ConfigureNavigationServices();
    ConfigureAutoApiControllers();
    ConfigureSwaggerServices(context.Services);
  }

  private void ConfigureAuthentication(ServiceConfigurationContext context)
  {
    context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
    {
      options.IsDynamicClaimsEnabled = true;
    });
  }

  private void ConfigureUrls(IConfiguration configuration)
  {
    Configure<AppUrlOptions>(options =>
    {
      options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
    });
  }

  private void ConfigureBundles()
  {
    Configure<AbpBundlingOptions>(options =>
    {
      options.StyleBundles.Configure(
              LeptonXLiteThemeBundles.Styles.Global,
              bundle =>
              {
              bundle.AddFiles("/global-styles.css");
            }
          );
    });
  }

  private void ConfigureAutoMapper()
  {
    Configure<AbpAutoMapperOptions>(options =>
    {
      options.AddMaps<agency_transfercenterWebModule>();
    });
  }

  private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
  {
    if (hostingEnvironment.IsDevelopment())
    {
      Configure<AbpVirtualFileSystemOptions>(options =>
      {
        options.FileSets.ReplaceEmbeddedByPhysical<agency_transfercenterDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}agency_transfercenter.Domain.Shared"));
        options.FileSets.ReplaceEmbeddedByPhysical<agency_transfercenterDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}agency_transfercenter.Domain"));
        options.FileSets.ReplaceEmbeddedByPhysical<agency_transfercenterApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}agency_transfercenter.Application.Contracts"));
        options.FileSets.ReplaceEmbeddedByPhysical<agency_transfercenterApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}agency_transfercenter.Application"));
        options.FileSets.ReplaceEmbeddedByPhysical<agency_transfercenterWebModule>(hostingEnvironment.ContentRootPath);
      });
    }
  }

  private void ConfigureNavigationServices()
  {
    Configure<AbpNavigationOptions>(options =>
    {
      options.MenuContributors.Add(new agency_transfercenterMenuContributor());
    });
  }

  private void ConfigureAutoApiControllers()
  {
    Configure<AbpAspNetCoreMvcOptions>(options =>
    {
      options.ConventionalControllers.Create(typeof(agency_transfercenterApplicationModule).Assembly);
    });
  }

  private void ConfigureSwaggerServices(IServiceCollection services)
  {
    services.AddAbpSwaggerGen(
        options =>
        {
          options.SwaggerDoc("v1", new OpenApiInfo { Title = "agency_transfercenter API", Version = "v1" });
          options.DocInclusionPredicate((docName, description) => true);
          options.CustomSchemaIds(type => type.FullName);
          options.HideAbpEndpoints();
        }
    );
  }

  public override void OnApplicationInitialization(ApplicationInitializationContext context)
  {
    var app = context.GetApplicationBuilder();
    var env = context.GetEnvironment();

    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseAbpRequestLocalization();

    if (!env.IsDevelopment())
    {
      app.UseErrorPage();
    }

    app.UseCorrelationId();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAbpOpenIddictValidation();

    if (MultiTenancyConsts.IsEnabled)
    {
      app.UseMultiTenancy();
    }

    app.UseUnitOfWork();
    app.UseDynamicClaims();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseAbpSwaggerUI(options =>
    {
      options.SwaggerEndpoint("/swagger/v1/swagger.json", "agency_transfercenter API");
    });

    app.UseAuditing();
    app.UseAbpSerilogEnrichers();
    app.UseConfiguredEndpoints();
  }
}
