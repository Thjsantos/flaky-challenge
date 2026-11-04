using Microsoft.Playwright.Xunit;

namespace Tests;

/// <summary>
/// Base class for all tests. Points the browser at the live site and adds a random 0-1500 ms delay
/// to the catalog request (data/products.json), standing in for a slow, jittery backend.
/// Do not remove or reduce this delay: the tests must cope with it.
/// </summary>
public abstract class HefestoTest : PageTest
{
    public override BrowserNewContextOptions ContextOptions() => new()
    {
        BaseURL = "https://thjsantos.github.io/hefesto-studio/",
        ServiceWorkers = ServiceWorkerPolicy.Block,
    };

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await Context.RouteAsync("**/data/products.json", async route =>
        {
            await Task.Delay(Random.Shared.Next(0, 1500));
            await route.ContinueAsync();
        });
    }
}
