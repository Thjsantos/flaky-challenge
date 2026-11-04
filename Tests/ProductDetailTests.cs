namespace Tests;

public class ProductDetailTests : HefestoTest
{
    private const string Deku = "produto.html?id=deku-my-hero-academy";

    [Fact]
    public async Task Title_contains_product_name()
    {
        await Page.GotoAsync(Deku);

        Assert.Contains("Deku", await Page.TitleAsync());
    }

    [Fact]
    public async Task Product_page_has_two_scales()
    {
        await Page.GotoAsync(Deku);

        var count = await Page.Locator(".scales button").CountAsync();
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task Changing_scale_updates_price()
    {
        await Page.GotoAsync(Deku);
        var before = await Page.Locator("#price").InnerTextAsync();

        await Page.Locator(".scales button").Nth(1).ClickAsync();

        var after = await Page.Locator("#price").InnerTextAsync();
        Assert.NotEqual(before, after);
    }

    [Fact]
    public async Task Catalog_card_opens_product_page()
    {
        await Page.GotoAsync("catalogo.html");
        await Page.Locator("#grid article.card a.pic").First.ClickAsync();

        Assert.Contains("produto.html?id=", Page.Url);
        Assert.True(await Page.Locator("h1").IsVisibleAsync());
    }
}
