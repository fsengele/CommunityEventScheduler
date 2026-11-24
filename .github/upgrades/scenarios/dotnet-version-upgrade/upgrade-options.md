# Upgrade Options — CommunityEventScheduler

Assessment: One SDK-style ASP.NET Core Razor Pages project, net10.0 → net11.0 preview; 12 API issues and 6 package upgrades recommended.

## Strategy

### Upgrade Strategy
A single modern .NET project with no project dependency graph is best handled in one atomic pass.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects simultaneously in a single atomic pass; fastest approach with no multi-targeting overhead. |

## Compatibility

### Unsupported API Handling
The assessment identifies 11 source-incompatible APIs and one behavioral change in ASP.NET Core Identity and exception/migrations middleware APIs.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve every API change in the upgrade task, including complex changes, leaving no deferred work. |
| Defer Complex Changes | Apply simple replacements inline, create minimal compilable stubs for complex changes, and resolve them in follow-up tasks. |
