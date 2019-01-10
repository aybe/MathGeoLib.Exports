using System.Runtime.InteropServices;
using JetBrains.Annotations;

#if UNITY || UNITY_EDITOR
#else

// ReSharper disable once CheckNamespace
namespace MathGeoLib
{
    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public readonly float X;

        public readonly float Y;

        public readonly float Z;

        public static Vector3 Right { get; } = new Vector3(1, 0, 0);

        public static Vector3 Up { get; } = new Vector3(0, 1, 0);

        public static Vector3 Forward { get; } = new Vector3(0, 0, 1);

        public static Vector3 Zero { get; } = new Vector3(0, 0, 0);

        public static Vector3 One { get; } = new Vector3(1, 1, 1);

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
        }

        public static Vector3 operator *(Vector3 vector3, float scale)
        {
            return new Vector3(vector3.X * scale, vector3.Y * scale, vector3.Z * scale);
        }
    }
}

#endif // !UNITY