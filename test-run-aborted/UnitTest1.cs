using Microsoft.Playwright;

namespace test_run_aborted;

[Parallelizable]
//[TestFixtureSource(nameof(GetDevices))]
[TestFixture(Devices.PcSXGA)]
[TestFixture(Devices.PcUHD)]
[TestFixture(Devices.PcHD)]
[TestFixture(Devices.PcWQHD)]
[TestFixture(Devices.PcQHD)]
[TestFixture(Devices.PcFHD)]
[TestFixture(Devices.PcHDPlus)]
[TestFixture(Devices.PcWUXGA)]
public class Tests : BasePageTest
{
    public Tests(Devices device) 
        : base(device.ToDeviceModel())
    { }

    [Test]
    public async Task Testing_Event_handler()
    {
        await Page.GotoAsync("https://playwright.dev/");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Docs" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Debugging Tests" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Exploring selectors" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Headed mode" }).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "Headed mode#​" }).ClickAsync();

        await Page.GotoAsync("https://www.google.nl/");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Accept all" }).ClickAsync();

        await Page.GetByRole(AriaRole.Combobox, new() { NameString = "Search" }).ClickAsync();

        await Page.GetByRole(AriaRole.Combobox, new() { NameString = "Search" }).FillAsync("testing this ");

        await Page.GetByRole(AriaRole.Combobox, new() { NameString = "Search" }).PressAsync("Enter");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Software testing - Wikipedia https://en.wikipedia.org › wiki › Software_testing" }).ClickAsync();

        await Page.Locator("#firstHeading").GetByText("Software testing").ClickAsync();
    }

    private static int _count = 0;

    [Test]
    public async Task Test1()
    {
        await Page.GotoAsync("https://playwright.dev/");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Docs" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Debugging Tests" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Exploring selectors" }).ClickAsync();

        if (_count == 4)
        {
            throw new Exception("this one should continue with the other test fixtures");
        }
        _count++;

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Headed mode" }).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "Headed mode#​" }).ClickAsync();

        await Page.GotoAsync("https://www.google.nl/");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Accept all" }).ClickAsync();

        await Page.GetByRole(AriaRole.Combobox, new() { NameString = "Search" }).ClickAsync();

        await Page.GetByRole(AriaRole.Combobox, new() { NameString = "Search" }).FillAsync("testing this ");

        await Page.GetByRole(AriaRole.Combobox, new() { NameString = "Search" }).PressAsync("Enter");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Software testing - Wikipedia https://en.wikipedia.org › wiki › Software_testing" }).ClickAsync();

        await Page.Locator("#firstHeading").GetByText("Software testing").ClickAsync();
    }
}