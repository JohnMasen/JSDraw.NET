
function main()
{
    var p = JSImage.create(640, 480);
    var p1 = JSImage.load("arrow.jpg");
    p.setBrushColor("#d9d9d9");
    p.fill();
    
    p.drawImage(p1);

    var points = [
        { "x": 0, "y": 0 },
        { "x": 100, "y": 400 }
    ];
    p.setBrushColor("#000000");
    p.drawLines(points);
    
    p.setFont("Boogaloo-Regular.ttf", 36);
    var position = { "x": 100, "y": 200 };
    p.drawText("It Works!!", position);

    p.setOutput();
}