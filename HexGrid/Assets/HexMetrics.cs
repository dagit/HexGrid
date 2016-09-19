using UnityEngine;
using System.Collections;

public static class HexMetrics {
	public const float outerRadius = 10f;
	public const float sqrt3div2 = 0.86602540378444f;
	public const float innerRadius = outerRadius * sqrt3div2;

	public static Vector3[] corners = {
		new Vector3 (0f, 0f, outerRadius),
		new Vector3 (innerRadius, 0f, 0.5f * outerRadius),
		new Vector3 (innerRadius, 0f, -0.5f * outerRadius),
		new Vector3 (0f, 0f, -outerRadius),
		new Vector3 (-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3 (-innerRadius, 0f, 0.5f * outerRadius),
		new Vector3 (0f, 0f, outerRadius)
	};
}
