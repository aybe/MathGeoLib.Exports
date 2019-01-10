using System.Runtime.InteropServices;
using JetBrains.Annotations;

#if UNITY || UNITY_EDITOR
// using Unity type
#else
// ReSharper disable once CheckNamespace
namespace MathGeoLib
{
    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public struct Ray
    {
        public readonly Vector3 Origin;

        public readonly Vector3 Direction;

        public Ray(Vector3 point1, Vector3 point2)
        {
            Origin = point1;
            Direction = point2;
        }

        public override string ToString()
        {
            return $"{nameof(Origin)}: {Origin}, {nameof(Direction)}: {Direction}";
        }
    }
}

#endif