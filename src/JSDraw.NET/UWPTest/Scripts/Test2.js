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
    img.DrawLines({ brush: brush_draw, thickness: 1 }, points);
    img.SetOutput();
}
//# sourceMappingURL=Test2.js.map