using Tests.Pages;

namespace Tests;

public class NavigationTests : HefestoTest
{
    [Fact]
    public async Task Home_button_opens_catalog()
    {
        var home = new HomePage(Page);
        await home.GotoAsync();
        await home.OpenCatalogAsync();

        Assert.EndsWith("catalogo.html", home.Url);
    }

    [Fact]
    public async Task Quote_button_opens_whatsapp()
    {
        var product = new ProductPage(Page);
        await product.GotoAsync(ProductPage.Deku);

        var popup = await product.ClickQuoteAsync();

        Assert.StartsWith("https://wa.me/", popup.Url);
    }
}
