using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework.Interfaces;
using System.Diagnostics;

namespace test_run_aborted;

public abstract class BasePageTest : BrowserTest
{
    protected readonly DeviceModel _deviceModel;
    protected TestContext TestContext;

    protected static Devices[] GetDevices()
    {
        return Enum.GetValues<Devices>().Except( new[] { Devices.Invalid }).ToArray();
    }

    private BrowserNewContextOptions ContextOptions => new()
    {
        ViewportSize = _deviceModel.ViewportSize,
        DeviceScaleFactor = _deviceModel.DeviceScaleFactor,
        Locale = "en-US"
    };

    public IPage Page { get; private set; }
    public IBrowserContext Context { get; private set; }

    protected BasePageTest(DeviceModel deviceModel)
    {
        _deviceModel = deviceModel;
    }

    [OneTimeSetUp]
    public void StartTest()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [OneTimeTearDown]
    public void EndTest()
    {
        Trace.Flush();
    }

    [SetUp]
    public async Task BasePageSetup()
    {
        Context = await NewContext(ContextOptions).ConfigureAwait(continueOnCapturedContext: false);
        Page = await Context.NewPageAsync().ConfigureAwait(continueOnCapturedContext: false);

        TestContext = TestContext.CurrentContext;

        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        Page.SetDefaultTimeout(50000);
    }

    [TearDown]
    public async Task Recording()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
        {
            var environmentBase = Environment.CurrentDirectory;
            var fileName = $"{TestContext.Test.Name}-{_deviceModel.Device}-{DateTime.UtcNow:dd-MM-yy-HHmmss}";
            var tracePath = $@"{environmentBase}\Recordings\TraceViewers\{fileName}.zip";

            await Context.Tracing.StopAsync(new()
            {
                Path = tracePath
            });
        }
    }
}