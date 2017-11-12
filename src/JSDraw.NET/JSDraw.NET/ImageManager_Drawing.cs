using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Brushes;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace JSDraw.NET
{
    public partial class ImageManager
    {
        public void SetBrushWidth(float size)
        {
            penWidth = size;
        }
        public void SetBrushColor(string colorHex)
        {
            currentBrush = Brushes.Solid<Rgba32>(Rgba32.FromHex(colorHex));
        }

        public void DrawLines(int id, IEnumerable<PointF> points)
        {
            getImageBuffer(id).image.Mutate(ipc => ipc.DrawLines(currentBrush, penWidth, points.ToArray()));
        }

        public void Fill(int id)
        {
            getImageBuffer(id).image.Mutate(ipc => ipc.Fill(currentBrush));
        }

        public void DrawImage(int id,int textureID,Size size ,Point point)
        {
            var target = getImageBuffer(textureID).image;
            getImageBuffer(id).image.Mutate(ctx => ctx.DrawImage(target, 1, size, point));
        }

        public void SetImageBrush(int id)
        {
            ImageBrush<Rgba32> brush = new ImageBrush<Rgba32>(getImageBuffer(id).image);
            currentBrush = brush;
        }
    }
}
