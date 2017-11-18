declare const _api: any;
declare abstract class idObject {
    readonly id: number;
    constructor(id: number);
}
declare abstract class BrushBase extends idObject {
}
declare enum BlendMode {
    Normal = 0,
    Multiply = 1,
    Add = 2,
    Substract = 3,
    Screen = 4,
    Darken = 5,
    Lighten = 6,
    Overlay = 7,
    HardLight = 8,
    Src = 9,
    Atop = 10,
    Over = 11,
    In = 12,
    Out = 13,
    Dest = 14,
    DestAtop = 15,
    DestOver = 16,
    DestIn = 17,
    DestOut = 18,
    Clear = 19,
    Xor = 20,
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
declare class JSColor {
    readonly hexString: string;
    constructor(hex: string);
}
declare class JSBrush extends idObject {
    static createSolid(color: JSColor): JSSolidBrush;
}
declare class JSSolidBrush extends BrushBase {
    readonly color: JSColor;
    constructor(color: JSColor);
}
declare class JSFont extends idObject {
    readonly family: string;
    readonly size: number;
    static Install(path: string): void;
    constructor(family: string, size: number);
    MeasureText(text: string): Size;
}
declare class JSImage extends idObject {
    static Load(path: string, isPersistent?: boolean): JSImage;
    static Create(width: number, height: number, isPersistent?: boolean): JSImage;
    readonly size: Size;
    private constructor();
    DrawLines({brush: BrushBase, thickness: number}: {
        brush: any;
        thickness: any;
    }, points: Point[]): any;
    Fill(brush: BrushBase): void;
    SetOutput(name?: string): void;
    DrawImage(texture: JSImage, blend?: BlendMode, percent?: number, size?: Size, location?: Point): void;
    DrawText(text: string, font: JSFont, brush: BrushBase, location: Point): void;
}
