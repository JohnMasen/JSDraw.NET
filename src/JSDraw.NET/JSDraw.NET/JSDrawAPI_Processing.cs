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
        public void GaussianBlur(int imgID,int sigma,Rectangle rect)
        {
            withImage(imgID, ctx =>
             {
                 ctx.GaussianBlur(sigma, rect);
             }
            );
        }

        public void BlackWhite(int imgId,Rectangle rect)
        {
            withImage(imgId, ctx =>
             {
                 ctx.BlackWhite(rect);
             });
        }
    }
}
