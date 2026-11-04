using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Core.Domain.Entities;

public partial class HotelScaffoldDbContext : DbContext
{
    public HotelScaffoldDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BusinessRole> BusinessRoles { get; set; }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<HotelMembership> HotelMemberships { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationMember> OrganizationMembers { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.ConcurrencyStamp).HasColumnName("concurrencyStamp");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.NormalizedName)
                .HasMaxLength(256)
                .HasColumnName("normalizedName");
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_roleId");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClaimType).HasColumnName("claimType");
            entity.Property(e => e.ClaimValue).HasColumnName("claimValue");
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("roleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.AccessFailedCount).HasColumnName("accessFailedCount");
            entity.Property(e => e.ConcurrencyStamp).HasColumnName("concurrencyStamp");
            entity.Property(e => e.DisplayName).HasColumnName("displayName");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.EmailConfirmed).HasColumnName("emailConfirmed");
            entity.Property(e => e.LockoutEnabled).HasColumnName("lockoutEnabled");
            entity.Property(e => e.LockoutEnd)
                .HasColumnType("datetime")
                .HasColumnName("lockoutEnd");
            entity.Property(e => e.NormalizedEmail)
                .HasMaxLength(256)
                .HasColumnName("normalizedEmail");
            entity.Property(e => e.NormalizedUserName)
                .HasMaxLength(256)
                .HasColumnName("normalizedUserName");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("phoneNumberConfirmed");
            entity.Property(e => e.SecurityStamp).HasColumnName("securityStamp");
            entity.Property(e => e.TwoFactorEnabled).HasColumnName("twoFactorEnabled");
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .HasColumnName("userName");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_roleId");
                        j.IndexerProperty<Guid>("UserId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("userId");
                        j.IndexerProperty<Guid>("RoleId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("roleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_userId");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClaimType).HasColumnName("claimType");
            entity.Property(e => e.ClaimValue).HasColumnName("claimValue");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_userId");

            entity.Property(e => e.LoginProvider)
                .HasDefaultValueSql("''")
                .HasColumnName("loginProvider");
            entity.Property(e => e.ProviderKey)
                .HasDefaultValueSql("''")
                .HasColumnName("providerKey");
            entity.Property(e => e.ProviderDisplayName).HasColumnName("providerDisplayName");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("userId");
            entity.Property(e => e.LoginProvider)
                .HasDefaultValueSql("''")
                .HasColumnName("loginProvider");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BusinessRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("business_roles");

            entity.HasIndex(e => new { e.HotelId, e.Code }, "IX_business_roles_hotelId_code").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.Code }, "IX_business_roles_organizationId_code").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("code");
            entity.Property(e => e.CreatedAtUtc)
                .HasMaxLength(6)
                .HasDefaultValueSql("'0001-01-01 00:00:00.000000'")
                .HasColumnName("createdAtUtc");
            entity.Property(e => e.HotelId).HasColumnName("hotelId");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organizationId");
            entity.Property(e => e.Scope).HasColumnName("scope");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                    l => l.HasOne<BusinessRole>().WithMany().HasForeignKey("RoleId"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("role_permissions");
                        j.HasIndex(new[] { "PermissionId" }, "IX_role_permissions_permissionId");
                        j.IndexerProperty<Guid>("RoleId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("roleId");
                        j.IndexerProperty<Guid>("PermissionId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("permissionId");
                    });
        });

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("hotels");

            entity.HasIndex(e => new { e.OrganizationId, e.Code }, "IX_hotels_organizationId_code").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("code");

            entity.Property(e => e.CreatedAtUtc)
                .HasMaxLength(6)
                .HasDefaultValueSql("'0001-01-01 00:00:00.000000'")
                .HasColumnName("createdAtUtc");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            // entity.Property(e => e.Phone)
            //     .HasMaxLength(32)
            //     .HasColumnName("phone");

            entity.Property(e => e.OrganizationId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("organizationId");

            entity.HasOne(d => d.Organization).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<HotelMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("hotel_memberships");

            entity.HasIndex(e => e.HotelId, "IX_hotel_memberships_hotelId");

            entity.HasIndex(e => new { e.OrganizationMemberId, e.HotelId }, "IX_hotel_memberships_organizationMemberId_hotelId").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAtUtc)
                .HasMaxLength(6)
                .HasDefaultValueSql("'0001-01-01 00:00:00.000000'")
                .HasColumnName("createdAtUtc");
            entity.Property(e => e.HotelId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("hotelId");
            entity.Property(e => e.OrganizationMemberId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("organizationMemberId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Hotel).WithMany(p => p.HotelMemberships)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.OrganizationMember).WithMany(p => p.HotelMemberships)
                .HasForeignKey(d => d.OrganizationMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.Roles).WithMany(p => p.HotelMemberships)
                .UsingEntity<Dictionary<string, object>>(
                    "HotelMembershipRole",
                    r => r.HasOne<BusinessRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<HotelMembership>().WithMany().HasForeignKey("HotelMembershipId"),
                    j =>
                    {
                        j.HasKey("HotelMembershipId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("hotel_membership_roles");
                        j.HasIndex(new[] { "RoleId" }, "IX_hotel_membership_roles_roleId");
                        j.IndexerProperty<Guid>("HotelMembershipId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("hotelMembershipId");
                        j.IndexerProperty<Guid>("RoleId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("roleId");
                    });
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("organizations");

            entity.HasIndex(e => e.Code, "IX_organizations_code").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Logo)
                .HasMaxLength(2048)
                .HasColumnName("logo");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(32)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("code");
            entity.Property(e => e.CreatedAtUtc)
                .HasMaxLength(6)
                .HasDefaultValueSql("'0001-01-01 00:00:00.000000'")
                .HasColumnName("createdAtUtc");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
        });

        modelBuilder.Entity<OrganizationMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("organization_members");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "IX_organization_members_organizationId_userId").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAtUtc)
                .HasMaxLength(6)
                .HasDefaultValueSql("'0001-01-01 00:00:00.000000'")
                .HasColumnName("createdAtUtc");
            entity.Property(e => e.OrganizationId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("organizationId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("userId");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationMembers)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.Roles).WithMany(p => p.OrganizationMembers)
                .UsingEntity<Dictionary<string, object>>(
                    "OrganizationMemberRole",
                    r => r.HasOne<BusinessRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<OrganizationMember>().WithMany()
                        .HasForeignKey("OrganizationMemberId")
                        .HasConstraintName("FK_organization_member_roles_organization_members_organizationM~"),
                    j =>
                    {
                        j.HasKey("OrganizationMemberId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("organization_member_roles");
                        j.HasIndex(new[] { "RoleId" }, "IX_organization_member_roles_roleId");
                        j.IndexerProperty<Guid>("OrganizationMemberId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("organizationMemberId");
                        j.IndexerProperty<Guid>("RoleId")
                            .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                            .HasColumnName("roleId");
                    });
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Code, "IX_permissions_code").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(120)
                .HasDefaultValueSql("''")
                .HasColumnName("code");
            entity.Property(e => e.CreatedAtUtc)
                .HasMaxLength(6)
                .HasDefaultValueSql("'0001-01-01 00:00:00.000000'")
                .HasColumnName("createdAtUtc");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasDefaultValueSql("''")
                .HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
