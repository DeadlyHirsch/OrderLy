using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace OrderLy_WPF_Client
{
    public class ViewModel
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] {2,4,1,2,3,2,4,1,2,3,4,5,1,2,4,1,2,3,2,4,1,2,3,4,5,1,2,4,1,2,3,2,4,1,2,3,4,5,1,2,4,1,2,3,2,4,1,2,3,4,5,1},
                    Fill = null,
                    LineSmoothness = 0.8
                }
            };

        public Axis[] XAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                Name = "Calendar Week",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.White),
                TextSize = 10,
                SeparatorsPaint = new SolidColorPaint(SKColors.White)
            }

        };

        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "# of Orders",
                    NamePaint = new SolidColorPaint(SKColors.White),

                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 2,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };
    }
}
