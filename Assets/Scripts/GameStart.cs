using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {



	public void ChangeLevel(){


		Parameters.parameterSize.y = GameObject.Find("Height").GetComponent<Slider>().value;

		Parameters.parameterSize.x = GameObject.Find("Width").GetComponent<Slider>().value;

		Parameters.parameterSize.z = GameObject.Find("Length").GetComponent<Slider>().value;

		Parameters.parameterRotation = GameObject.Find("Rotation").GetComponent<Toggle>().isOn;


		DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene("EnviroScene");


	}

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }
}
