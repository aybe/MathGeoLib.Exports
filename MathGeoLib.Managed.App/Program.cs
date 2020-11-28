using System;
using System.Linq;
using System.Diagnostics;
#if UNITY_EDITOR || UNITY
using UnityEngine;
#endif

namespace MathGeoLib.Managed.App
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var random = new System.Random(1234);

            Vector3 GetPoint()
            {
                float Get()
                {
                    return (float)Math.Round((1.0d - 2.0d * random.NextDouble()) * 10);
                }

                return new Vector3(Get(), Get(), Get());
            }

            var points = Enumerable.Range(0, 10000).Select(s => GetPoint()).ToArray();


            var numEdges = OrientedBoundingBox.NumEdges;
            var numFaces = OrientedBoundingBox.NumFaces;
            var numVertices = OrientedBoundingBox.NumVertices;

#if UNITY_EDITOR || UNITY
            var box = new OrientedBoundingBox(Vector3.zero, Vector3.zero, Vector3.right, Vector3.up, Vector3.forward);
#else
            var box = new OrientedBoundingBox(Vector3.Zero, Vector3.Zero, Vector3.Right, Vector3.Up, Vector3.Forward);

#endif

            foreach (var point in points)
                box.Enclose(point);

#if UNITY_EDITOR || UNITY
            var b1 = box.Contains(Vector3.zero);
#else
            var b1 = box.Contains(Vector3.Zero);

#endif
            var b2 = box.Contains(new Vector3(100, 100, 100));
            var c1 = box.CornerPoint(0);
            var c2 = box.CornerPoint(7);
            var f1 = box.FacePoint(0, 0.0f, 0.0f);
            var f2 = box.FacePoint(0, 1.0f, 1.0f);
            var p1 = box.PointInside(0.0f, 0.0f, 0.0f);
            var p2 = box.PointInside(1.0f, 1.0f, 1.0f);
            var p3 = box.PointInside(0.5f, 0.5f, 0.5f);

#if UNITY_EDITOR || UNITY
            box.Translate(Vector3.one * +2);
            box.Translate(Vector3.one * -2);
            box.Scale(Vector3.zero, Vector3.one * 0.5f);
            box.Scale(Vector3.zero, Vector3.one * 2.0f);
            var d1 = box.Distance(Vector3.one * 20.0f);
            var d2 = box.Distance(Vector3.one * 30.0f);
#else
            box.Translate(Vector3.One * +2);
            box.Translate(Vector3.One * -2);
            box.Scale(Vector3.Zero, Vector3.One * 0.5f);
            box.Scale(Vector3.Zero, Vector3.One * 2.0f);
            var d1 = box.Distance(Vector3.One * 20.0f);
            var d2 = box.Distance(Vector3.One * 30.0f);
#endif
            var e1 = box.PointOnEdge(0, 0.0f);
            var e2 = box.PointOnEdge(0, 1.0f);
            var e3 = box.PointOnEdge(0, 0.5f);
            var l1 = box.Edge(0);
            var l2 = box.Edge(1);
            var m1 = box.WorldToLocal();
            var m2 = box.LocalToWorld();
            var n1 = box.FacePlane(0);
            var n2 = box.FacePlane(1);
            var x1 = OrientedBoundingBox.OptimalEnclosing(points);
            var x2 = OrientedBoundingBox.BruteEnclosing(points);

            LCG lcg = new LCG();
            lcg.IntFast();
            lcg.Float();
            lcg.Int();
            lcg.Int(0,10);

#if UNITY_EDITOR || UNITY
            OrientedBoundingBox obb = new OrientedBoundingBox(Vector3.zero, Vector3.one, Vector3.right, Vector3.up, Vector3.forward);
#else
            OrientedBoundingBox obb = new OrientedBoundingBox(Vector3.Zero, Vector3.One, Vector3.Right, Vector3.Up, Vector3.Forward);
#endif
            obb.RandomPointOnSurface(lcg);
        }
    }
}
