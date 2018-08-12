using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaBehaviour : MonoBehaviour
{
    int goalCubes;
    int actualCubes = 0;

    // Use this for initialization
    void Start()
    {
        goalCubes = GameObject.Find("Shapes").GetComponent<CubeGenerator>().NumberOfCubes;
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        Vector3 Scale = GameObject.Find("Shapes").GetComponent<CubeGenerator>().Dimensions;
        transform.localScale = Scale;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x + Scale.y / 2, transform.localPosition.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Cube")
            return;
        
        actualCubes++;
        if (actualCubes == goalCubes)
            Debug.Log("Win Faggot!");

        Debug.Log(actualCubes + "/" + goalCubes);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "Cube")
            return;

        actualCubes--;

        Debug.Log(actualCubes + "/" + goalCubes);
    }
    

}
