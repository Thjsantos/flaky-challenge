using Tests.Pages;

namespace Tests;

public class ProductListTests : HefestoTest
{
    // FIXED (example 1): was Thread.Sleep(800) + CountAsync().
    // The catalog arrives after a random 0-1500 ms delay, so a fixed sleep is either too long or too short.
    // Expect(...).ToHaveCountAsync() retries until the count matches or the timeout (5 s) expires.
    [Fact]
    public async Task Shows_all_products()
    {
        var catalog = new CatalogPage(Page);
        await catalog.GotoAsync();

        await Expect(catalog.Cards).ToHaveCountAsync(5);
    }

    // FIXED (example 2): was CountAsync() right after GotoAsync.
    // GotoAsync returns on the "load" event, but the cards are rendered later by JavaScript
    // (after fetching products.json). CountAsync() reads once and never waits; the web-first assertion does.
    [Fact]
    public async Task Anime_tab_shows_three_products()
    {
        var catalog = new CatalogPage(Page);
        await catalog.GotoAsync("anime");

        await Expect(catalog.Cards).ToHaveCountAsync(3);
    }

    [Fact]
    public async Task Search_filters_by_name()
    {
        var catalog = new CatalogPage(Page);
        await catalog.GotoAsync();
        await catalog.SearchAsync("frieren");
        await Task.Delay(200);

        await Expect(catalog.Cards).ToHaveCountAsync(1);
    }

    [Fact]
    public async Task Search_without_match_shows_empty_message()
    {
        var catalog = new CatalogPage(Page);
        await catalog.GotoAsync();
        await catalog.SearchAsync("zzzz");

        Assert.True(await catalog.EmptyMessage.IsVisibleAsync());
    }
}
