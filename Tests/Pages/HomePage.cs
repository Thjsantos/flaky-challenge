namespace Tests.Pages;

public class HomePage : BasePage
{
    public HomePage(IPage page) : base(page) { }

    public ILocator ViewCatalogLink => Page.GetByRole(AriaRole.Link, new() { Name = "Ver catálogo" });

    public async Task GotoAsync() => await Page.GotoAsync("index.html");

    public async Task OpenCatalogAsync() => await ViewCatalogLink.ClickAsync();
}
