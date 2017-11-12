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

    drawImage(texture, size, point) {
        _manager.drawImage(this.id, texture.id, size, point);
    }
}