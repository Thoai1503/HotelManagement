# Graph Report - HotelManagement  (2026-11-04)

## Corpus Check
- cluster-only mode — file stats not available

## Summary
- 482 nodes · 880 edges · 45 communities (25 shown, 20 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 34 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `fe285606`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Community 0
- Community 1
- Community 2
- Community 3
- Community 4
- Community 5
- Community 6
- Community 7
- Community 8
- Community 9
- Community 10
- Community 11
- Community 12
- Community 13
- Community 14
- Community 15
- Community 16
- Community 17
- Community 18
- Community 19
- Community 20
- Community 21
- Community 22
- Community 23
- Community 25
- Community 26
- Community 27
- Community 28
- Community 29
- Community 30
- Community 31
- Community 32
- Community 33
- Community 34
- Community 35
- Community 36
- Community 37
- Community 38
- Community 39

## God Nodes (most connected - your core abstractions)
1. `HotelScaffoldDbContext` - 32 edges
2. `AspNetUser` - 30 edges
3. `Hotel` - 21 edges
4. `HotelModal` - 21 edges
5. `OrganizationModal` - 20 edges
6. `BusinessRole` - 19 edges
7. `Organization` - 19 edges
8. `Hotel.Core.Domain.Entities` - 17 edges
9. `HotelMembership` - 17 edges
10. `OrganizationMember` - 17 edges

## Surprising Connections (you probably didn't know these)
- `HotelDbContextFactory` --references--> `HotelDbContext`  [EXTRACTED]
  Hotel.Web/HotelDbContextFactory.cs → Hotel.Core/Domain/Entities/HotelDbContext.cs
- `HotelController` --references--> `HotelService`  [EXTRACTED]
  Hotel.Web/Areas/Tenant/Controllers/HotelController.cs → Hotel.Core/Domain/Service/Tenant/HotelService.cs
- `HotelController` --references--> `OrganizationService`  [EXTRACTED]
  Hotel.Web/Areas/Tenant/Controllers/HotelController.cs → Hotel.Core/Domain/Service/Tenant/OrganizationService.cs
- `OrganizationController` --references--> `OrganizationService`  [EXTRACTED]
  Hotel.Web/Areas/Tenant/Controllers/OrganizationController.cs → Hotel.Core/Domain/Service/Tenant/OrganizationService.cs
- `AspNetRole` --references--> `AspNetRoleClaim`  [EXTRACTED]
  Hotel.Core/Domain/Entities/AspNetRole.cs → Hotel.Core/Domain/Entities/AspNetRoleClaim.cs

## Import Cycles
- None detected.

## Communities (45 total, 20 thin omitted)

### Community 0 - "Community 0"
Cohesion: 0.09
Nodes (24): HotelModal, address, code, email, id, isActive, name, organizationId (+16 more)

### Community 1 - "Community 1"
Cohesion: 0.06
Nodes (15): Hotel.Core.Domain.Infrastructure.Models, Hotel.Web, Hotel.Core.Domain.Repositories.Tenant, Hotel.Web.Controllers, Hotel.Core.Domain.Service.Tenant, Hotel.Core.Domain.Repositories, Hotel.Web.Areas.Tenant.Controllers, Hotel.Core.Domain.Service (+7 more)

### Community 2 - "Community 2"
Cohesion: 0.10
Nodes (6): Hotel.Core.Domain.Entities, Hotel.Core.Migrations, Hotel.Core.Domain.Helpers, Hotel.Core.Domain.Entities.Migrations, HotelDbContext, HotelDbContextFactory

### Community 3 - "Community 3"
Cohesion: 0.09
Nodes (15): SearchOptions, Example, IsSearchAll, Limit, Offset, Sort, SearchResult, Count (+7 more)

### Community 4 - "Community 4"
Cohesion: 0.08
Nodes (25): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+17 more)

### Community 5 - "Community 5"
Cohesion: 0.08
Nodes (25): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+17 more)

### Community 6 - "Community 6"
Cohesion: 0.17
Nodes (4): EntityModelMapper, BaseRepository, Entities, IBaseRepository

### Community 7 - "Community 7"
Cohesion: 0.10
Nodes (21): AspNetUser, AccessFailedCount, AspNetUserClaims, AspNetUserLogins, AspNetUserTokens, ConcurrencyStamp, DisplayName, Email (+13 more)

### Community 8 - "Community 8"
Cohesion: 0.16
Nodes (4): AddOrganizationContactAndStatus, AddHotelInfo, AddHotelContactAndStatus, InitialCreate

### Community 9 - "Community 9"
Cohesion: 0.16
Nodes (5): BaseRepository, Connection, DbContext, Transaction, IBaseRepository

### Community 10 - "Community 10"
Cohesion: 0.14
Nodes (14): HotelScaffoldDbContext, AspNetRoleClaims, AspNetRoles, AspNetUserClaims, AspNetUserLogins, AspNetUsers, AspNetUserTokens, BusinessRoles (+6 more)

### Community 11 - "Community 11"
Cohesion: 0.17
Nodes (12): Hotel, Address, Code, CreatedAtUtc, Email, HotelMemberships, Id, IsActive (+4 more)

### Community 12 - "Community 12"
Cohesion: 0.18
Nodes (11): BusinessRole, Code, CreatedAtUtc, HotelId, HotelMemberships, Id, Name, OrganizationId (+3 more)

### Community 13 - "Community 13"
Cohesion: 0.18
Nodes (11): Organization, Code, CreatedAtUtc, Email, Hotels, Id, IsActive, Logo (+3 more)

### Community 14 - "Community 14"
Cohesion: 0.22
Nodes (9): HotelMembership, CreatedAtUtc, Hotel, HotelId, Id, OrganizationMember, OrganizationMemberId, Roles (+1 more)

### Community 15 - "Community 15"
Cohesion: 0.22
Nodes (9): OrganizationMember, CreatedAtUtc, HotelMemberships, Id, Organization, OrganizationId, Roles, Status (+1 more)

### Community 16 - "Community 16"
Cohesion: 0.29
Nodes (7): AspNetRole, AspNetRoleClaims, ConcurrencyStamp, Id, Name, NormalizedName, Users

### Community 17 - "Community 17"
Cohesion: 0.29
Nodes (6): Permission, Code, CreatedAtUtc, Description, Id, Roles

### Community 18 - "Community 18"
Cohesion: 0.29
Nodes (4): net8.0, Microsoft.EntityFrameworkCore.Design (8.0.31), Pomelo.EntityFrameworkCore.MySql (8.0.3), Microsoft.NET.Sdk.Web

### Community 19 - "Community 19"
Cohesion: 0.33
Nodes (6): AspNetRoleClaim, ClaimType, ClaimValue, Id, Role, RoleId

### Community 20 - "Community 20"
Cohesion: 0.33
Nodes (6): AspNetUserClaim, ClaimType, ClaimValue, Id, User, UserId

### Community 21 - "Community 21"
Cohesion: 0.33
Nodes (6): AspNetUserLogin, LoginProvider, ProviderDisplayName, ProviderKey, User, UserId

### Community 22 - "Community 22"
Cohesion: 0.33
Nodes (6): AspNetUserToken, LoginProvider, Name, User, UserId, Value

### Community 23 - "Community 23"
Cohesion: 0.33
Nodes (5): net8.0, Microsoft.EntityFrameworkCore.Design (8.0.31), Pomelo.EntityFrameworkCore.MySql (8.0.3), Microsoft.NET.Sdk.Web, Microsoft.EntityFrameworkCore (8.0.31)

### Community 26 - "Community 26"
Cohesion: 0.67
Nodes (3): EfmigrationsHistory, MigrationId, ProductVersion

## Knowledge Gaps
- **170 isolated node(s):** `Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`, `AspNetRoleClaims` (+165 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 238 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **20 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `HotelScaffoldDbContext` connect `Community 10` to `Community 2`, `Community 7`, `Community 9`, `Community 11`, `Community 12`, `Community 13`, `Community 14`, `Community 15`, `Community 16`, `Community 17`, `Community 19`, `Community 20`, `Community 21`, `Community 22`, `Community 24`, `Community 26`?**
  _High betweenness centrality (0.096) - this node is a cross-community bridge._
- **What connects `Id`, `Name`, `NormalizedName` to the rest of the system?**
  _170 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Community 0` be split into smaller, more focused modules?**
  _Cohesion score 0.08909995477159656 - nodes in this community are weakly interconnected._
- **Why does `AspNetUser` connect `Community 7` to `Community 0`, `Community 2`, `Community 10`, `Community 16`, `Community 17`, `Community 20`, `Community 21`, `Community 22`, `Community 24`, `Community 25`?**
  _High betweenness centrality (0.068) - this node is a cross-community bridge._
- **Should `Community 1` be split into smaller, more focused modules?**
  _Cohesion score 0.06382978723404255 - nodes in this community are weakly interconnected._
- **Why does `Hotel` connect `Community 11` to `Community 0`, `Community 1`, `Community 2`, `Community 10`, `Community 13`, `Community 14`, `Community 17`, `Community 24`, `Community 25`?**
  _High betweenness centrality (0.055) - this node is a cross-community bridge._
- **Should `Community 2` be split into smaller, more focused modules?**
  _Cohesion score 0.10404040404040404 - nodes in this community are weakly interconnected._