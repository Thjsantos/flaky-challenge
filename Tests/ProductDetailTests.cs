using Tests.Pages;

namespace Tests;

public class ProductDetailTests : HefestoTest
{
    [Fact]
    public async Task Title_contains_product_name()
    {
        var product = new ProductPage(Page);
        await product.GotoAsync(ProductPage.Deku);

        Assert.Contains("Deku", await product.TitleAsync());
    }

    [Fact]
    public async Task Product_page_has_two_scales()
    {
        var product = new ProductPage(Page);
        await product.GotoAsync(ProductPage.Deku);

        var count = await product.ScaleButtons.CountAsync();
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task Changing_scale_updates_price()
    {
        var product = new ProductPage(Page);
        await product.GotoAsync(ProductPage.Deku);
        var before = await product.Price.InnerTextAsync();

        await product.SelectScaleAsync(1);

        var after = await product.Price.InnerTextAsync();
        Assert.NotEqual(before, after);
    }

    [Fact]
    public async Task Catalog_card_opens_product_page()
    {
        var catalog = new CatalogPage(Page);
        await catalog.GotoAsync();
        await catalog.OpenFirstProductAsync();

        var product = new ProductPage(Page);
        Assert.Contains("produto.html?id=", product.Url);
        Assert.True(await product.Heading.IsVisibleAsync());
    }
}
