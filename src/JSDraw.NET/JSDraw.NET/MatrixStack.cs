using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace JSDraw.NET
{
    public class MatrixStack
    {
        public Matrix3x2 Matrix { get; set; } = Matrix3x2.Identity;
        Stack<Matrix3x2> stack = new Stack<Matrix3x2>();
        private Matrix3x2 world = Matrix3x2.Identity;
        public Matrix3x2 TransformMatrix
        {
            get { return Matrix * world; }
        }

        public void PushMatrix()
        {
            world = Matrix * world;
            stack.Push(world);
            Matrix = Matrix3x2.Identity;
        }
        public void PopMatrix()
        {
            world = stack.Pop();
            Matrix = Matrix3x2.Identity;
        }
        public void ResetMatrix()
        {
            Matrix = Matrix3x2.Identity;
            world = Matrix3x2.Identity;
            stack.Clear();
        }

    }
}
