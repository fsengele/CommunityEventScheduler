# 01-upgrade-razor-pages-app: Upgrade the Razor Pages application to .NET 11 preview

Verify the installed .NET 11 preview SDK/toolchain, then update `CommunityEvenScheduler/CommunityEventScheduler.csproj` from `net10.0` to `net11.0` and align the ASP.NET Core Identity and Entity Framework Core package references with versions compatible with the .NET 11 preview. Restore the solution and resolve the 11 source-incompatible APIs and one behavioral change identified by the assessment, focusing on Identity registration/context APIs and exception/migrations middleware in the Razor Pages startup path.

Research the current project startup code, package compatibility, global SDK configuration, and affected API call sites before editing. Preserve the existing nullable and implicit-using settings and ensure database migrations remain consistent with the application model.

## Research Findings

- **Project**: `CommunityEvenScheduler/CommunityEventScheduler.csproj`; one SDK-style ASP.NET Core project, no project references or dependents.
- **Toolchain**: .NET SDKs `10.0.401` and `11.0.100` are installed; no `global.json` was found.
- **Package management**: Standard `PackageReference` versions are defined directly in the project file; no central package management imports were reported.
- **Assessment findings**: 19 issues total: one mandatory TFM change, six package upgrade recommendations, 11 source-incompatible API incidents, and one behavioral change. The affected source files are `Program.cs` and `Data/ApplicationDbContext.cs`.
- **Package updates**: `dotnet list package --outdated` reports `11.0.0` as latest for all six explicit ASP.NET Core/EF Core packages. The assessment's earlier `10.0.15` suggestions are superseded by the target-framework-specific package query.
- **Security**: The baseline restore reports NU1902 and NU1903 for transitive `System.Security.Cryptography.Pkcs` 9.0.11. The upgrade should attempt to resolve these through the .NET 11 dependency graph; do not suppress the warnings.
- **API call sites**: `Program.cs` uses `AddDatabaseDeveloperPageExceptionFilter`, Identity registration, `UseMigrationsEndPoint`, and `UseExceptionHandler`; `Data/ApplicationDbContext.cs` derives from `IdentityDbContext`.
- **Stubs**: No `// STUB:` markers were found in the project source.

**Done when**: The project targets `net11.0`, all selected package references restore successfully, all identified API changes are resolved inline, and the project compiles without errors or warnings.
