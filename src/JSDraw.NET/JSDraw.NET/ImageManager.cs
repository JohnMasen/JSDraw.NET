using System;
using SixLabors.ImageSharp;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SixLabors.Primitives;
using SixLabors.ImageSharp.Drawing.Brushes;
using SixLabors.ImageSharp.Drawing.Pens;
using SixLabors.Fonts;

namespace JSDraw.NET
{
    public partial class ImageManager
    {
        private int currentID;
        private IBrush<Rgba32> currentBrush;
        private float penWidth = 1;
        private Font currentFont;
        private FontCollection fc = new FontCollection();

        public class ImageBuffer
        {
            public string tag;
            public bool persistent;
            public bool isOutput;
            public Image<Rgba32> image;
            public ImageBuffer(Image<Rgba32> image, string tag = null, bool persistent = false, bool isOutput = false)
            {
                this.image = image;
                this.tag = tag;
                this.isOutput = isOutput;
                this.persistent = persistent;
            }
        }
        public string ImagePath { get; set; } = string.Empty;
        public ImageManager()
        {
            currentBrush = Brushes.Solid<Rgba32>(Rgba32.Black);
        }

        private Dictionary<int, ImageBuffer> items = new Dictionary<int, ImageBuffer>();
        private int createImage(Image<Rgba32> source, bool persistent = false)
        {
            ImageBuffer result = new ImageBuffer(source, null, persistent);
            items.Add(currentID, result);
            return currentID++;
        }

        public int CreateImage(int width, int height, bool persistent)
        {
            return createImage(new Image<Rgba32>(width, height), persistent);
        }

        public int LoadImage(string path)
        {
            if (!string.IsNullOrWhiteSpace(ImagePath))
            {
                path = System.IO.Path.Combine(ImagePath, path);
            }
            return createImage(Image.Load<Rgba32>(path));
        }

        public void SetOutput(int id, string tag)
        {
            getImageBuffer(id).isOutput = true;
        }

        public IEnumerable<ImageBuffer> GetOutput()
        {
            return (from item in items
                    where item.Value.isOutput
                    select item.Value);
        }

        public void Clear(bool removePersistent = false)
        {
            if (removePersistent)
            {
                items.Clear();
            }
            else
            {
                var keys = (from item in items
                            where !item.Value.persistent
                            select item.Key).ToList();
                foreach (var key in keys)
                {
                    items.Remove(key);
                }
            }
            if (items.Count > 0)
            {
                currentID = items.Keys.Max() + 1;
            }
            else
            {
                currentID = 0;
            }

        }

        private ImageBuffer getImageBuffer(int id)
        {
            return items[id];
        }



        public int GetImageWidth(int id)
        {
            return items[id].image.Width;
        }

        public int GetImageHeight(int id)
        {
            return items[id].image.Height;
        }

        public void SetFont(string name,int size)
        {
            string path = System.IO.Path.Combine(ImagePath, name);
            var family=fc.Install(path);
            currentFont = family.CreateFont(size);
        }

        

    }
}
