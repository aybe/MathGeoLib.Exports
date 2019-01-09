// MathGeoLib.Exports.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

// ReSharper disable once CppUnusedIncludeDirective
#include <algorithm>
#include "MathGeoLib.h"

#define API extern "C" __declspec(dllexport)

API void optimal_enclosing_obb(const vec *point_array, const int num_points, vec* center, vec* extent, vec* axis)
{
	const auto obb = OBB::OptimalEnclosingOBB(point_array, num_points);

	*center = obb.pos;
	*extent = obb.r;

	for (auto i = 0; i < 3; ++i)
	{
		axis[i] = obb.axis[i];
	}
}

