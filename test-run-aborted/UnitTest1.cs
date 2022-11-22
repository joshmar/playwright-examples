namespace test_run_aborted;

[Parallelizable]
[TestFixtureSource(nameof(GetDevices))]
public class Tests : BasePageTest
{
    public Tests(Devices device) 
        : base(device.ToDeviceModel())
    {
    }

    [Test]
    public void Test1()
    {
    }
}