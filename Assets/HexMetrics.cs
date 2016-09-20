using UnityEngine;
using System.Collections;

public class HexMetrics : MonoBehaviour {
	public float outerRadius = 10f;
	public float sqrt3div2 = 0.86602540378444f;
    public float innerRadius; /* = outerRadius * sqrt3div2; */
    public Vector3[] corners;

    public void Awake()
    {
        innerRadius = outerRadius * sqrt3div2;

        corners = new Vector3[] {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
        };
    }
}
