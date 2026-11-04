<#
.SYNOPSIS  Build and run the Playwright tests against the live Hefesto Studio site.
.EXAMPLE   ./scripts/run.ps1                 # Docker, one run
.EXAMPLE   ./scripts/run.ps1 -Repeat 5       # run the suite 5 times to expose flakiness
.EXAMPLE   ./scripts/run.ps1 -Filter "FullyQualifiedName~ProductDetail"
.EXAMPLE   ./scripts/run.ps1 -Local          # no Docker: needs .NET 8 SDK + PowerShell (pwsh)
#>
param(
  [int]$Repeat = 1,
  [string]$Filter = "",
  [switch]$Local
)
$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$extra = @()
if ($Filter) { $extra = @("--filter", $Filter) }

if ($Local) {
  Push-Location $root
  try {
    dotnet build Tests/Tests.csproj -nologo -v q
    pwsh Tests/bin/Debug/net8.0/playwright.ps1 install chromium
    $green = 0; $red = 0
    1..$Repeat | ForEach-Object {
      Write-Host "=== run $_/$Repeat ==="
      dotnet test Tests/Tests.csproj --no-build -nologo @extra
      if ($LASTEXITCODE -eq 0) { $green++ } else { $red++ }
    }
    if ($Repeat -gt 1) { Write-Host "SUMMARY: $green green runs, $red red runs out of $Repeat" }
  } finally { Pop-Location }
  return
}

docker run --rm -it -e REPEAT=$Repeat `
  -v "${root}:/work" -w /work `
  mcr.microsoft.com/playwright/dotnet:v1.49.0-noble `
  bash scripts/inside.sh @extra
