namespace Tests.Pages;

/// <summary>Base for all page objects: holds the Playwright page. Page objects expose locators and actions; tests do the asserting.</summary>
public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page) => Page = page;

    public string Url => Page.Url;

    public Task<string> TitleAsync() => Page.TitleAsync();
}
