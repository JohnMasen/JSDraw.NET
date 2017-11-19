class idObject {
    constructor(id) {
        this.id = id;
    }
}
;
class DrawWith extends idObject {
    constructor(id, thickness, type) {
        super(id);
        this._thickness = thickness;
        this._type = type;
    }
}
class BrushBase extends DrawWith {
    get Thickness() { return this._thickness; }
    set Thickness(value) { this._thickness = value; }
    constructor(id, thickness = 1) {
        super(id, thickness, DrawWithType.brush);
    }
}
var DrawWithType;
(function (DrawWithType) {
    DrawWithType[DrawWithType["brush"] = 0] = "brush";
    DrawWithType[DrawWithType["pen"] = 1] = "pen";
})(DrawWithType || (DrawWithType = {}));
var BlendMode;
(function (BlendMode) {
    //
    // Summary:
    //     Default blending mode, also known as "Normal" or "Alpha Blending"
    BlendMode[BlendMode["Normal"] = 0] = "Normal";
    //
    // Summary:
    //     Blends the 2 values by multiplication.
    BlendMode[BlendMode["Multiply"] = 1] = "Multiply";
    //
    // Summary:
    //     Blends the 2 values by addition.
    BlendMode[BlendMode["Add"] = 2] = "Add";
    //
    // Summary:
    //     Blends the 2 values by subtraction.
    BlendMode[BlendMode["Substract"] = 3] = "Substract";
    //
    // Summary:
    //     Multiplies the complements of the backdrop and source values, then complements
    //     the result.
    BlendMode[BlendMode["Screen"] = 4] = "Screen";
    //
    // Summary:
    //     Selects the minimum of the backdrop and source values.
    BlendMode[BlendMode["Darken"] = 5] = "Darken";
    //
    // Summary:
    //     Selects the max of the backdrop and source values.
    BlendMode[BlendMode["Lighten"] = 6] = "Lighten";
    //
    // Summary:
    //     Multiplies or screens the values, depending on the backdrop vector values.
    BlendMode[BlendMode["Overlay"] = 7] = "Overlay";
    //
    // Summary:
    //     Multiplies or screens the colors, depending on the source value.
    BlendMode[BlendMode["HardLight"] = 8] = "HardLight";
    //
    // Summary:
    //     returns the source colors
    BlendMode[BlendMode["Src"] = 9] = "Src";
    //
    // Summary:
    //     returns the source over the destination
    BlendMode[BlendMode["Atop"] = 10] = "Atop";
    //
    // Summary:
    //     returns the detination over the source
    BlendMode[BlendMode["Over"] = 11] = "Over";
    //
    // Summary:
    //     the source where the desitnation and source overlap
    BlendMode[BlendMode["In"] = 12] = "In";
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    BlendMode[BlendMode["Out"] = 13] = "Out";
    //
    // Summary:
    //     the destination where the source does not overlap it
    BlendMode[BlendMode["Dest"] = 14] = "Dest";
    //
    // Summary:
    //     the source where they dont overlap othersie dest in overlapping parts
    BlendMode[BlendMode["DestAtop"] = 15] = "DestAtop";
    //
    // Summary:
    //     the destnation over the source
    BlendMode[BlendMode["DestOver"] = 16] = "DestOver";
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    BlendMode[BlendMode["DestIn"] = 17] = "DestIn";
    //
    // Summary:
    //     the source where the desitnation and source overlap
    BlendMode[BlendMode["DestOut"] = 18] = "DestOut";
    //
    // Summary:
    //     the clear.
    BlendMode[BlendMode["Clear"] = 19] = "Clear";
    //
    // Summary:
    //     clear where they overlap
    BlendMode[BlendMode["Xor"] = 20] = "Xor";
})(BlendMode || (BlendMode = {}));
class JSColor {
    constructor(hex) {
        this.hexString = hex;
    }
}
class JSBrush extends idObject {
    static createSolid(color) {
        return new JSSolidBrush(color);
    }
}
class JSSolidBrush extends BrushBase {
    constructor(color) {
        super(_api.createSolidBrush(color.hexString));
        this.color = color;
    }
}
class JSFont extends idObject {
    static Install(path) {
        _api.installFont(path);
    }
    constructor(family, size) {
        super(_api.getFont(family, size));
        this.family = family;
        this.size = size;
    }
    MeasureText(text) {
        return _api.measureText(this.id, text);
    }
}
class JSImage extends idObject {
    static Load(path, isPersistent = false) {
        return new JSImage(_api.loadImage(path, isPersistent));
    }
    static Create(width, height, isPersistent = false) {
        return new JSImage(_api.createImage(width, height, isPersistent));
    }
    constructor(id) {
        super(id);
        this.size = _api.getImageSize(id);
    }
    DrawLines(drawWith, points) {
        _api.drawLines(this.id, drawWith, points);
    }
    Fill(brush) {
        _api.fill(this.id, brush.id);
    }
    SetOutput(name = "") {
        _api.setOutput(this.id, name);
    }
    DrawImage(texture, blend = BlendMode.Normal, percent = 1, size, location = { x: 0, y: 0 }) {
        let b = blend;
        size = (size) || texture.size;
        _api.drawImage(this.id, texture.id, b, percent, size, location);
    }
    DrawText(text, font, drawWith, location) {
        _api.drawText(this.id, text, font.id, drawWith, location);
    }
    DrawEclipse(drawWith, location, radius) {
        _api.drawEclipse(this.id, drawWith, location, radius);
    }
}
//# sourceMappingURL=boot.js.map