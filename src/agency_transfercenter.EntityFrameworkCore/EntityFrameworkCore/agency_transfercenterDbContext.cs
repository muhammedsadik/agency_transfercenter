﻿using agency_transfercenter.Entities.Addresses;
using agency_transfercenter.Entities.Agencies;
using agency_transfercenter.Entities.Lines;
using agency_transfercenter.Entities.Stations;
using agency_transfercenter.Entities.TransferCenters;
using agency_transfercenter.Entities.Units;
using agency_transfercenter.EntityConsts.LineConsts;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace agency_transfercenter.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class agency_transfercenterDbContext :
    AbpDbContext<agency_transfercenterDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
  

  public DbSet<TransferCenter> TransferCenters { get; set; }
  public DbSet<Agency> Agencies { get; set; }
  public DbSet<Line> Lines { get; set; }
  public DbSet<Unit> Units { get; set; }
  public DbSet<Station> Stations { get; set; }

  #region Entities from the modules

  //Identity
  public DbSet<IdentityUser> Users { get; set; }
  public DbSet<IdentityRole> Roles { get; set; }
  public DbSet<IdentityClaimType> ClaimTypes { get; set; }
  public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
  public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
  public DbSet<IdentityLinkUser> LinkUsers { get; set; }
  public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

  // Tenant Management
  public DbSet<Tenant> Tenants { get; set; }
  public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

  #endregion

  public agency_transfercenterDbContext(DbContextOptions<agency_transfercenterDbContext> options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.ConfigurePermissionManagement();
    builder.ConfigureSettingManagement();
    builder.ConfigureBackgroundJobs();
    builder.ConfigureAuditLogging();
    builder.ConfigureIdentity();
    builder.ConfigureOpenIddict();
    builder.ConfigureFeatureManagement();
    builder.ConfigureTenantManagement();

    builder.Entity<Line>(b =>
    {
      b.ToTable(agency_transfercenterConsts.DbTablePrefix + "Lines", agency_transfercenterConsts.DbSchema);
      b.ConfigureByConvention();

      b.Property(x => x.Name).IsRequired().HasMaxLength(LineConst.MaxNameLength);
      b.HasIndex(x => x.Name).IsUnique();
      b.Property(x => x.LineType).IsRequired();

      b.HasMany(x => x.Stations).WithOne().HasForeignKey(x => x.LineId).IsRequired();

    });


    builder.Entity<Station>(b =>
    {
      b.HasKey(x => new { x.UnitId, x.LineId });
      b.HasOne(x => x.Line).WithMany(x => x.Stations).HasForeignKey(x => x.LineId);
      b.HasOne(x => x.Unit).WithMany(x => x.Stations).HasForeignKey(x => x.UnitId);

      b.ToTable(agency_transfercenterConsts.DbTablePrefix + "Stations", agency_transfercenterConsts.DbSchema);
      b.ConfigureByConvention();
    });


    builder.Entity<Agency>(b =>
    {
      b.HasOne(x => x.TransferCenter).WithMany(x => x.Agencies).HasForeignKey(x => x.TransferCenterId);

      b.Property(x => x.TransferCenterId).IsRequired();

      b.OwnsOne(x => x.Address);
    });

    builder.Entity<TransferCenter>(b =>
    {
      b.OwnsOne(x => x.Address);
    });
  }
}
