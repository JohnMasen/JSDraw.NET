using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JSDraw.NET;
using SixLabors.Primitives;
using SixLabors.ImageSharp;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using System.Threading.Tasks;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ImageManager im;
        MemoryStream stream = new MemoryStream();
        Random r = new Random();
        WriteableBitmap s = new WriteableBitmap(640, 480);
        Rect rect = new Rect(0,0,640, 480);
        Windows.UI.Xaml.Controls.Image ic;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            im = new ImageManager();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //await testLowApi();
            JSDraw.NET.JSDraw obj = new JSDraw.NET.JSDraw();
            obj.Load(loadScript("test1.js"));
            obj.Run();
            await renderResult(obj.Output.First().image);
        }
        private async Task renderResult(SixLabors.ImageSharp.Image<Rgba32> img)
        {
            stream = new MemoryStream();
            img.SaveAsBmp(stream);
            stream.Position = 0;
            var bii = await BitmapFactory.FromStream(stream);
            imgResult.Source = bii;
        }

        private async System.Threading.Tasks.Task testLowApi()
        {
            var img = im.CreateImage(640, 480, false);
            im.SetBrushColor("#ffffff");
            im.Fill(img);
            im.SetBrushColor("#000000");
            var points = new PointF[] { new PointF(r.Next(0, 640), r.Next(0, 480)), new PointF(r.Next(0, 640), r.Next(0, 480)) };
            perfTest("drawLine", () => im.DrawLines(img, points));
            var p1 = im.LoadImage("arrow.jpg");
            perfTest("drawImg", () => im.DrawImage(img, p1, new SixLabors.Primitives.Size(100, 100), SixLabors.Primitives.Point.Empty));

            im.SetImageBrush(p1);
            im.SetBrushWidth(20);
            im.DrawLines(img, points);

            im.SetOutput(img, string.Empty);
            var output = im.GetOutput().First().image;
            stream = new MemoryStream();
            output.SaveAsBmp(stream);
            stream.Position = 0;
            var bii = await BitmapFactory.FromStream(stream);
            imgResult.Source = bii;

            im.Clear();
        }

        private void perfTest(string name,Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            Debug.WriteLine($"{name} = {sw.ElapsedMilliseconds} ms");
        }

        private string loadScript(string name)
        {
            string filepath = "Scripts\\" + name;
            return File.ReadAllText(filepath);

        }
    }
}
