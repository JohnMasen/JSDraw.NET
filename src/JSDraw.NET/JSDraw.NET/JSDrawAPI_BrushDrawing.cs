using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Brushes;
using SixLabors.Primitives;
using System.Linq;

namespace JSDraw.NET
{
    public partial class JSDrawAPI
    {
        private IBrush<Rgba32> getBrush(int brushID)
        {
            return manager.GetItem<IBrush<Rgba32>>(brushID);
        }
        public int CreateSolidBrush(string color)
        {
            var result = Brushes.Solid<Rgba32>(Rgba32.FromHex(color));
            return add<IBrush<Rgba32>>(result);
        }

        public void BrushDrawLines(int imgId, int brushId, int thickness, IEnumerable<PointF> points)
        {
            withImage(imgId,
                ctx =>
                {
                    ctx.DrawLines(getBrush(brushId), thickness, points.ToArray());
                }
                );
        }
        public void BrushFill(int imgId, int brushID)
        {
            withImage(imgId, ctx =>
            {
                ctx.Fill(getBrush(brushID));
             }
            );
        }
        public void BrushDrawText(int imgID, string text, int fontID,int brushID,PointF location)
        {
            withImage(imgID, ctx => { ctx.DrawText(text, GetFont(fontID), getBrush(brushID), location); });
        }
    }
}
