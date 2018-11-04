using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAreaRender : MonoBehaviour
{
    void Start()
    {
        GameObject SolidArea = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 Scale = ShapeGenerator.Dimensions;
        SolidArea.transform.SetParent(transform, false);
        transform.localScale = new Vector3(Scale.x * 2, 10, Scale.z * 2);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 5f, transform.localPosition.z);
        Color color = Random.ColorHSV(0, 1);
        transform.GetChild(0).GetComponent<Renderer>().material.color = color;
    }
}
