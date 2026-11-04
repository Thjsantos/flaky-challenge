namespace Tests;

public class NavigationTests : HefestoTest
{
    [Fact]
    public async Task Home_button_opens_catalog()
    {
        await Page.GotoAsync("index.html");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Ver catálogo" }).ClickAsync();

        Assert.EndsWith("catalogo.html", Page.Url);
    }

    [Fact]
    public async Task Quote_button_opens_whatsapp()
    {
        await Page.GotoAsync("produto.html?id=deku-my-hero-academy");

        var popup = await Page.RunAndWaitForPopupAsync(async () =>
            await Page.Locator("#quote").ClickAsync());

        Assert.StartsWith("https://wa.me/", popup.Url);
    }
}
