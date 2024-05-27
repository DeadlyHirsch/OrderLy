using LiveChartsCore;
using LiveChartsCore.Drawing;
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
                    Values = GenerateRandomValues(),
                    Fill = new SolidColorPaint(new SKColor(13,115,119)) {StrokeThickness = 5},
                    Stroke = new SolidColorPaint(new SKColor(20,255,235)),
                    GeometryStroke = new SolidColorPaint(new SKColor(20,255,235)),
                    LineSmoothness = 0.5,
                    EasingFunction = EasingFunctions.EaseIn,
                    GeometrySize = 3
                }
            };

        public Axis[] XAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                Name = "Calendar Week",
                NamePaint = new SolidColorPaint(SKColors.White),
                NameTextSize = 10,
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
                    NameTextSize = 10,

                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 2,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };
        public DrawMarginFrame DrawMarginFrame => new DrawMarginFrame
        {
            Fill = new SolidColorPaint(new SKColor(50,50,50))
        };

        private static double[] GenerateRandomValues()
        {
            double[] values = new double[52];
            Random rand = new Random();

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0.0;
            }
            values[21] = 5.0;
            return values;
        }
    }
}
