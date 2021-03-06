﻿function main() {
    let img: JSImage = JSImage.Create(640, 480, false);
    let background: JSColor = new JSColor("#000000");
    let foreground: JSColor = new JSColor("#ffffff");
    let brush_background = JSBrush.createSolid(background);
    let brush_draw = JSBrush.createSolid(foreground);
    brush_draw.Thickness = 4;
    img.Fill(brush_background);

    //let texture = JSImage.Load("arrow.jpg");
    //img.DrawImage(texture);

    //let points: Point[] = [
    //    { x: 0, y: 0 },
    //    { x: 300, y: 100 }
    //];
    //img.DrawLines(brush_draw, points);

    //JSFont.Install("Boogaloo-Regular.ttf");
    //let f = new JSFont("Boogaloo", 24);
    //img.DrawText("it works", f, brush_draw, { x: 100, y: 150 });
    //img.DrawEclipse(brush_draw, { x: 150, y: 200 }, { width: 100, height: 100 });
    test1(img, brush_draw);
    test2(img, brush_draw);
    test3(img, brush_draw);
    img.SetOutput();

}
function test3(img: JSImage, b: JSBrush) {
    setAA(false);
    img.FillEclipse(b, { x: 300, y: 300 }, { width: 100, height: 50 });
    img.FillPolygon(b, [{ x: 0, y: 0 }, { x: 100, y: 100 }, { x: 0, y: 100 }]);
    setAA(true);
}

function test1(img: JSImage, d: DrawWith) {
    img.matrix.push();
    img.matrix.translate({ x: 100, y: 100 });

    img.matrix.push();
    img.DrawLines(d, [{ x: 0, y: 0 }, { x: 0, y: 50 }]);
    img.matrix.rotate(45);
    img.DrawLines(d, [{ x: 0, y: 0 }, { x: 0, y: 50 }]);
    img.matrix.translate({ x: 100, y: 100 });
    img.DrawLines(d, [{ x: 0, y: 0 }, { x: 0, y: 50 }]);
    img.matrix.pop();

    img.matrix.pop();

}
function test2(img: JSImage, d: DrawWith) {
    nestDraw(img, () => {
        img.matrix.rotate(10);
        img.matrix.scale({ x: 1.1, y: 1.1 });
        img.matrix.translate({ x: 40, y: 40 });
        img.DrawPolygon(d, [
            { x: -10, y: -10 },
            { x: 10, y: -10 },
            { x: 10, y: 10 },
            { x: -10, y: 10 }
        ])
    }, 10);
}

function drawClock(img: JSImage, d: DrawWith) {
    img.matrix.push();
    //img.DrawEclipse(d, { x: 0, y: 0 }, { width: 10, height: 10 });
    img.matrix.translate({ x: img.size.width / 2, y: img.size.height / 2 });
    //img.DrawEclipse(d, { x: 0, y: 0 }, { width:10, height:10 });
    for (var i = 0; i < 12; i++) {
        img.matrix.push();
        img.matrix.translate({ x: 0, y: 100 });
        img.matrix.rotate(i * 36);

        img.matrix.scale({ x: 2, y: 2 });
        img.DrawLines(d, [{ x: 0, y: 0 }, { x: 0, y: - 20 }]);
        img.matrix.pop();
    }
    img.matrix.pop();
}

function nestDraw(img: JSImage, action: Function, count: number) {
    if (count == 0) {
        return;
    }
    img.matrix.push();
    action();
    nestDraw(img, action, count - 1);
    img.matrix.pop();
    

}