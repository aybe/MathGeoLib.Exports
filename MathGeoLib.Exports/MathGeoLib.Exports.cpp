// MathGeoLib.Exports.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

// ReSharper disable once CppUnusedIncludeDirective
#include <algorithm>
#include "MathGeoLib.h"

#define API extern "C" __declspec(dllexport)

API void obb_optimal_enclosing(const vec *point_array, const int num_points, vec *center, vec *extent, vec *axis)
{
	const auto obb = OBB::OptimalEnclosingOBB(point_array, num_points);

	*center = obb.pos;
	*extent = obb.r;

	for (auto i = 0; i < 3; ++i)
	{
		axis[i] = obb.axis[i];
	}
}

API void obb_brute_enclosing(const vec *point_array, const int num_points, vec *center, vec *extent, vec *axis)
{
	const auto obb = OBB::BruteEnclosingOBB(point_array, num_points);

	*center = obb.pos;
	*extent = obb.r;

	for (auto i = 0; i < 3; ++i)
	{
		axis[i] = obb.axis[i];
	}
}

API void obb_enclose(OBB *obb, vec *point)
{
	obb->Enclose(*point);
}

API void obb_point_inside(OBB *obb, float x, float y, float z, vec *point)
{
	*point = obb->PointInside(x, y, z);
}

API bool obb_contains_point(OBB *obb, vec *other)
{
	return obb->Contains(*other);
}

API bool obb_contains_obb(OBB *obb, OBB *other)
{
	return obb->Contains(*other);
}

API bool obb_contains_line_segment(OBB *obb, LineSegment *other)
{
	return obb->Contains(*other);
}

API bool obb_intersects_line_segment(OBB *obb, LineSegment *other)
{
	return obb->Intersects(*other);
}

API bool obb_intersects_ray(OBB *obb, Ray *other)
{
	return obb->Intersects(*other);
}

API bool obb_intersects_plane(OBB *obb, Plane *other)
{
	return obb->Intersects(*other);
}

API bool obb_intersects_obb(OBB *obb, OBB *other)
{
	return obb->Intersects(*other);
}

API void obb_corner_point(OBB *obb, int index, vec *point)
{
	*point = obb->CornerPoint(index);
}

API void obb_face_point(OBB *obb, int index, float u, float v, vec *point)
{
	*point = obb->FacePoint(index, u, v);
}

API int obb_num_faces()
{
	return OBB::NumFaces();
}

API int obb_num_edges()
{
	return OBB::NumEdges();
}

API int obb_num_vertices()
{
	return OBB::NumVertices();
}

API void obb_scale(OBB *obb, vec *center, vec *factor)
{
	obb->Scale(*center, *factor);
}

API void obb_translate(OBB *obb, vec *offset)
{
	obb->Translate(*offset);
}

API float obb_distance(OBB *obb, vec *point)
{
	return obb->Distance(*point);
}

API void obb_point_on_edge(OBB *obb, int index, float u, vec *point)
{
	*point = obb->PointOnEdge(index, u);
}

API void obb_edge(OBB *obb, int index, LineSegment *segment)
{
	*segment = obb->Edge(index);
}

API void obb_world_to_local(OBB *obb, float3x4 *local)
{
	*local = obb->WorldToLocal();
}

API void obb_local_to_world(OBB *obb, float3x4 *world)
{
	*world = obb->LocalToWorld();
}

API void obb_face_plane(OBB *obb, int index, Plane *plane)
{
	*plane = obb->FacePlane(index);
}

API bool obb_is_finite(OBB *obb)
{
	return obb->IsFinite();
}

API bool obb_is_degenerate(OBB *obb)
{
	return obb->IsDegenerate();
}
