function main() {
    let img = JSImage.Create(640, 480, false);
    let background = new JSColor("#ffffff");
    let foreground = new JSColor("#000000");
    let brush_background = JSBrush.createSolid(background);
    let brush_draw = JSBrush.createSolid(foreground);
    img.Fill(brush_background);
    let texture = JSImage.Load("arrow.jpg");
    img.DrawImage(texture);
    let points = [
        { x: 0, y: 0 },
        { x: 300, y: 100 }
    ];
    img.DrawLines({ brush: brush_draw, thickness: 1 }, points);
    JSFont.Install("Boogaloo-Regular.ttf");
    let f = new JSFont("Boogaloo", 24);
    img.DrawText("it works", f, brush_draw, { x: 100, y: 150 });
    img.DrawEclipse({ brush: brush_draw, thickness: 1 }, { x: 150, y: 200 }, 100);
    img.SetOutput();
}
//# sourceMappingURL=Test2.js.map