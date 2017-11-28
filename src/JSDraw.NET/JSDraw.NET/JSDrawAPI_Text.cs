using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Brushes;
using SixLabors.Primitives;
using System.Linq;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
namespace JSDraw.NET
{
    public partial class JSDrawAPI
    {
        public void DrawText(int imgID, string text, int fontID, DrawWith d, PointF location,int matrixID)
        {
            location = transformByMatrix(matrixID, location);
            withImage(imgID, ctx =>
            {
                switch (d.Type)
                {
                    case DrawWithTypeEnum.Brush:
                        ctx.DrawText(text, GetFont(fontID), getBrush(d), location);
                        break;
                    case DrawWithTypeEnum.Pen:
                        ctx.DrawText(text, GetFont(fontID), getPen(d), location);
                        break;
                    default:
                        break;
                }

            });
        }

        public SizeF MeasureText(int fontId,string text)
        {
            //TODO: Performance impact, should not create new RenderOptions everytime
            return TextMeasurer.Measure(text, new RendererOptions(GetFont(fontId)));
        }
    }
}
