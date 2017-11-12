using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace JSDraw.NET
{
    public partial class ImageManager
    {
        public void BlackWhite(int id)
        {
            getImageBuffer(id).image.Mutate(ctx => ctx.BlackWhite());
        }

        public void Resize(int id,int width,int height)
        {
            getImageBuffer(id).image.Mutate(ctx => ctx.Resize(width, height));
        }
    }
}
