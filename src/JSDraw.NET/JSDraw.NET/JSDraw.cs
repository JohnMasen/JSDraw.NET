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
            api.OnLoadResource += Api_OnLoadResource;
        }

        private void Api_OnLoadResource(object sender, OnLoadEventArgs<System.IO.Stream> e)
        {
            string path = e.Path;
            if (!string.IsNullOrWhiteSpace(WorkPath))
            {
                path = System.IO.Path.Combine(WorkPath, e.Path);
            }
            e.Item= System.IO.File.OpenRead(path);
        }

        public void Load(string script)
        {
            api.Clear(false);
            //manager.ImagePath = WorkPath;
            initJSRuntime();
            injectJSConverter();
            initAPI();
            context.RunScript(script);
        }
        public void ClearObjectList(bool keepPersistent=true)
        {
            api.Clear(keepPersistent);
        }
        //public IEnumerable<ImageManager.ImageBuffer> Output
        //{
        //    get
        //    {
        //        return manager.GetOutput();
        //    }
        //}

        public void Run(string entryPoint="main")
        {
            context.GlobalObject.CallMethod(entryPoint);
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
            converter.RegisterStructConverter<DrawWith>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty<int>("id", value.Id);
                    jsvalue.WriteProperty<float>("_thickness", value.Thickness);
                    jsvalue.WriteProperty<int>("_type", (int)value.Type);
                },
                (jsvalue) =>
                {
                    int id = jsvalue.ReadProperty<int>("id");
                    float thickness = jsvalue.ReadProperty<float>("_thickness");
                    int type = jsvalue.ReadProperty<int>("_type");
                    return new DrawWith(id, thickness, type);
                });

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
            converter.RegisterStructConverter<SizeF>(
                (jsvalue, value) =>
                {
                    jsvalue.WriteProperty("width", value.Width);
                    jsvalue.WriteProperty("height", value.Height);
                },
                (jsvalue) =>
                {
                    float w = jsvalue.ReadProperty<float>("width");
                    float h = jsvalue.ReadProperty<float>("height");
                    return new SizeF(w, h);
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
                    binding.SetMethod<int,int>("fill", obj.Fill);
                    binding.SetMethod<int, DrawWith, IEnumerable<PointF>>("drawLines", obj.DrawLines);
                    binding.SetMethod<int, string>("setOutput", obj.SetOutput);
                    binding.SetMethod<int, int, int, float, Size, Point>("drawImage", obj.DrawImage);
                    binding.SetFunction<int, Size>("getImageSize", obj.GetImageSize);
                    binding.SetMethod<string>("installFont", obj.InstallFont);
                    binding.SetFunction<string, float, int>("getFont", obj.GetFont);
                    binding.SetFunction<int,string, SizeF>("measureText", obj.MeasureText);
                    binding.SetMethod<int, string, int, DrawWith, PointF>("drawText", obj.DrawText);
                    binding.SetMethod<int, DrawWith,PointF, float>("drawEclipse", obj.DrawEclipse);
                });
            
        }
        private void initAPI()
        {
            context.GlobalObject.WriteProperty<JSDrawAPI>("_api", api);
        }
    }
}
