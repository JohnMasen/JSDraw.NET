
function main()
{
    var p = JSImage.create(640, 480);
    var points = [
        { "x": 0, "y": 0 },
        { "x": 100, "y": 100 }
    ];
    p.setBrushColor("#ffffff");
    p.drawLines(points);
    p.setOutput();
}