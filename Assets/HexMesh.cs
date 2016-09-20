using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {
    public HexMetrics hexMetrics;

	Mesh hexMesh;
	List<Vector3> vertices;
	List<int> triangles;
    List<Vector2> uvs;

    MeshCollider meshCollider;

    List<Color> colors;
    public Texture2D atlas;
    public int atlasCols;
    public HexMetrics atlasHexMetrics;

    void Awake(){
		GetComponent<MeshFilter> ().mesh = hexMesh = new Mesh ();
        meshCollider = gameObject.AddComponent<MeshCollider>();
		hexMesh.name = "Hex Mesh";
		vertices = new List<Vector3> ();
        colors = new List<Color>();
		triangles = new List<int> ();
        uvs = new List<Vector2>();
	}

	public void Triangulate (HexCell[] cells){
		hexMesh.Clear ();
		vertices.Clear ();
		triangles.Clear ();
        uvs.Clear();
        colors.Clear();
		for (int i = 0; i < cells.Length; i++) {
			Triangulate (cells [i]);
		}
		hexMesh.vertices = vertices.ToArray ();
        hexMesh.colors = colors.ToArray();
		hexMesh.triangles = triangles.ToArray ();
        hexMesh.uv = uvs.ToArray();
		hexMesh.RecalculateNormals ();
        meshCollider.GetComponent<MeshRenderer>().material.mainTexture = atlas;
        meshCollider.sharedMesh = hexMesh;
	}

	void Triangulate(HexCell cell) {
		Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++) {
            AddTriangle(
                center,
                center + hexMetrics.corners[i],
                center + hexMetrics.corners[i + 1]
            );
            AddTriangleUVs(
                Vector3.zero,
                atlasHexMetrics.corners[i],
                atlasHexMetrics.corners[i + 1],
                cell.textureIdx
            );
            AddTriangleColor(cell.color);
		}
	}

    Vector2 XZToUV(Vector3 xz, int textureIdx)
    {
        if (textureIdx < 0)
            return Vector2.zero;
        return new Vector2(XtoU(xz.x, textureIdx), ZtoV(xz.z, 0));
    }

    float scaleAndTranslate(float x, float max, int colIndex, int cols)
    {
        /* TODO: fix rounding errors, currently shows up as a bit of texture getting cut off */
        /* x will be in the range [-max .. max] */
        /* First, scale it to the range [0 .. max] */
        var scaledToMax = (x + max)/2;
        /* Next, scale to unit [0 .. 1] */
        var scaledToUnit = scaledToMax / max;
        /* Now, translate it based on how the texture is split up:
         * [ 0 .. 1/atlasCols ]
         */
        var scaleToSlice = scaledToUnit / cols;
        /* finally, translate that to the correct portion of the atlas */
        return scaleToSlice + ((float)colIndex / cols);
    }

    float XtoU(float x, int textureIdx)
    {
        return scaleAndTranslate(x, atlasHexMetrics.innerRadius, textureIdx, atlasCols);
    }

    float ZtoV(float z, int textureIdx)
    {
        return scaleAndTranslate(z, atlasHexMetrics.outerRadius, 0, 1);
    }

    void AddTriangleUVs(Vector3 v1, Vector3 v2, Vector3 v3, int textureIdx)
    {
        Vector2 uv1 = XZToUV(v1, textureIdx);
        Vector2 uv2 = XZToUV(v2, textureIdx);
        Vector2 uv3 = XZToUV(v3, textureIdx);

        uvs.Add(uv1);
        uvs.Add(uv2);
        uvs.Add(uv3);
    }

    void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

	void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3){
		int vertexIndex = vertices.Count;
		vertices.Add (v1);
		vertices.Add (v2);
		vertices.Add (v3);
		triangles.Add (vertexIndex + 0);
		triangles.Add (vertexIndex + 1);
		triangles.Add (vertexIndex + 2);
	}
}
