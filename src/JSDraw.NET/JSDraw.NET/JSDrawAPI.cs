using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Brushes;
using SixLabors.Primitives;
using System.Linq;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using SixLabors.Fonts;

namespace JSDraw.NET
{
    public partial class JSDrawAPI
    {
        public enum DrawWithTypeEnum:int
        {
            Brush=0,
            Pen=1
        }
        public struct DrawWith
        {
            public int Id;
            public float Thickness;
            public DrawWithTypeEnum Type;
            public DrawWith(int id,float thickness,int type)
            {
                this.Id = id;
                this.Thickness = thickness;
                this.Type = (DrawWithTypeEnum)type;
            }
            
        }
        public event EventHandler<OnLoadEventArgs<Stream>> OnLoadResource;
        public class ImgInfo
        {
            public bool IsOutput { get; set; }
            public string Name { get; set; }
        }
        public class OnLoadEventArgs<T>:EventArgs
        {
            public string Path { get; private set; }
            public T Item { get; set; }
            public OnLoadEventArgs(string path)
            {
                Path = path;
            }
        }

        
        private Stream loadResource(string path)
        {
            if (OnLoadResource!=null)
            {
                OnLoadEventArgs<Stream> e = new OnLoadEventArgs<Stream>(path);
                OnLoadResource(this, e);
                return e.Item;
            }
            else
            {
                return System.IO.File.Open(path, FileMode.Open);
            }
        }

        
        private ObjectManager<ImgInfo> manager = new ObjectManager<ImgInfo>();
        private int add<T>(T obj,bool isPersistent=false,ImgInfo info=null)
        {
            return manager.Add<T>(obj, isPersistent,info).ID;
        }
        private void withImage(int imgId,Action<IImageProcessingContext<Rgba32>>a)
        {
            manager.Get<Image<Rgba32>>(imgId).Item.Mutate(a);
        }
        private Image<Rgba32> getImage(int id)
        {
            return manager.GetItem<Image<Rgba32>>(id);
        }
        public int CreateImage(int width,int height,bool isPersistent)
        {
            Image<Rgba32> img = new Image<Rgba32>(width, height);
            return add(img, isPersistent,new ImgInfo());
        }

        public int LoadImage(string path, bool isPersistent)
        {
            OnLoadEventArgs<Image<Rgba32>> args = new OnLoadEventArgs<Image<Rgba32>>(path);
            Image<Rgba32> img;
            img = Image.Load<Rgba32>(loadResource(path));
            return add(img, isPersistent,new ImgInfo());
        }

        public void SetOutput(int imgId,string name)
        {
            var item=manager.Get<Image<Rgba32>>(imgId);
            item.Tag.Name = name;
            item.Tag.IsOutput = true;
        }

        public IEnumerable<ManagedItem<Image<Rgba32>, ImgInfo>> GetOutput()
        {
            return from item in manager.Items
                   where item.Value.Tag?.IsOutput == true
                   select item.Value as ManagedItem<Image<Rgba32>, ImgInfo>;
        }

        public void DrawImage(int imgId,int textureImageId,int blendMode,float percent,Size size, Point location)
        {
            withImage(imgId, ctx => ctx.DrawImage(getImage(textureImageId), (PixelBlenderMode)blendMode, percent, size, location));
        }

        public Size GetImageSize(int id)
        {
            var img = getImage(id);
            return new Size(img.Width, img.Height);
        }
    }
}
