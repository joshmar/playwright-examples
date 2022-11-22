using Microsoft.Playwright;

namespace test_run_aborted;

public record DeviceModel
{
    public required Devices Device { get; set; }
    public required ViewportSize ViewportSize { get; set; }
    public required float DeviceScaleFactor { get; set; }
    public required bool IsPc { get; set; }
}