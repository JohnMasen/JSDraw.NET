function main() {
    let img: JSImage = JSImage.Create(640, 480, false);
    let background: JSColor = new JSColor("#ffffff");
    let foreground: JSColor = new JSColor("#000000");
    let brush_background = JSBrush.createSolid(background);
    let brush_draw = JSBrush.createSolid(foreground);
    img.Fill(brush_background);

    let texture = JSImage.Load("arrow.jpg");
    img.DrawImage(texture);

    let points: Point[] = [
        { x: 0, y: 0 },
        { x: 300, y: 100 }
    ];
    img.DrawLines({ brush: brush_draw, thickness: 1 }, points);
    img.SetOutput();
    
 }