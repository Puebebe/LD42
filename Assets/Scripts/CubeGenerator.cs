using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    const int BOX_SIZE = 3;
    GameObject[,,] cubes = new GameObject[BOX_SIZE, BOX_SIZE, BOX_SIZE];
    

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < BOX_SIZE; i++)
        {
            for (int j = 0; j < BOX_SIZE; j++)
            {
                for (int k = 0; k < BOX_SIZE; k++)
                {
                    cubes[i, j, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes[i, j, k].transform.position = new Vector3(i, j, k);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
