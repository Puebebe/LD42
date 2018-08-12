using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAreaRender : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject SolidArea = GameObject.CreatePrimitive(PrimitiveType.Cube);

		Vector3 Scale = GameObject.Find("Shapes").GetComponent<CubeGenerator>().Dimensions;
		SolidArea.transform.SetParent(gameObject.transform);
		transform.localScale = new Vector3 (Scale.x*2, 10, Scale.z*2);
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y -5f, transform.localPosition.z);
		Color color = Random.ColorHSV(0, 1);
		gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = color;


	}

	// Update is called once per frame
	void Update () {

	}
}
