using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void ChangeLevel()
    {
        Parameters.parameterSize.y = GameObject.Find("Height").GetComponent<Slider>().value;
        Parameters.parameterSize.x = GameObject.Find("Width").GetComponent<Slider>().value;
        Parameters.parameterSize.z = GameObject.Find("Length").GetComponent<Slider>().value;
        Parameters.parameterRotation = GameObject.Find("Rotation").GetComponent<Toggle>().isOn;

        //DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Gameplay");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
        else if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("Height").GetComponent<Slider>().value = Parameters.parameterSize.y;
            GameObject.Find("Width").GetComponent<Slider>().value = Parameters.parameterSize.x;
            GameObject.Find("Length").GetComponent<Slider>().value = Parameters.parameterSize.z;
            GameObject.Find("Rotation").GetComponent<Toggle>().isOn = Parameters.parameterRotation;
        }
    }

    public void UpdateLabel(string name)
    {
        GameObject.Find(name + "Label").GetComponent<Text>().text = name + " = " + GameObject.Find(name).GetComponent<Slider>().value;
    }
}
