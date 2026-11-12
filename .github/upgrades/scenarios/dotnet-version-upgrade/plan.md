# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade the CommunityEventScheduler Razor Pages application from .NET 10 to .NET 11 preview (`net11.0`), including compatible ASP.NET Core/EF Core packages and required API fixes.
**Scope**: One SDK-style ASP.NET Core project with approximately 9,066 lines of code, 52 NuGet packages, 12 identified API incidents, and 6 recommended package updates.

## Tasks

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: One SDK-style project targeting modern .NET with no project dependency graph.

### 01-upgrade-razor-pages-app: Upgrade the Razor Pages application to .NET 11 preview

Verify the installed .NET 11 preview SDK/toolchain, then update `CommunityEvenScheduler/CommunityEventScheduler.csproj` from `net10.0` to `net11.0` and align the ASP.NET Core Identity and Entity Framework Core package references with versions compatible with the .NET 11 preview. Restore the solution and resolve the 11 source-incompatible APIs and one behavioral change identified by the assessment, focusing on Identity registration/context APIs and exception/migrations middleware in the Razor Pages startup path.

Research the current project startup code, package compatibility, global SDK configuration, and affected API call sites before editing. Preserve the existing nullable and implicit-using settings and ensure database migrations remain consistent with the application model.

**Done when**: The project targets `net11.0`, all selected package references restore successfully, all identified API changes are resolved inline, and the project compiles without errors or warnings.

---

### 02-validate-solution: Validate the upgraded solution

Run a clean restore/build of the full solution after the atomic upgrade, execute all discovered tests, and review the resulting warnings and runtime-sensitive changes. Confirm that the Razor Pages application, Identity/EF Core integration, migrations, and existing project assets remain consistent; document any preview-specific limitations or deferred recommendations.

**Done when**: The full solution builds with zero errors and warnings, all tests pass, and validation results are recorded for the completed upgrade.
