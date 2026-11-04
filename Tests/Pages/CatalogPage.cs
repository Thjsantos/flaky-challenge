namespace Tests.Pages;

public class CatalogPage : BasePage
{
    public CatalogPage(IPage page) : base(page) { }

    public ILocator Cards => Page.Locator("#grid article.card");
    public ILocator SearchBox => Page.Locator("#q");
    public ILocator EmptyMessage => Page.Locator("#empty");
    public ILocator CardLinks => Page.Locator("#grid article.card a.pic");

    /// <param name="category">Optional tab to open, e.g. "anime" or "games" (the URL hash).</param>
    public async Task GotoAsync(string? category = null)
        => await Page.GotoAsync(category is null ? "catalogo.html" : $"catalogo.html#{category}");

    public async Task SearchAsync(string text) => await SearchBox.FillAsync(text);

    public async Task OpenFirstProductAsync() => await CardLinks.First.ClickAsync();
}
