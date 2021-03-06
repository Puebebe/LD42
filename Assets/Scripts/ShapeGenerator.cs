﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    static int boxWidth;
    static int boxHeight;
    static int boxLength;
    int min = 3;
    int max = 5;
    GameObject[,,] cubes;
    bool[,,] usedCubes;

    public static int NumberOfCubes
    {
        get
        {
            return boxWidth * boxHeight * boxLength;
        }
    }

    public static Vector3 Dimensions
    {
        get
        {
            return new Vector3(boxWidth, boxHeight, boxLength);
        }
    }


    // Use this for initialization
    void Awake ()
    {
        boxWidth = (int)Parameters.parameterSize.x;
        boxHeight = (int)Parameters.parameterSize.y;
        boxLength = (int)Parameters.parameterSize.z;
        cubes = new GameObject[boxWidth, boxHeight, boxLength];
        usedCubes = new bool[boxWidth, boxHeight, boxLength];

        for (int i = 0; i < boxWidth; i++)
        {
            for (int j = 0; j < boxHeight; j++)
            {
                for (int k = 0; k < boxLength; k++)
                {
                    cubes[i, j, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes[i, j, k].transform.SetParent(transform);
                    cubes[i, j, k].transform.localPosition = new Vector3(i, j, k);
                }
            }
        }

            
        List<GameObject> Shapes = new List<GameObject>();
        Vector3? nextUnusedCube = Vector3.zero;

        while(nextUnusedCube != null)
        {
            Shapes.Add(generateShape(nextUnusedCube.Value));
            nextUnusedCube = getNext();
        }

        transform.position = new Vector3(transform.position.x - boxWidth / 2f + 0.5f, transform.position.y, transform.position.z - boxLength / 2f + 0.5f);

        float force = Mathf.Sqrt(NumberOfCubes) / 2;

        foreach (var shape in Shapes)
        {
            float x = Random.Range(-force, force);
            float y = Random.Range(-force, force);
            float z = Random.Range(-force, force);
            Vector3 v = new Vector3(x, y, z);

            if (Shapes.Count == 1)
            {
                v.Set(x, 0, -Mathf.Abs(z));
                v.Normalize();
                v.Scale(new Vector3(boxWidth, boxHeight, boxLength));
            }

            shape.GetComponent<Rigidbody>().velocity = v;
        }
    }

    Vector3? getNext()
    {
        for (int i = 0; i < boxWidth; i++)
        {
            for (int j = 0; j < boxHeight; j++)
            {
                for (int k = 0; k < boxLength; k++)
                {
                    if (usedCubes[i, j, k] != true)
                        return new Vector3(i, j, k);
                }
            }
        }

        return null;
    }

    GameObject generateShape(Vector3 firstCube)
    {
        GameObject shape = new GameObject();
        shape.name = "Shape";
        shape.transform.SetParent(transform);
        shape.transform.localPosition = firstCube;
        Color color = Random.ColorHSV(0, 1);
        shape.AddComponent<Rigidbody>();
        shape.AddComponent<PlayerInput>();
        shape.GetComponent<Rigidbody>().freezeRotation = !Parameters.parameterRotation;

        int x = (int)firstCube.x;
        int y = (int)firstCube.y;
        int z = (int)firstCube.z;

        usedCubes[x, y, z] = true;
        cubes[x, y, z].transform.SetParent(shape.transform);

        int cubesInShape = Random.Range(min, max + 1);

        for (int i = 1; i < cubesInShape; i++)
        {
            Vector3? cords = findNext(x, y, z);

            if (cords == null)
                break;
      
            x = (int)cords.Value.x;
            y = (int)cords.Value.y;
            z = (int)cords.Value.z;

            usedCubes[x, y, z] = true;
            cubes[x, y, z].transform.SetParent(shape.transform);
        }

        var renderers = shape.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
            renderer.material.color = color;
        

        return shape;
    }

    Vector3? findNext(int x, int y, int z)
    {

        List<int> Directions = new List<int>();

        if (x + 1 < boxWidth && !usedCubes[x + 1, y, z])
        {
            Directions.Add(0);
        }
        if (x - 1 >= 0 && !usedCubes[x - 1, y, z]){
            Directions.Add(1);
        }
        if (y + 1 < boxHeight && !usedCubes[x, y + 1, z])
        {
            Directions.Add(2);
        }
        if (y - 1 >= 0 && !usedCubes[x, y - 1, z]){
            Directions.Add(3);
        }
        if (z + 1 < boxLength && !usedCubes[x, y, z + 1])
        {
            Directions.Add(4);
        }
        if (z - 1 >= 0 && !usedCubes[x, y, z - 1])
        {
            Directions.Add(5);
        }

        if (Directions.Count == 0)
            return null;

        int index = Random.Range(0, Directions.Count);

        switch (Directions[index])
        {
            case 0:
                return new Vector3(x + 1, y, z);
                //return Vector3.right + new Vector3(x, y, z);

            case 1:
                return new Vector3(x - 1, y, z);

            case 2:
                return new Vector3(x, y + 1, z);

            case 3:
                return new Vector3(x, y - 1, z);

            case 4:
                return new Vector3(x, y, z + 1);

            case 5:
                return new Vector3(x, y, z - 1);

            default:
                return null;
        }
    }
}
