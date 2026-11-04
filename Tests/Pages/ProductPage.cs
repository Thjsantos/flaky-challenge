namespace Tests.Pages;

public class ProductPage : BasePage
{
    public const string Deku = "deku-my-hero-academy";

    public ProductPage(IPage page) : base(page) { }

    public ILocator Heading => Page.Locator("h1");
    public ILocator Price => Page.Locator("#price");
    public ILocator ScaleButtons => Page.Locator(".scales button");
    public ILocator QuoteButton => Page.Locator("#quote");

    public async Task GotoAsync(string productId) => await Page.GotoAsync($"produto.html?id={productId}");

    public async Task SelectScaleAsync(int index) => await ScaleButtons.Nth(index).ClickAsync();

    /// <summary>Clicks "Pedir orçamento" and returns the new tab it opens.</summary>
    public Task<IPage> ClickQuoteAsync() => Page.RunAndWaitForPopupAsync(async () => await QuoteButton.ClickAsync());
}
