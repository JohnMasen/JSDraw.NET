declare const _api: any;
abstract class idObject {
    readonly id: number;
    constructor(id: number) {
        this.id = id;
    }
};

abstract class DrawWith extends idObject {
    protected _thickness: number;
    protected _type: DrawWithType;
    constructor(id: number, thickness: number,  type: DrawWithType) {
        super(id);
        this._thickness = thickness;
        this._type = type;
    }
}

abstract class BrushBase extends DrawWith {
    get Thickness(): number { return this._thickness }
    set Thickness(value: number) {this._thickness=value}
    constructor(id: number, thickness: number = 1) {
        super(id, thickness,  DrawWithType.brush);
    }
}

enum DrawWithType {
    brush = 0,
    pen = 1
}



enum BlendMode {
    //
    // Summary:
    //     Default blending mode, also known as "Normal" or "Alpha Blending"
    Normal = 0,
    //
    // Summary:
    //     Blends the 2 values by multiplication.
    Multiply = 1,
    //
    // Summary:
    //     Blends the 2 values by addition.
    Add = 2,
    //
    // Summary:
    //     Blends the 2 values by subtraction.
    Substract = 3,
    //
    // Summary:
    //     Multiplies the complements of the backdrop and source values, then complements
    //     the result.
    Screen = 4,
    //
    // Summary:
    //     Selects the minimum of the backdrop and source values.
    Darken = 5,
    //
    // Summary:
    //     Selects the max of the backdrop and source values.
    Lighten = 6,
    //
    // Summary:
    //     Multiplies or screens the values, depending on the backdrop vector values.
    Overlay = 7,
    //
    // Summary:
    //     Multiplies or screens the colors, depending on the source value.
    HardLight = 8,
    //
    // Summary:
    //     returns the source colors
    Src = 9,
    //
    // Summary:
    //     returns the source over the destination
    Atop = 10,
    //
    // Summary:
    //     returns the detination over the source
    Over = 11,
    //
    // Summary:
    //     the source where the desitnation and source overlap
    In = 12,
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    Out = 13,
    //
    // Summary:
    //     the destination where the source does not overlap it
    Dest = 14,
    //
    // Summary:
    //     the source where they dont overlap othersie dest in overlapping parts
    DestAtop = 15,
    //
    // Summary:
    //     the destnation over the source
    DestOver = 16,
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    DestIn = 17,
    //
    // Summary:
    //     the source where the desitnation and source overlap
    DestOut = 18,
    //
    // Summary:
    //     the clear.
    Clear = 19,
    //
    // Summary:
    //     clear where they overlap
    Xor = 20
}

interface Point {
    x: number;
    y: number;
}
interface Size {
    width: number;
    height: number;
}
interface Rectangle {
    x: number;
    y: number;
    width: number;
    height: number;
}


class JSColor {
    readonly hexString: string;
    constructor(hex: string) {
        this.hexString = hex;
    }

}

class JSBrush extends idObject {
    public static createSolid(color: JSColor): JSSolidBrush {
        return new JSSolidBrush(color);
    }
}
class JSSolidBrush extends BrushBase {
    public readonly color: JSColor;
    constructor(color: JSColor) {
        super(_api.createSolidBrush(color.hexString));
        this.color = color;
    }
}
class JSFont extends idObject {
    public readonly family: string;
    public readonly size: number;
    public static Install(path: string) {
        _api.installFont(path);
    }
    constructor(family: string, size: number) {
        super(_api.getFont(family, size));
        this.family = family;
        this.size = size;
    }
    MeasureText(text: string): Size {
        return _api.measureText(this.id, text);
    }
}

class JSImage extends idObject {
    public static Load(path: string, isPersistent: boolean = false): JSImage {
        return new JSImage(_api.loadImage(path, isPersistent));
    }
    public static Create(width: number, height: number, isPersistent: boolean = false): JSImage {
        return new JSImage(_api.createImage(width, height, isPersistent));
    }

    public readonly size: Size;

    private constructor(id: number) {
        super(id);
        this.size = _api.getImageSize(id);
    }

   
    public DrawLines(drawWith: DrawWith, points: Point[]) {
        _api.drawLines(this.id, drawWith, points);
    }

    public Fill(brush: BrushBase) {
        _api.fill(this.id, brush.id);
    }

    public SetOutput(name: string = "") {
        _api.setOutput(this.id, name);
    }

    public DrawImage(texture: JSImage, blend: BlendMode = BlendMode.Normal, percent: number = 1, size?: Size, location: Point = { x: 0, y: 0 }) {
        let b: number = blend;
        size = (size) || texture.size;
        _api.drawImage(this.id, texture.id, b, percent, size, location);
    }

    public DrawText(text: string, font: JSFont, drawWith:DrawWith, location: Point) {
        _api.drawText(this.id, text, font.id, drawWith, location);
    }
    public DrawEclipse(drawWith: DrawWith, location: Point, radius: number) {
        _api.drawEclipse(this.id, drawWith, location, radius);
    }
}
