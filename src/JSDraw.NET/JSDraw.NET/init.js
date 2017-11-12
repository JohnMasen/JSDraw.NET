class JSImage {
    constructor(id) {
        this.id = id;
        this.width = _manager.getImageWidth(this.id);
        this.height = _manager.getImageHeight(this.id);
    }
    static create(width, height, isPersistent) {
        isPersistent = isPersistent || false;
        var id = _manager.createImage(width, height, isPersistent);
        return new JSImage(id);
    }
    static load(path) {
        var id = _manager.load(path);
        return new JSImage(id);
    }

    setOutput(tag) {
        tag = tag || "";
        _manager.setOutput(this.id, tag);
    }

    drawLines(points) {
        _manager.drawLines(this.id, points);
    }

    setBrushColor(color) {
        _manager.setBrushColor(color);
    }

    fill() {
        _manager.fill(this.id);
    }

    drawImage(texture, position, size,  alpha) {
        alpha = alpha || 1;
        size = size || { "width": texture.width, "height": texture.height };
        position = position || { "x": 0, "y": 0 };
        _manager.drawImage(this.id, texture.id, size, position,alpha);
    }
    load(path) {
        var id = _manager.load(path);
        return new JSImage(id);
    }
    setFont(path,size) {
        _manager.setFont(path,size);
    }
    drawText(text, position) {
        _manager.drawText(this.id, text, position);
    }
    
}