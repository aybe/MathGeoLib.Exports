﻿using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
#if UNITY || UNITY_EDITOR
using UnityEngine;

#endif

#pragma warning disable IDE1006 // naming rules blah blah blah

// ReSharper disable once CheckNamespace
namespace MathGeoLib
{
    [PublicAPI]
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public sealed class OrientedBoundingBox
    {
        #region Native

        private static class NativeMethods
        {
#if UNITY || UNITY_EDITOR
            private const string DllName = "MathGeoLib.Exports";
#else
            private const string DllName = "MathGeoLib.Exports.dll";
#endif

            [DllImport(DllName)]
            public static extern void obb_optimal_enclosing(
                Vector3[] points, int numPoints, out Vector3 center, out Vector3 extent, [In] [Out] Vector3[] axis
            );

            [DllImport(DllName)]
            public static extern void obb_brute_enclosing(
                Vector3[] points, int numPoints, out Vector3 center, out Vector3 extent, [In] [Out] Vector3[] axis
            );

            [DllImport(DllName)]
            public static extern void obb_enclose(
                [In] [Out] OrientedBoundingBox box,
                Vector3 point
            );

            [DllImport(DllName)]
            public static extern void obb_point_inside(
                [In] [Out] OrientedBoundingBox box,
                float x, float y, float z, out Vector3 point
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_contains_point(
                [In] [Out] OrientedBoundingBox box,
                Vector3 other
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_contains_line_segment(
                [In] [Out] OrientedBoundingBox box,
                Line other
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_contains_obb(
                [In] [Out] OrientedBoundingBox box,
                OrientedBoundingBox other
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_intersects_obb(
                [In] [Out] OrientedBoundingBox box,
                OrientedBoundingBox other
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_intersects_line_segment(
                [In] [Out] OrientedBoundingBox box,
                Line other
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_intersects_ray(
                [In] [Out] OrientedBoundingBox box,
                Ray other
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_intersects_plane(
                [In] [Out] OrientedBoundingBox box,
                Plane other
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_is_finite(
                [In] [Out] OrientedBoundingBox box
            );

            [DllImport(DllName)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool obb_is_degenerate(
                [In] [Out] OrientedBoundingBox box
            );

            [DllImport(DllName)]
            public static extern void obb_corner_point(
                [In] [Out] OrientedBoundingBox box,
                int index, out Vector3 point
            );

            [DllImport(DllName)]
            public static extern void obb_face_point(
                [In] [Out] OrientedBoundingBox box,
                int index, float u, float v, out Vector3 point
            );

            [DllImport(DllName)]
            public static extern int obb_num_faces();

            [DllImport(DllName)]
            public static extern int obb_num_edges();

            [DllImport(DllName)]
            public static extern int obb_num_vertices();

            [DllImport(DllName)]
            public static extern void obb_scale(
                [In] [Out] OrientedBoundingBox box,
                Vector3 center, Vector3 factor
            );

            [DllImport(DllName)]
            public static extern void obb_translate(
                [In] [Out] OrientedBoundingBox box,
                Vector3 offset
            );

            [DllImport(DllName)]
            public static extern float obb_distance(
                [In] [Out] OrientedBoundingBox box,
                Vector3 point
            );

            [DllImport(DllName)]
            public static extern void obb_point_on_edge(
                [In] [Out] OrientedBoundingBox box,
                int index, float u, out Vector3 point
            );

            [DllImport(DllName)]
            public static extern void obb_edge(
                [In] [Out] OrientedBoundingBox box,
                int index, out Line segment
            );

            [DllImport(DllName)]
            public static extern void obb_world_to_local(
                [In] [Out] OrientedBoundingBox box,
                out Matrix3X4 local
            );

            [DllImport(DllName)]
            public static extern void obb_local_to_world(
                [In] [Out] OrientedBoundingBox box,
                out Matrix3X4 world
            );

            [DllImport(DllName)]
            public static extern void obb_face_plane(
                [In] [Out] OrientedBoundingBox box,
                int index, out Plane plane
            );

            [DllImport(DllName)]
            public static extern void obb_random_point_on_surface(
                [In] OrientedBoundingBox box,
                LCG rng,
                out Vector3 outVec
            );
        }

        #endregion

        #region Fields

        // NOTE region prevents re-ordering on cleanup

        public Vector3 Center;
        public Vector3 Extent;
        public Vector3 Right;
        public Vector3 Up;
        public Vector3 Forward;
        
        #endregion

        #region Constructors

        [PublicAPI]
        public OrientedBoundingBox()
        {
            // for serialization
        }

        public OrientedBoundingBox(Vector3 center, Vector3 extent, Vector3 right, Vector3 up, Vector3 forward)
        {
            Center = center;
            Extent = extent;
            Right = right;
            Up = up;
            Forward = forward;
        }

        #endregion

        #region Static

        public static int NumEdges => NativeMethods.obb_num_edges();

        public static int NumFaces => NativeMethods.obb_num_faces();

        public static int NumVertices => NativeMethods.obb_num_vertices();

        public static OrientedBoundingBox OptimalEnclosing(Vector3[] points)
        {
            var axis = new Vector3[3];

            NativeMethods.obb_optimal_enclosing(points, points.Length, out var center, out var extent, axis);

            var box = new OrientedBoundingBox(center, extent, axis[0], axis[1], axis[2]);

            return box;
        }

        public static OrientedBoundingBox BruteEnclosing(Vector3[] points)
        {
            var axis = new Vector3[3];

            NativeMethods.obb_brute_enclosing(points, points.Length, out var center, out var extent, axis);

            var box = new OrientedBoundingBox(center, extent, axis[0], axis[1], axis[2]);

            return box;
        }

        #endregion

        #region Instance
        
        public bool IsDegenerate => NativeMethods.obb_is_degenerate(this);

        public bool IsFinite => NativeMethods.obb_is_finite(this);

        public bool Contains(Vector3 other)
        {
            return NativeMethods.obb_contains_point(this, other);
        }

        public bool Contains(Line other)
        {
            return NativeMethods.obb_contains_line_segment(this, other);
        }

        public bool Contains(OrientedBoundingBox other)
        {
            return NativeMethods.obb_contains_obb(this, other);
        }

        public bool Intersects(OrientedBoundingBox other)
        {
            return NativeMethods.obb_intersects_obb(this, other);
        }

        public bool Intersects(Ray other)
        {
            return NativeMethods.obb_intersects_ray(this, other);
        }

        public bool Intersects(Plane other)
        {
            return NativeMethods.obb_intersects_plane(this, other);
        }

        public bool Intersects(Line other)
        {
            return NativeMethods.obb_intersects_line_segment(this, other);
        }

        public Vector3 CornerPoint(int index)
        {
            NativeMethods.obb_corner_point(this, index, out var point);
            return point;
        }

        public void Enclose(Vector3 point)
        {
            NativeMethods.obb_enclose(this, point);
        }

        public Vector3 FacePoint(int index, float u, float v)
        {
            NativeMethods.obb_face_point(this, index, u, v, out var point);
            return point;
        }

        public Vector3 PointInside(float x, float y, float z)
        {
            NativeMethods.obb_point_inside(this, x, y, z, out var point);
            return point;
        }

        public void Scale(Vector3 center, Vector3 factor)
        {
            NativeMethods.obb_scale(this, center, factor);
        }

        public void Translate(Vector3 offset)
        {
            NativeMethods.obb_translate(this, offset);
        }

        public float Distance(Vector3 point)
        {
            return NativeMethods.obb_distance(this, point);
        }

        public Vector3 PointOnEdge(int index, float u)
        {
            NativeMethods.obb_point_on_edge(this, index, u, out var point);
            return point;
        }

        public Line Edge(int index)
        {
            NativeMethods.obb_edge(this, index, out var segment);
            return segment;
        }

        public Matrix3X4 WorldToLocal()
        {
            NativeMethods.obb_world_to_local(this, out var local);
            return local;
        }

        public Matrix3X4 LocalToWorld()
        {
            NativeMethods.obb_local_to_world(this, out var world);
            return world;
        }

        public Plane FacePlane(int index)
        {
            NativeMethods.obb_face_plane(this, index, out var plane);
            return plane;
        }

        public override string ToString()
        {
            return $"{nameof(Center)}: {Center}, {nameof(Extent)}: {Extent}";
        }

        public Vector3 RandomPointOnSurface(LCG rng)
        {
            NativeMethods.obb_random_point_on_surface(this, rng, out var point);
            return point;
        }

        #endregion
    }
}
