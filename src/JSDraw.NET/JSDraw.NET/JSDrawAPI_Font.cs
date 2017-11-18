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
        private Font GetFont(int id)
        {
            return manager.GetItem<Font>(id);
        }
        public void InstallFont(string path)
        {
            fc.Install(loadResource(path));
        }

        public int GetFont(string family,float size)
        {
            return add<Font>(fc.CreateFont(family, size));
        }
    }
}
