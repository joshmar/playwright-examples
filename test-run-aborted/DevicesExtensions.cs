using Microsoft.Playwright;

namespace test_run_aborted;

public static partial class DevicesExtensions
{
    private static ViewportSize ToViewport(this Devices device)
    {
        return device switch
        {
            // Phone resolutions
            Devices.IphoneSE => new ViewportSize { Width = 375, Height = 667 },
            Devices.IphoneXR => new ViewportSize { Width = 414, Height = 896 },
            Devices.Iphone12Pro => new ViewportSize { Width = 390, Height = 844 },
            Devices.SamsungGalaxyS8Plus => new ViewportSize { Width = 360, Height = 740 },
            Devices.SamsungGalaxyS20Ultra => new ViewportSize { Width = 412, Height = 915 },
            Devices.SamsungGalaxyA51A71 => new ViewportSize { Width = 412, Height = 914 },
            Devices.GalaxyFold => new ViewportSize { Width = 280, Height = 653 },
            Devices.Pixel5 => new ViewportSize { Width = 393, Height = 851 },
            Devices.SurvaceDuo => new ViewportSize { Width = 540, Height = 720 },
            Devices.NestHub => new ViewportSize { Width = 1024, Height = 600 },
            Devices.IpadAir => new ViewportSize { Width = 820, Height = 1180 },
            Devices.IpadMini => new ViewportSize { Width = 768, Height = 1024 },
            Devices.NestHubMax => new ViewportSize { Width = 1280, Height = 800 },
            Devices.SurvacePro7 => new ViewportSize { Width = 912, Height = 1368 },

            // PC resolutions
            Devices.PcSXGA => new ViewportSize { Width = 1280, Height = 1024 },
            Devices.PcHD => new ViewportSize { Width = 1366, Height = 768 },
            Devices.PcHDPlus => new ViewportSize { Width = 1600, Height = 900 },
            Devices.PcFHD => new ViewportSize { Width = 1920, Height = 1080 },
            Devices.PcWUXGA => new ViewportSize { Width = 1920, Height = 1200 },
            Devices.PcQHD => new ViewportSize { Width = 2560, Height = 1440 },
            Devices.PcWQHD => new ViewportSize { Width = 3440, Height = 1440 },
            Devices.PcUHD => new ViewportSize { Width = 3840, Height = 2160 },

            _ => throw new NotImplementedException()
        };
    }

    private static float ToDeviceScaleFactor(this Devices device)
    {
        return device switch
        {
            // Phones scales
            Devices.IphoneSE => 2.0f,
            Devices.IphoneXR => 2.0f,
            Devices.Iphone12Pro => 3.0f,
            Devices.SamsungGalaxyS8Plus => 4.0f,
            Devices.SamsungGalaxyS20Ultra => 3.5f,
            Devices.SamsungGalaxyA51A71 => 2.6f,
            Devices.GalaxyFold => 3.0f,
            Devices.Pixel5 => 2.8f,
            Devices.SurvaceDuo => 2.5f,
            Devices.NestHub => 2.0f,
            Devices.IpadAir => 2.0f,
            Devices.IpadMini => 2.0f,
            Devices.NestHubMax => 2.0f,
            Devices.SurvacePro7 => 2.0f,

            // Pc resolutions
            Devices.PcSXGA => 1.0f,
            Devices.PcHD => 1.0f,
            Devices.PcHDPlus => 1.0f,
            Devices.PcFHD => 1.0f,
            Devices.PcWUXGA => 1.0f,
            Devices.PcQHD => 1.0f,
            Devices.PcWQHD => 1.0f,
            Devices.PcUHD => 1.0f,

            _ => throw new NotImplementedException()
        };
    }

    private static bool IsPc(this Devices device)
    {
        return device.ToDeviceScaleFactor() == 1.0
            || device.ToDeviceScaleFactor() != 1.0
                && (device.ToViewport().Width < 768 || device.ToViewport().Height < 768);
    }

    public static DeviceModel ToDeviceModel(this Devices device)
    {
        return new DeviceModel
        {
            Device = device,
            ViewportSize = device.ToViewport(),
            DeviceScaleFactor = device.ToDeviceScaleFactor(),
            IsPc = device.IsPc()
        };
    }
}