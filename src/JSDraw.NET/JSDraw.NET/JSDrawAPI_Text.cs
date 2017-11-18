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
        public SizeF MeasureText(int fontId,string text)
        {
            //TODO: Performance impact, should not create new RenderOptions everytime
            return TextMeasurer.Measure(text, new RendererOptions(GetFont(fontId)));
        }
    }
}
