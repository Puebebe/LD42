using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour {

    public Material mat;



    // Use this for initialization
    void Start()
    {
      // You can change that line to provide another MeshFilter
MeshFilter filter = gameObject.AddComponent< MeshFilter >();
Mesh mesh = filter.mesh;
mesh.Clear();

float length = 1f;
float width = 1f;
float height = 1f;

#region Vertices
Vector3 p0 = new Vector3( -length * .5f,	-width * .5f, height * .5f );
Vector3 p1 = new Vector3( length * .5f, 	-width * .5f, height * .5f );
Vector3 p2 = new Vector3( length * .5f, 	-width * .5f, -height * .5f );
Vector3 p3 = new Vector3( -length * .5f,	-width * .5f, -height * .5f );

Vector3 p4 = new Vector3( -length * .5f,	width * .5f,  height * .5f );
Vector3 p5 = new Vector3( length * .5f, 	width * .5f,  height * .5f );
Vector3 p6 = new Vector3( length * .5f, 	width * .5f,  -height * .5f );
Vector3 p7 = new Vector3( -length * .5f,	width * .5f,  -height * .5f );

Vector3 p8 = new Vector3( -length * .45f,	-width * .45f,  height * .45f );
Vector3 p9 = new Vector3( length * .45f, 	-width * .45f,  height * .45f );
Vector3 p10 = new Vector3( length * .45f, 	-width * .45f,  -height * .45f );
Vector3 p11 = new Vector3( -length * .45f,	-width * .4f,  -height * .45f );

Vector3 p12 = new Vector3( -length * .45f,	width * .45f,  height * .45f );
Vector3 p13 = new Vector3( length * .45f, 	width * .45f,  height * .45f );
Vector3 p14 = new Vector3( length * .45f, 	width * .45f,  -height * .45f );
Vector3 p15 = new Vector3( -length * .45f,	width * .45f,  -height * .45f );

Vector3[] vertices = new Vector3[]
{



  // Side Left
  p15, p11, p8, p12,

  // Side Right
  p9, p10, p14, p13,


  // Top Front
  p12, p13, p5, p4,

  // Top Back
  p14, p15, p7, p6,

  // Top Left
  p15, p12, p4, p7,

  // Top Right
  p13, p14, p6, p5

};
#endregion

#region Normales
Vector3 up 	= Vector3.up;
Vector3 down 	= Vector3.down;
Vector3 front 	= Vector3.forward;
Vector3 back 	= Vector3.back;
Vector3 left 	= Vector3.left;
Vector3 right 	= Vector3.right;

Vector3[] normales = new Vector3[]
{
	// Bottom
	down, down, down, down,

	// Left
	left, left, left, left,

	// Front
	front, front, front, front,

	// Back
	back, back, back, back,

	// Right
	right, right, right, right,

	// Top
	up, up, up, up
};
#endregion

#region UVs
Vector2 _00 = new Vector2( 0f, 0f );
Vector2 _10 = new Vector2( 1f, 0f );
Vector2 _01 = new Vector2( 0f, 1f );
Vector2 _11 = new Vector2( 1f, 1f );

Vector2[] uvs = new Vector2[]
{
	// Bottom
	_11, _01, _00, _10,

	// Left
	_11, _01, _00, _10,

	// Front
	_11, _01, _00, _10,

	// Back
	_11, _01, _00, _10,

	// Right
	_11, _01, _00, _10,

	// Top
	_11, _01, _00, _10,
};
#endregion

#region Triangles
int[] triangles = new int[]
{
	// Bottom
	3, 1, 0,
	3, 2, 1,

	// Left
	3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
	3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,

	// Front
	3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
	3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,

	// Back
	3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
	3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,

	// Right
	3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
	3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,

	// Top
	3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
	3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

};
#endregion

mesh.vertices = vertices;
mesh.normals = normales;
mesh.uv = uvs;
mesh.triangles = triangles;

mesh.RecalculateBounds();
;
    }
}
