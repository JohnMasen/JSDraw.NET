using System;
using System.Collections.Generic;
using System.Text;
using ChakraCore.NET;
using SixLabors.Primitives;

namespace JSDraw.NET
{
    public class JSDraw
    {
        ChakraRuntime runtime;
        ChakraContext context;
        ImageManager manager;
        public string WorkPath { get; set; } = null;
        public void Load(string script)
        {
            manager = new ImageManager();
            manager.ImagePath = WorkPath;
            initJSRuntime();
            injectJSConverter();
            initJSImageManager();
            context.RunScript(script);
            
        }
        public IEnumerable<ImageManager.ImageBuffer> Output
        {
            get
            {
                return manager.GetOutput();
            }
        }

        public void Run()
        {
            context.GlobalObject.CallMethod("main");
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
            context.RunScript(Properties.Resources.init);
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

            converter.RegisterProxyConverter<ImageManager>(
                (jsvalue, obj, node) =>
                {
                    jsvalue.SetFunction<int, int, bool, int>("createImage", obj.CreateImage);
                    jsvalue.SetMethod<string>("setBrushColor", obj.SetBrushColor);
                    jsvalue.SetMethod<float>("setBrushWidth", obj.SetBrushWidth);
                    jsvalue.SetMethod<int>("fill", obj.Fill);
                    jsvalue.SetMethod<int, IEnumerable<PointF>>("drawLines", obj.DrawLines);
                    jsvalue.SetFunction<int, int>("getImageWidth", obj.GetImageWidth);
                    jsvalue.SetFunction<int, int>("getImageHeight", obj.GetImageHeight);
                    jsvalue.SetFunction<string, int>("load", obj.LoadImage);
                    jsvalue.SetMethod<int>("blackWhite", obj.BlackWhite);
                    jsvalue.SetMethod<int, int, int>("resize", obj.Resize);
                    jsvalue.SetMethod<int, int, Size, Point,float>("drawImage", obj.DrawImage);
                    jsvalue.SetMethod<int, string>("setOutput", obj.SetOutput);
                    jsvalue.SetFunction<string, int>("load", obj.LoadImage);
                    jsvalue.SetMethod<string,int>("setFont", obj.SetFont);
                    jsvalue.SetMethod<int, string, PointF>("drawText", obj.DrawText);
                });
        }
        private void initJSImageManager()
        {
            context.GlobalObject.WriteProperty<ImageManager>("_manager", manager);
        }
    }
}
