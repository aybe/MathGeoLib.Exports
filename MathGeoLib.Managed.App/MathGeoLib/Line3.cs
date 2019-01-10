using System.Runtime.InteropServices;
using JetBrains.Annotations;

#if UNITY || UNITY_EDITOR
using Vector3 = UnityEngine.Vector3;
#endif

// ReSharper disable once CheckNamespace
namespace MathGeoLib
{
    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public struct Line3
    {
        public readonly Vector3 Point1;

        public readonly Vector3 Point2;

        public Line3(Vector3 point1, Vector3 point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public override string ToString()
        {
            return $"{nameof(Point1)}: {Point1}, {nameof(Point2)}: {Point2}";
        }
    }
}