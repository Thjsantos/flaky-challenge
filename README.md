# Flaky test suite (practice)

A Playwright + xUnit (C#) end-to-end suite against the live site
https://thjsantos.github.io/hefesto-studio/ (tests in `/Tests`).
The suite is **flaky**: it passes on some runs and fails on others, with no code changes.

## Your task

Make the suite **reliably green** and be able to explain what caused each flake.

- Fix the **tests**. Don't remove the random delay in `Tests/HefestoTest.cs`; it stands in for a slow backend.
- Prioritize the most impactful problems first, and say what you'd do next if you ran out of time.
- Talk through your reasoning as you go.

## Running it

Docker, from the repo root in PowerShell (needs internet access):

```powershell
./scripts/run.ps1                 # one run
./scripts/run.ps1 -Repeat 5       # run the suite 5 times and summarize green/red
./scripts/run.ps1 -Filter "FullyQualifiedName~ProductDetail"
```

No Docker? Install the .NET 8 SDK and PowerShell (`pwsh`), then `./scripts/run.ps1 -Local -Repeat 5`.

`Tests/PlaywrightXunit/` is the Playwright xUnit helper (PageTest base class) vendored
because the NuGet package has no 1.49.0 build. You don't need to edit it.
