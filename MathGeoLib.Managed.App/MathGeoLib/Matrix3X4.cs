using System.Runtime.InteropServices;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace MathGeoLib
{
    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix3X4
    {
        public readonly float M00, M01, M02, M03;
        public readonly float M10, M11, M12, M13;
        public readonly float M20, M21, M22, M23;

        public Matrix3X4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21,
            float m22, float m23)
        {
            M00 = m00;
            M01 = m01;
            M02 = m02;
            M03 = m03;
            M10 = m10;
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M20 = m20;
            M21 = m21;
            M22 = m22;
            M23 = m23;
        }

        public override string ToString()
        {
            return
                $"{nameof(M00)}: {M00}, " +
                $"{nameof(M01)}: {M01}, " +
                $"{nameof(M02)}: {M02}, " +
                $"{nameof(M03)}: {M03}, " +
                $"{nameof(M10)}: {M10}, " +
                $"{nameof(M11)}: {M11}, " +
                $"{nameof(M12)}: {M12}, " +
                $"{nameof(M13)}: {M13}, " +
                $"{nameof(M20)}: {M20}, " +
                $"{nameof(M21)}: {M21}, " +
                $"{nameof(M22)}: {M22}, " +
                $"{nameof(M23)}: {M23}";
        }
    }
}