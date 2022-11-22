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
        return Enum.GetValues<Devices>().Except( new[] { 
            Devices.Invalid,
            Devices.IphoneSE,
            Devices.IphoneXR,
            Devices.Iphone12Pro,
            Devices.SamsungGalaxyS8Plus,
            Devices.SamsungGalaxyS20Ultra,
            Devices.SamsungGalaxyA51A71,
            Devices.GalaxyFold,
            Devices.Pixel5,
            Devices.SurvaceDuo,
            Devices.NestHub,
            Devices.IpadAir,
            Devices.IpadMini,
            Devices.NestHubMax,
            Devices.SurvacePro7
        }).ToArray();
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
        
        //Page.RequestFinished += Testing_Exception;
    }

    /// <summary>
    /// Uncomment this part including the eventhandle to test the case in which the tests will fail
    /// </summary>
    /// <returns></returns>
    //private static int _eventCounter = 0;

    //private void Testing_Exception(object? sender, IRequest e)
    //{
    //    if (_eventCounter == 5)
    //    {
    //        throw new Exception("This throw will crash all other tets...");
    //    }

    //    _eventCounter++;
    //}

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