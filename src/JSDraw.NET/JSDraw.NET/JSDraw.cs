using System;
using System.Collections.Generic;
using System.Text;
using ChakraCore.NET;
using SixLabors.Primitives;
using SixLabors.ImageSharp;
using static JSDraw.NET.JSDrawAPI;

namespace JSDraw.NET
{
    public class JSDraw
    {
        ChakraRuntime runtime;
        ChakraContext context;
        public string WorkPath { get; set; } = null;
        JSDrawAPI api = new JSDrawAPI();
        public JSDraw()
        {
            api.OnLoadImage += Api_OnLoadImage;
        }

        private void Api_OnLoadImage(object sender, OnLoadEventArgs e)
        {
            string path = e.Path;
            if (!string.IsNullOrWhiteSpace(WorkPath))
            {
                path = System.IO.Path.Combine(WorkPath, e.Path);
            }
            e.Image=Image.Load<Rgba32>(path);
        }

        public void Load(string script)
        {

            //manager.ImagePath = WorkPath;
            initJSRuntime();
            injectJSConverter();
            initAPI();
            context.RunScript(script);
            
        }
        //public IEnumerable<ImageManager.ImageBuffer> Output
        //{
        //    get
        //    {
        //        return manager.GetOutput();
        //    }
        //}

        public void Run()
        {
            context.GlobalObject.CallMethod("main");
        }
        
        public IEnumerable<ManagedItem<Image<Rgba32>, ImgInfo>> GetOutput()
        {
            return api.GetOutput();
        }

        private void initJSRuntime()
        {
            runtime = ChakraRuntime.Create();
#if DEBUG
            context = runtime.CreateContext(true);
#else
            context = runtime.CreateContext(false);
#endif
            JSRequireLoader.EnableRequire(context, WorkPath);
            var s = Properties.Resources.boot;
            context.RunScript(Properties.Resources.boot);
        }

        private void injectJSConverter()
        {
            var converter = context.ServiceNode.GetService<IJSValueConverterService>();

            converter.RegisterStructConverter<PointF>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty("x", value.X);
                    jsvalue.WriteProperty("y", value.Y);
                },
                (jsvalue) =>
                {
                    float x = jsvalue.ReadProperty<float>("x");
                    float y = jsvalue.ReadProperty<float>("y");
                    return new PointF(x, y);
                });
            converter.RegisterArrayConverter<PointF>();

            converter.RegisterStructConverter<Point>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty("x", value.X);
                    jsvalue.WriteProperty("y", value.Y);
                },
                (jsvalue) =>
                {
                    int x = jsvalue.ReadProperty<int>("x");
                    int y = jsvalue.ReadProperty<int>("y");
                    return new Point(x, y);
                });

            converter.RegisterStructConverter<Size>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty("width", value.Width);
                    jsvalue.WriteProperty("height", value.Height);
                },
                (jsvalue) =>
                {
                    int w = jsvalue.ReadProperty<int>("width");
                    int h = jsvalue.ReadProperty<int>("height");
                    return new Size(w, h);
                });
            converter.RegisterStructConverter<Rectangle>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty<int>("x", value.X);
                    jsvalue.WriteProperty<int>("y", value.Y);
                    jsvalue.WriteProperty<int>("width", value.Width);
                    jsvalue.WriteProperty<int>("height", value.Height);
                },
                (jsvalue) =>
                {
                    return new Rectangle(
                        jsvalue.ReadProperty<int>("x"),
                        jsvalue.ReadProperty<int>("y"),
                        jsvalue.ReadProperty<int>("width"),
                        jsvalue.ReadProperty<int>("height")
                        );
                });

            converter.RegisterProxyConverter<JSDrawAPI>(
                (binding, obj, node) =>
                {
                    binding.SetFunction<int, int, bool, int>("createImage", obj.CreateImage);
                    binding.SetFunction<string, bool, int>("loadImage", obj.LoadImage);
                    binding.SetFunction<string, int>("createSolidBrush", obj.CreateSolidBrush);
                    binding.SetMethod<int,int>("brushFill", obj.BrushFill);
                    binding.SetMethod<int, int, int, IEnumerable<PointF>>("brushDrawLines", obj.BrushDrawLines);
                    binding.SetMethod<int, string>("setOutput", obj.SetOutput);
                    binding.SetMethod<int, int, int, float, Size, Point>("drawImage", obj.DrawImage);
                    binding.SetFunction<int, Size>("getImageSize", obj.GetImageSize);
                    binding.SetMethod<string>("installFont", obj.InstallFont);
                    binding.SetFunction<string, float, int>("getFont", obj.GetFont);
                });
            
        }
        private void initAPI()
        {
            context.GlobalObject.WriteProperty<JSDrawAPI>("_api", api);
        }
    }
}
