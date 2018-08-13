using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloor : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Color color = Random.ColorHSV(0, 1);
        gameObject.transform.GetComponent<Renderer>().material.color = color;
    }
}
