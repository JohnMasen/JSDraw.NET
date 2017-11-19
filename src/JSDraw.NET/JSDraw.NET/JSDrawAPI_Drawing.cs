using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Brushes;
using SixLabors.Primitives;
using System.Linq;
using SixLabors.ImageSharp.Drawing.Pens;

namespace JSDraw.NET
{
    public partial class JSDrawAPI
    {
        private IBrush<Rgba32> getBrush(int brushId)
        {
            return manager.GetItem<IBrush<Rgba32>>(brushId);
        }
        private IBrush<Rgba32> getBrush(DrawWith d)
        {
            if (d.Type==DrawWithTypeEnum.Brush)
            {
                return getBrush(d.Id);
            }
            else
            {
                throw new ArgumentException("getBrush requires a brush type DrawWith parameter",nameof(d));
            }
        }

        private IPen<Rgba32> getPen(int penId)
        {
            return manager.GetItem<IPen<Rgba32>>(penId);
        }
        private IPen<Rgba32> getPen(DrawWith d)
        {
            if (d.Type == DrawWithTypeEnum.Pen)
            {
                return getPen(d.Id);
            }
            else
            {
                throw new ArgumentException("getBrush requires a pen type DrawWith parameter", nameof(d));
            }
        }

        public int CreateSolidBrush(string color)
        {
            var result = Brushes.Solid<Rgba32>(Rgba32.FromHex(color));
            return add<IBrush<Rgba32>>(result);
        }

        public void DrawLines(int imgId,DrawWith d, IEnumerable<PointF> points)
        {
            withImage(imgId,
                    ctx =>
                    {
                        switch (d.Type)
                        {
                            case DrawWithTypeEnum.Brush:
                                ctx.DrawLines(getBrush(d), d.Thickness, points.ToArray());
                                break;
                            case DrawWithTypeEnum.Pen:
                                ctx.DrawLines(getPen(d), points.ToArray());
                                break;
                            default:
                                break;
                        }
                    }
                    );
        }
        public void Fill(int imgId, int brushID)
        {
            withImage(imgId, ctx =>
            {
                ctx.Fill(getBrush(brushID));
             }
            );
        }
        public void DrawText(int imgID, string text, int fontID, DrawWith d, PointF location)
        {
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
        public void DrawEclipse(int imgID, DrawWith d, PointF position,float radius)
        {
            withImage(imgID, ctx => 
            {
                switch (d.Type)
                {
                    case DrawWithTypeEnum.Brush:
                        ctx.Draw(getBrush(d), d.Thickness, new SixLabors.Shapes.EllipsePolygon(position, radius));
                        break;
                    case DrawWithTypeEnum.Pen:
                        ctx.Draw(getPen(d), new SixLabors.Shapes.EllipsePolygon(position, radius));
                        break;
                    default:
                        break;
                }
                
            });
        }
    }
}
