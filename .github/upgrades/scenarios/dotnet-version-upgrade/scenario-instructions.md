# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net11.0 (.NET 11 preview)

## Source Control
- **Source Branch**: Modernize_.net11
- **Working Branch**: dotnet-version-upgrade-11
- **Pending Changes**: Committed before branch creation (`ae77e87`)
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## User Preferences
### Technical Preferences
- Target .NET 11 preview (`net11.0`), not a GA release or another framework.

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Compatibility
- Unsupported API Handling: Fix Inline

## Strategy
**Selected**: All-at-Once
**Rationale**: One SDK-style modern .NET Razor Pages project with no project dependency graph.

### Execution Constraints
- Upgrade the project and package references in one atomic pass.
- Verify the .NET 11 preview SDK before editing project files.
- Restore and build after the upgrade, fixing all compilation errors and warnings.
- Run tests only after the atomic upgrade builds successfully.

## Build Tool Decisions
- **CommunityEvenScheduler/CommunityEventScheduler.csproj**: dotnet build (SDK-style modern .NET project with no special build requirements)
