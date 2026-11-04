namespace Tests;

public class ProductListTests : HefestoTest
{
    // FIXED (example 1): was Thread.Sleep(800) + CountAsync().
    // The catalog arrives after a random 0-1500 ms delay, so a fixed sleep is either too long or too short.
    // Expect(...).ToHaveCountAsync() retries until the count matches or the timeout (5 s) expires.
    [Fact]
    public async Task Shows_all_products()
    {
        await Page.GotoAsync("catalogo.html");

        await Expect(Page.Locator("#grid article.card")).ToHaveCountAsync(5);
    }

    // FIXED (example 2): was CountAsync() right after GotoAsync.
    // GotoAsync returns on the "load" event, but the cards are rendered later by JavaScript
    // (after fetching products.json). CountAsync() reads once and never waits; the web-first assertion does.
    [Fact]
    public async Task Anime_tab_shows_three_products()
    {
        await Page.GotoAsync("catalogo.html#anime");

        await Expect(Page.Locator("#grid article.card")).ToHaveCountAsync(3);
    }

    [Fact]
    public async Task Search_filters_by_name()
    {
        await Page.GotoAsync("catalogo.html");
        await Page.FillAsync("#q", "frieren");
        await Task.Delay(200);

        await Expect(Page.Locator("#grid article.card")).ToHaveCountAsync(1);
    }

    [Fact]
    public async Task Search_without_match_shows_empty_message()
    {
        await Page.GotoAsync("catalogo.html");
        await Page.FillAsync("#q", "zzzz");

        Assert.True(await Page.Locator("#empty").IsVisibleAsync());
    }
}
