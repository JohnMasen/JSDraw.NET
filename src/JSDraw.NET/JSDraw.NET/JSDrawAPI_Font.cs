using System;
using System.Collections.Generic;
using System.Text;
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
        FontCollection fc = new FontCollection();
        public event EventHandler<OnLoadEventArgs<System.IO.Stream>> OnLoadFont;
        public void InstallFont(string path)
        {
            if (OnLoadFont!=null)
            {
                var e = new OnLoadEventArgs<System.IO.Stream>(path);
                OnLoadFont(this,e);
                e.Item.Position = 0;
                fc.Install(e.Item);
            }
            else
            {
                fc.Install(path);
            }
        }

        public int GetFont(string family,float size)
        {
            return add<Font>(fc.CreateFont(family, size));
        }
    }
}
