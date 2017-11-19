function main() {
    var img = JSImage.Create(640, 480, false);
    var background = new JSColor("#ffffff");
    var foreground = new JSColor("#000000");
    var brush_background = JSBrush.createSolid(background);
    var brush_draw = JSBrush.createSolid(foreground);
    img.Fill(brush_background);
    var texture = JSImage.Load("arrow.jpg");
    img.DrawImage(texture);
    var points = [
        { x: 0, y: 0 },
        { x: 300, y: 100 }
    ];
    img.DrawLines(brush_draw, points);
    JSFont.Install("Boogaloo-Regular.ttf");
    var f = new JSFont("Boogaloo", 24);
    img.DrawText("it works", f, brush_draw, { x: 100, y: 150 });
    img.DrawEclipse(brush_draw, { x: 150, y: 200 }, 100);
    img.SetOutput();
}
//# sourceMappingURL=Test2.js.map