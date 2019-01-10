using System;
using JetBrains.Annotations;
#if UNITY_EDITOR
using UnityEditor;

#endif

#if UNITY_EDITOR
// ReSharper disable once CheckNamespace
namespace MathGeoLib
{
    public static class OrientedBoundingBoxExtensions
    {
        public static void DrawWireCube([NotNull] this OrientedBoundingBox box)
        {
            if (box == null)
                throw new ArgumentNullException(nameof(box));

            var p0 = box.CornerPoint(0);
            var p1 = box.CornerPoint(1);
            var p2 = box.CornerPoint(2);
            var p3 = box.CornerPoint(3);
            var p4 = box.CornerPoint(4);
            var p5 = box.CornerPoint(5);
            var p6 = box.CornerPoint(6);
            var p7 = box.CornerPoint(7);

            var points = new[]
            {
                p0, p2, p6, p4, p0,
                p1, p3, p7, p5, p1
            };

            Handles.DrawPolyLine(points);

            Handles.DrawLine(p2, p3);
            Handles.DrawLine(p6, p7);
            Handles.DrawLine(p4, p5);
        }
    }
}
#endif