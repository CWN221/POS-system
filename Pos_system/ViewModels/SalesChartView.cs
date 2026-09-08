using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Pos_system.Services;
using SkiaSharp;

namespace Pos_system.ViewModels
{
    public class SalesChartView
    {
        private readonly ReportsService _reportsService = new();

        public async Task<ISeries[]> LoadMonthlySalesSeriesAsync(int months = 6)
        {
            var data = await _reportsService.GetMonthlySalesTrendAsync(months);

            if (data.Count == 0)
            {
                return
                [
                    new ColumnSeries<decimal>
                    {
                        Values = [0m],
                        Name = "Sales",
                        Fill = new SolidColorPaint(new SKColor(37, 99, 235))
                    }
                ];
            }

            return
            [
                new ColumnSeries<decimal>
                {
                    Values = data.Select(d => d.Total).ToArray(),
                    Name = "Sales",
                    Fill = new SolidColorPaint(new SKColor(37, 99, 235)),
                    Stroke = new SolidColorPaint(new SKColor(29, 78, 216))
                }
            ];
        }

        public async Task<string[]> LoadMonthLabelsAsync(int months = 6)
        {
            var data = await _reportsService.GetMonthlySalesTrendAsync(months);
            return data.Count == 0
                ? ["No data"]
                : data.Select(d => d.Month).ToArray();
        }
    }
}
