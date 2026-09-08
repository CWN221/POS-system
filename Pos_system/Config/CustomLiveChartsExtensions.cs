using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;

namespace Pos_system
{
    public static class CustomLiveChartsExtensions
    {
        public static LiveChartsSettings AddLiveChartsAppSettings(this LiveChartsSettings settings)
        {
            return settings
                .AddSkiaSharp()
                .AddDefaultMappers()
                .AddDefaultTheme()
                .UseDefaults();
        }

        public static LiveChartsSettings AddLiveChartsRenderSettings(this LiveChartsSettings settings)
        {
            return settings
                .HasRenderingSettings(settings =>
                {
                    // settings.UseGPU = false; // Uncomment if needed
                    // settings.TryUseVSync = true; // Uncomment if needed
                    // settings.LiveChartsRenderLoopFPS = 60; // Uncomment if needed
                    // settings.ShowFPS = false; // Uncomment if needed
                })
                .HasTextSettings(new TextSettings
                {
                    DefaultTypeface = SKTypeface.Default,
                    FontBuilder = (paint, typeface, size) =>
                    {
                        paint.TextSize = size;
                        paint.Typeface = typeface;
                        paint.IsAntialias = true;
                        paint.LcdRenderText = true;

                        return new SKFont(typeface, size)
                        {
                            Edging = SKFontEdging.SubpixelAntialias,
                            Hinting = SKFontHinting.Normal
                        };
                    }
                });
        }
    }
}