using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp.Drawing;
using System.Numerics;
using SixLabors.Primitives;
using SixLabors.ImageSharp;
using System.Linq;

namespace JSDraw.NET
{
    public partial class JSDrawAPI
    {
        private MatrixStack getMatrix(int id)
        {
            return manager.GetItem<MatrixStack>(id);
        }
        public int CreateMatrix()
        {
            return add(new MatrixStack());
        }

        private PointF[] transformByMatrix(int id,IEnumerable<PointF> points)
        {
            var m = getMatrix(id);
            return (from item in points
                    select PointF.Transform(item, m.TransformMatrix)).ToArray();
        }

        private PointF transformByMatrix(int id, PointF point)
        {
            var m = getMatrix(id);
            return PointF.Transform(point, m.TransformMatrix);
        }

        private Point transformByMatrix(int id, Point point)
        {
            var m = getMatrix(id);
            var p= Point.Transform(point, m.TransformMatrix);
            return new Point((int)p.X, (int)p.Y);
        }

        private SizeF transformByMatrix(int id, SizeF size)
        {
            var m = getMatrix(id);
            return SizeF.Transform(size, m.TransformMatrix);
        }

        private Size transformByMatrix(int id, Size size)
        {
            var m = getMatrix(id);
            var tmp= Size.Transform(size, m.TransformMatrix);
            return new Size((int)tmp.Width, (int)tmp.Height);
        }

        public void MatrixTranslation(int id, float x, float y)
        {
            var m = getMatrix(id);
            m.Matrix = m.Matrix * Matrix3x2.CreateTranslation(x, y);
        }

        public void MatrixRotate(int id, float degrees, PointF center)
        {
            var m = getMatrix(id);
            m.Matrix = m.Matrix * Matrix3x2Extensions.CreateRotationDegrees(degrees, center);
        }

        public void MatrixScale(int id, float x, float y)
        {
            var m = getMatrix(id);
            m.Matrix = m.Matrix * Matrix3x2.CreateScale(x, y);
        }

        public void MatrixPush(int id)
        {
            getMatrix(id).PushMatrix();
        }

        public void MatrixPop(int id)
        {
            getMatrix(id).PopMatrix();
        }

        public void MatrixReset(int id)
        {
            getMatrix(id).ResetMatrix();
        }
    }
}
